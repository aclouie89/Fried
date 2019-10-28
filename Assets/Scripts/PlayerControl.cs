using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player ID enum
enum PlayerNum {None = 0, One, Two}
// Player status
enum PlayerStatus {Start = 0, Normal, iFrame, CC}
// Key ghosting state
enum KeyGhost {None = 0, DownOnce, DownHold, UpOnce}
// Player facing
enum PlayerOrientation {North = 0, East, South, West}

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
  private string p_vertical;
  private string p_horizontal;
  private string p_interact_key;
  private string p_iframe_key;
  private string p_throw_key;

  // Pickup/Placing related
  private int key_ghost = (int)KeyGhost.None;
  // these following 3 string arrays must be updated for new tags to work
  // MAKE SURE PICKUP TAGS HAS THE SAME ORDER:
  //  SPAWNER, ITEM, PROCESS_ITEM
  string[] pickup_tags = {
                          // Ingredients
                          // spawner, ingredient, processed_ingredient
                          "tomato_spawner", "tomato", "cut_tomato", 
                          "cheese_spawner", "Cheese", "cut_cheese",
                          "lettuce_spawner", "Lettuce", "cut_lettuce",
                          // combined plate names
                          // final plate names
                          "test_final_plate",
                          // etc items
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
  private float smoothTime = 0.001f;
  private Vector3 AVelocity = Vector3.zero;

  // Collision related
  GameObject closest_table;
  string[] collision_tags = {"Player1", "Player2", "normal_table"};

  // Movement related
  CharacterController controller;
  private float movementSpeed = 4.0f;
  private float gravity = 32.0F;
  private float rotateSpeed = 1800.0f;
  private Vector3 moveDirection = Vector3.zero;
  private bool movementEnabled = true;

  private int status = (int)PlayerStatus.Start;
  private float iFrame_time = 0.5f;
  private float CC_time = 1.5f;
  private float DoubleTapCD = 0.5f;
  private int DoubleTapCount = 0;

  // Throwing related
  private int orientation;
  private float throwSpeed = 16.0f;

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
      p_interact_key = "P1Interact";
      p_throw_key = "P1Throw";
    }
    else if (player_id == (int)PlayerNum.Two)
    {
      p_vertical = "P2Vertical";
      p_horizontal = "P2Horizontal";
      p_iframe_key = "P2_iFrame_Key";
      p_interact_key = "P2Interact";
      p_throw_key = "P2Throw";
    }

    status = (int)PlayerStatus.Normal;
    orientation = (int) PlayerOrientation.South;

    // Players are shrinking after iframe, WHY   
    StartCoroutine(iFrameRotate());
  }

  // allow movement
  void allowMovement(bool flag)
  {
    movementEnabled = flag;
  }

  private void UpdateOrientation()
  {
    // ORDER?!?!?!?!?
    // how do i know which way they are facing if theyre hitting multiple keys
    // im just going to let this cascade and pick
    if(Input.GetAxis(p_vertical) > 0)
      orientation = (int) PlayerOrientation.North;
    if(Input.GetAxis(p_vertical) < 0)
      orientation = (int) PlayerOrientation.South;
    if(Input.GetAxis(p_horizontal) > 0)
      orientation = (int) PlayerOrientation.East;
    if(Input.GetAxis(p_horizontal) < 0)
      orientation = (int) PlayerOrientation.West;

    dbgprint(5, "Player is facing: " + orientation.ToString());
  }

  private void Movement()
  {
    // only change movement when grounded
    if (controller.isGrounded)
    {
        // update which way we're facing
        UpdateOrientation();

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
    if (Input.GetButtonDown(p_interact_key) && key_ghost == (int)KeyGhost.DownOnce
      && holding_item == false && processing_pickup_putdown == false)
    {
      processing_pickup_putdown = true;
      key_ghost = (int)KeyGhost.DownHold;
      dbgprint(2, "player hit interact");
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
        dbgprint(2, "Picking up: " + player_item.name);
        holding_item = true;
      }
      else
      {
        dbgprint(2, "No item found to pickup");
      }
      processing_pickup_putdown = false;
    }
  }

  // put an object down - works on counters, export tables
  private void PutDown()
  {
    if (Input.GetButtonDown(p_interact_key) && key_ghost == (int)KeyGhost.DownOnce
      && holding_item == true && processing_pickup_putdown == false)
    {
      processing_pickup_putdown = true;
      key_ghost = (int)KeyGhost.DownHold;
      dbgprint(3, "player hit interact");
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
        dbgprint(2, "Putting down object on: " + table.name);
        // placed here so we can ditch this item
        GameObject temp = player_item;
        // set object
        player_item = null;
        holding_item = false;
        // IF IT'S AN OUTPUT TABLE WE NEED TO SCORE IT
        // call the output table
        if(table.tag == "output_table")
        {
          var link_table = table.GetComponent<OutputTable>();
          link_table.playerPlaced(player_id, temp);
        }
      }
      else
      {
        dbgprint(2, "No Table found ");
      }
      processing_pickup_putdown = false;
    }
  }

  private void ThrowObject()
  {
    if(Input.GetButton(p_throw_key) && holding_item == true)
    {
      dbgprint(3, "player hit throw");
      if(player_item != null)
      {
        var link_projectile = player_item.GetComponent<Projectile>();
        string player_tag = "";
        if(player_id == (int)PlayerNum.One)
          player_tag = "Player2";
        else if (player_id == (int)PlayerNum.Two)
          player_tag = "Player1";
        link_projectile.setThrown(orientation, player_tag);
        player_item = null;
        holding_item = false;
      }
    }
  }

  // iframe, take the wheels jesus
  private IEnumerator iFrameRotate()
  {
    float cur_time = iFrame_time;
    while(cur_time > 0)
    {
      transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
      cur_time -= Time.deltaTime;
      yield return null;
    }
    dbgprint(3, "iFrame complete");
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
        dbgprint(2, gameObject.tag + " iFrame started");
        StartCoroutine(iFrameRotate());
      }
    }
  }

  // cc'd take the wheels jesus
  private IEnumerator CCStutter()
  {
    float cur_time = CC_time;
    while(cur_time > 0)
    {
      //transform.Rotate(0, rotateSpeed/5 * Time.deltaTime, 0);
      cur_time -= Time.deltaTime;
      yield return null;
    }
    dbgprint(3, "iFrame complete");
    status = (int)PlayerStatus.Normal;
    transform.rotation = Quaternion.identity;
  }

  // hit by an object
  public void HitByObject()
  {
    if(status != (int)PlayerStatus.iFrame)
    {
      status = (int)PlayerStatus.CC;
      dbgprint(2, gameObject.tag + " CC'd");
      // maybe do this better
      transform.Rotate(0, 0.0f, 45.0f);
      StartCoroutine(CCStutter());
    }
    else
    {
      dbgprint(2, gameObject.tag + " iFrame'd that CC");
    }
  }

  // non oriented movement
  void Update()
  {
    // prevent interact key from being fired multiple times
    if(Input.GetButton(p_interact_key))
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
      ThrowObject();
      PickUp();
      PutDown();
    // if(player_id == (int)PlayerNum.One)
    //   dbgprint(1, "Holding item: " + holding_item.ToString() );
    }

    // update held item position
    if(holding_item)
    {
      //vec = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
      player_item.transform.position = Vector3.SmoothDamp(player_item.transform.position, transform.position + new Vector3(0, 2, 0), ref AVelocity, smoothTime);
    }
  }

}
