using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player ID enum
enum PlayerNum {None = 0, One, Two}
// Player status
enum PlayerStatus {Start = 0, Normal, iFrame, CC}
// Key ghosting state
enum KeyGhost {None = 0, DownOnce, DownHold, UpOnce}

// Player Controller
/* - Handles Movement
 */
public class PlayerControl : MonoBehaviour
{
  /* DEBUG levels
   * 1 - Actions only
   * 2 - Search level
   * 3 - Verbose
   * 4 - Hyperverbose
   */
  int DEBUG = 1;
  // Character related
  public int player_id = (int)PlayerNum.None;
  private string p_vertical = "Vertical";
  private string p_horizontal = "Horizontal";
  private string p_interact = "Interact";
  private string p_iframe_key = "iFrame_Key";

  // Pickup/Placing related
  private int key_ghost = (int)KeyGhost.None;
  // these following 3 string arrays must be updated for new tags to work
  // MAKE SURE PICKUP TAGS HAS THE SAME ORDER:
  //  SPAWNER, ITEM, PROCESS_ITEM
  string[] pickup_tags = {"tomato_spawner", "tomato", "cut_tomato", 
                          "cheese_spawner", "Cheese", "cut_cheese",
                          "lettuce_spawner", "Lettuce", "cut_lettuce",
                          "plate_spawner", "Plate"};
  string[] putdown_tags = {"normal_table", "output_table", "trashcan"};
  // minimum distance
  private float min_dist_pickup = 3.0f;
  private float min_dist_putdown = 4.0f;
  // holding item
  GameObject player_item;
  private bool holding_item = false;
  private bool processing_pickup_putdown = false;
  // movement for held items
  public float smoothTime = 0.001f;
  private Vector3 AVelocity = Vector3.zero;

  // Collision related
  GameObject closest_table;
  string[] collision_tags = {"Player1", "Player2", "normal_table"};

  // Movement related
  CharacterController controller;
  private float movementSpeed = 4.0F;
  private float jumpSpeed = 8.0F;
  private float gravity = 32.0F;
  private float rotateSpeed = 1800.0f;
  private Vector3 moveDirection = Vector3.zero;
  private bool movementEnabled = true;

  private int status = (int)PlayerStatus.Start;
  private float iFrame_time = 0.5f;
  private float DoubleTapCD = 0.5f;
  private int DoubleTapCount = 0;

  void dbgprint(int level, string text)
  {
    if(DEBUG >= level)
      Debug.Log(text);
  }

  // Starting function
  void Start()
  {
    controller = GetComponent<CharacterController>();

    // set player one
    if (player_id == (int)PlayerNum.One)
    {
      p_vertical = "P1Vertical";
      p_horizontal = "P1Horizontal";
      p_iframe_key = "P1_iFrame_Key";
      p_interact = "P1Interact";
    }
    else if (player_id == (int)PlayerNum.Two)
    {
      p_vertical = "P2Vertical";
      p_horizontal = "P2Horizontal";
      p_iframe_key = "P2_iFrame_Key";
      p_interact = "P2Interact";
    }

    status = (int)PlayerStatus.Normal;
  }

  // allow movement
  void allowMovement(bool flag)
  {
    movementEnabled = flag;
  }

  private void Movement()
  {
    // only change movement when grounded
    if (controller.isGrounded)
    {
        moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
        // move forward
        if (Input.GetButton(p_vertical))
        {
            moveDirection.z += Input.GetAxis(p_vertical);
        }
        // move sideways
        if (Input.GetButton(p_horizontal))
        {
            moveDirection.x += Input.GetAxis(p_horizontal);
        }
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= movementSpeed;
    }

    // move character
    moveDirection.y -= gravity * Time.deltaTime;
    controller.Move(moveDirection * Time.deltaTime);
  }

  private string spawnerToItem(string spawner_text)
  {
    for(int i = 0; i < pickup_tags.Length; i++)
    {
      if(spawner_text == pickup_tags[i])
        return pickup_tags[i+1];
    }
    // SOMETHING WRONG HAPPENED HEAR
    dbgprint(0, "INVALID SPAWNER TYPE, did you forget to add the strings in PLayerControl.cs");
    return "";
  }

  // generate a clone
  private GameObject cloneObject(GameObject spawner)
  {
    dbgprint(2, "Cloning object: " + spawner.tag.ToString());
    // generate clone
    GameObject clone = Instantiate(spawner, spawner.transform.position, Quaternion.identity) as GameObject;
    clone.tag = spawnerToItem(clone.tag);

    return clone;
  }

