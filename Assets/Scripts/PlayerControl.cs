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
  // Character related
  public int player_id = (int)PlayerNum.None;
  private string p_vertical = "Vertical";
  private string p_horizontal = "Horizontal";
  private string p_interact = "Interact";
  private string p_iframe_key = "iFrame_Key";

  // Pickup/Placing related
  private int key_ghost = (int)KeyGhost.None;
  string[] pickup_tags = {"tomato", "Cheese", "Plate", "Lettuce", "cut_tomato", "cut_lettuce", "cut_cheese"};
  string[] putdown_tags = {"normal_table", "output_table"};
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

  // pick up code
  // reference: https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
  private void PickUp()
  {
    if (Input.GetButtonDown(p_interact) && key_ghost == (int)KeyGhost.DownOnce
      && holding_item == false && processing_pickup_putdown == false)
    {
      key_ghost = (int)KeyGhost.DownHold;
      processing_pickup_putdown = true;
      Debug.Log("player hit T");
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
            Debug.Log("Found GameObject: " + closest.name);
            closest_distance = curDistance;
          }
        }
        if(new_closest)
        {
          player_item = closest;
          Debug.Log("Found GameObject: " + player_item.name);
        }
      }

      // pick up item
      if(player_item != null)
      {
        // place it on our head
        player_item.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        Debug.Log("Picking up: " + player_item.name);
        holding_item = true;
      }
      else
      {
        holding_item = false;
      }
      processing_pickup_putdown = false;
    }
  }

  private void PutDown()
  {
    if (Input.GetButtonDown(p_interact) && key_ghost == (int)KeyGhost.DownOnce
      && holding_item == true && processing_pickup_putdown == false)
    {
      key_ghost = (int)KeyGhost.DownHold;
      processing_pickup_putdown = true;
      Debug.Log("player hit T");
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
          Debug.Log("Checking gameObject: " + go.name);
          float curDistance = diff.sqrMagnitude;
          if (curDistance < closest_distance && curDistance < min_dist_putdown)
          {
            new_closest = true;
            closest = go;
            Debug.Log("Found GameObject table: " + closest.name);
            closest_distance = curDistance;
          }
          else
          {
            Debug.Log("Table too far: " + curDistance.ToString());
          }
        }
        if(new_closest)
        {
          table = closest;
          Debug.Log("Found GameObject table: " + table.name);
        }
      }

      // pick up item
      if(table != null)
      {
        // place it on our head
        player_item.transform.position = new Vector3(table.transform.position.x, table.transform.position.y + 0.35f, table.transform.position.z + 0.05f);
        Debug.Log("Putting down object on: " + table.name);
        // set object
        player_item = null;
        holding_item = false;
      }
      else
      {
        Debug.Log("No Table found ");
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