  // pick an object up, works on spawners, plates and ingredients of all sorts
  // reference: https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
  private void PickUp()
  {
    if (Input.GetButtonDown(p_interact) && key_ghost == (int)KeyGhost.DownOnce
      && holding_item == false && processing_pickup_putdown == false)
    {
      key_ghost = (int)KeyGhost.DownHold;
      processing_pickup_putdown = true;
      dbgprint(2, "player hit T");
      float closest_distance = Mathf.Infinity;
      // find closest object
      for(int i = 0; i < pickup_tags.Length; i++)
      {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(pickup_tags[i]);
        GameObject closest = null;
        bool new_closest = false;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
          Vector3 diff = go.GetComponent<Transform>().position - position;
          float curDistance = diff.sqrMagnitude;
          if (curDistance < closest_distance && curDistance < min_dist_pickup)
          {
            new_closest = true;
            closest = go;
            dbgprint(3, "Found GameObject: " + closest.name);
            closest_distance = curDistance;
          }
        }
        if(new_closest)
        {
          player_item = closest;
          dbgprint(3, "Found GameObject: " + player_item.name);
        }
      }

      if(player_item != null)
      {
        // is a spawner
        if(player_item.tag.Contains("_spawner"))
        {
          dbgprint(3, "Found spawner: " + player_item.name.ToString());
          player_item = cloneObject(player_item);
        }
        // place it on our head
        player_item.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        dbgprint(1, "Picking up: " + player_item.name);
        holding_item = true;
      }
      else
      {
        dbgprint(1, "No item found to pickup");
        holding_item = false;
      }
      processing_pickup_putdown = false;
    }
  }

  // put an object down - works on counters, export tables
  private void PutDown()
  {
    if (Input.GetButtonDown(p_interact) && key_ghost == (int)KeyGhost.DownOnce
      && holding_item == true && processing_pickup_putdown == false)
    {
      key_ghost = (int)KeyGhost.DownHold;
      processing_pickup_putdown = true;
      dbgprint(3, "player hit T");
      float closest_distance = Mathf.Infinity;
      GameObject table = null;
      // find closest object
      for(int i = 0; i < putdown_tags.Length; i++)
      {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(putdown_tags[i]);
        GameObject closest = null;
        bool new_closest = false;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
          Vector3 diff = go.GetComponent<Transform>().position - position;
          dbgprint(4, "Checking gameObject: " + go.name);
          float curDistance = diff.sqrMagnitude;
          if (curDistance < closest_distance && curDistance < min_dist_putdown)
          {
            new_closest = true;
            closest = go;
            dbgprint(3, "Found GameObject table: " + closest.name);
            closest_distance = curDistance;
          }
          else
          {
            dbgprint(4, "Table too far: " + curDistance.ToString());
          }
        }
        if(new_closest)
        {
          table = closest;
          dbgprint(3,"Found GameObject table to put on: " + table.name);
        }
      }

      // pick up item
      if(table != null)
      {
        // location is a trash can
        if(table.tag == "trashcan")
        {
          // KILL THIS OBJECT
          Destroy(player_item);
        }
        else 
        {
          // place it on our table
          player_item.transform.position = new Vector3(table.transform.position.x, table.transform.position.y + 0.35f, table.transform.position.z + 0.05f);
        }
        dbgprint(1, "Putting down object on: " + table.name);
        // set object
        player_item = null;
        holding_item = false;
      }
      else
      {
        dbgprint(1, "No Table found ");
        holding_item = true;
      }
      processing_pickup_putdown = false;
    }
  }

  private IEnumerator iFrameRotate()
  {
    float cur_time = iFrame_time;
    //Debug.Log("iFraming");
    while(cur_time > 0)
    {
      transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
      cur_time -= Time.deltaTime;
      yield return null;
    }
    //Debug.Log("Normal");
    status = (int)PlayerStatus.Normal;
    transform.rotation = Quaternion.identity;
  }

  // iframe
  private void iFrame()
  {
    if (controller.isGrounded)
    {
      if (Input.GetButtonDown(p_iframe_key) && status == (int)PlayerStatus.Normal)
      {
        status = (int)PlayerStatus.iFrame;
        StartCoroutine(iFrameRotate());
        //iFrameRotate();
        // Rotate back to normal
      }
    }
  }

  // https://answers.unity.com/questions/240541/gameobjectfind-closest-.html
  /*public void setNearestTable(GameObject table)
  {
    closest_table = 
  }*/

  // non oriented movement
  void Update()
  {
    // prevent interact key from being fired multiple times
    if(Input.GetButton(p_interact))
    {
      // waiting state
      if(key_ghost == (int)KeyGhost.None)
        key_ghost = (int)KeyGhost.DownOnce;
      // after processing pickup/putdown
      else if(key_ghost == (int)KeyGhost.DownHold && processing_pickup_putdown == false)
        key_ghost = (int)KeyGhost.UpOnce;
    }
    else if(key_ghost != (int)KeyGhost.None)
      key_ghost = (int)KeyGhost.None;


    if (status == (int)PlayerStatus.Normal)
    {
      iFrame();
      Movement();
      PickUp();
      PutDown();
    }

    // update held item position
    if(holding_item)
    {
      //vec = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
      player_item.transform.position = Vector3.SmoothDamp(player_item.transform.position, transform.position + new Vector3(0, 2, 0), ref AVelocity, smoothTime);
    }
  }

}
