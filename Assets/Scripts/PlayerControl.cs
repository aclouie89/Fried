using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player ID enum
enum PlayerNum {None = 0, One, Two}
// Player status
enum PlayerStatus {Start = 0, Normal, iFrame, CC, Chopping}
// Key ghosting state
enum KeyGhost {None = 0, DownOnce, DownHold, UpOnce}
// Player facing
enum PlayerOrientation {North = 0, East, South, West}
// PLayer material
enum PlayerMat {StandN = 0, StandE, StandS, StandW}

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
  int DEBUG = 3;
  // Character related
  public int player_id = (int)PlayerNum.None;
  private string p_vertical;
  private string p_horizontal;
  private string p_interact_key;
  private string p_iframe_key;
  private string p_throw_key;

  // Animation related
  private Material[] player_mat;
  int last_mesh_index = (int)PlayerMat.StandN;

  // Pickup/Placing related
  private int key_ghost = (int)KeyGhost.None;

  // this must be updated for the number of raw ingredients
  int num_ingredients = 5;
  int num_spoiled = 1;
  int num_mid_plates = 16;
  int num_final_plates = 3;
  // index of RAW materials
  int[] type_ingredient;
  // index of SPOILED materials
  int[] type_spoiled;
  // midway finished plates
  int[] type_mid;
  // finished plates
  int[] type_finished;

  // these following 2 string arrays must be updated for new tags to work
  // MAKE SURE PICKUP TAGS HAS THE SAME ORDER:
  //  SPAWNER, ITEM, PROCESS_ITEM
  string[] pickup_tags = {
                          // Ingredients
                          // spawner, ingredient, processed_ingredient
                          "tomato_spawner", "tomato", "cut_tomato",
                          "cheese_spawner", "Cheese", "cut_cheese",
                          "lettuce_spawner", "Lettuce", "cut_lettuce",
                          "bread_spawner", "bread", "bread",
                          "steak_spawner", "steak", "cooked_steak",
                          "burnt_steak",
                          // combined plate names
                          "plate_tomato", "plate_cheese", "plate_lettuce", "plate_bread",
                          "plate_tomato_lettuce", "plate_tomato_cheese", "plate_lettuce_cheese",
                          "Plate_Bread_Cheese", "Plate_Bread_Steak", "Plate_Bread_Lettuce",
                          "Plate_Lettuce_Steak", "Plate_Cheese_Steak", "Plate_Bread_Cheese_Lettuce", "Plate_Bread_Cheese_Steak",
                          "Plate_Bread_Lettuce_Steak", "Plate_Cheese_Lettuce_Steak", 
                          // final plate names
                          "test_final_plate","plate_tomato_lettuce_cheese",
                            "plate_cheeseburger", "plate_burger",
                          // etc items
                          "plate_spawner", "Plate"};
  string[] putdown_tags = {"normal_table", "output_table", "trashcan","Chopping_Board", "Cooking_Pan"};
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

  // Processing ingredients related
  private float process_wait_time = 0f;
  private float process_start_time = 0f;
  GameObject processing_table;
  GameObject processing_item;


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

  public int status = (int)PlayerStatus.Start;
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

    player_mat = GetComponent<Renderer>().materials;

    // INIT index for types, assumes these strings are the first types
    // find raw materials
    int i = 2;
    for(int x = 0; x < pickup_tags.Length; x++)
      if(pickup_tags[x] == "tomato")
      {
        i = x;
        break;
      }
    type_ingredient = new int[num_ingredients];
    for(int x = 0; x < num_ingredients; x++)
    {
      type_ingredient[x] = i;
      i += 3;
      dbgprint(3, "Found ingredient: " + pickup_tags[type_ingredient[x]]);
      dbgprint(3, "Found processed ingredient: " + pickup_tags[type_ingredient[x]+1]);
    }
    // find spoiled materials
    for(int x = 0; x < pickup_tags.Length; x++)
      if(pickup_tags[x] == "burnt_steak")
      {
        i = x;
        break;
      }
    type_spoiled = new int[num_spoiled];
    for(int x = 0; x < num_spoiled; x++)
    {
      type_spoiled[x] = i;
      i += 1;
      dbgprint(3, "Found spoiled ingredient: " + pickup_tags[type_spoiled[x]]);
    }
    // find mid plates
    for(int x = 0; x < pickup_tags.Length; x++)
      if(pickup_tags[x] == "plate_tomato")
      {
        i = x;
        break;
      }
    type_mid = new int[num_mid_plates];
    for(int x = 0; x < num_mid_plates; x++)
    {
      type_mid[x] = i;
      i += 1;
      dbgprint(3, "Found mid_plate: " + pickup_tags[type_mid[x]]);
    }
    // find final plates
    for(int x = 0; x < pickup_tags.Length; x++)
      if(pickup_tags[x] == "plate_tomato_lettuce_cheese")
      {
        i = x;
        break;
      }
    type_finished = new int[num_final_plates];
    for(int x = 0; x < num_final_plates; x++)
    {
      type_finished[x] = i;
      i += 1;
      dbgprint(3, "Found final_plate: " + pickup_tags[type_finished[x]]);
    }

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
    
    // set up the sprite
    for(int x = 0; x < player_mat.Length; x++)
    {
      GetComponent<Renderer>().materials[x].color = new Color(1,1,1,0f);
    }
    showSingleMesh((int) PlayerMat.StandS);

    // Players are shrinking after iframe, WHY   
    StartCoroutine(iFrameRotate());
  }

  // allow movement
  void allowMovement(bool flag)
  {
    movementEnabled = flag;
  }

  private void meshIndexToOrientation(int index)
  {
    // normal standing
    if(index >= (int)PlayerMat.StandN && index <= (int)PlayerMat.StandW)
      orientation = index;
  }

  // display only one mesh
  private void showSingleMesh(int index)
  {
    if(index != last_mesh_index)
    {
      Color fade = new Color(1,1,1,0f);
      Color vis = new Color(1,1,1,1f);
      GetComponent<Renderer>().materials[index].color = new Color(1,1,1,1f);;
      GetComponent<Renderer>().materials[last_mesh_index].color = new Color(1,1,1,0f);
      last_mesh_index = index;
      meshIndexToOrientation(index);
    }
  }

  private void UpdateOrientation()
  {
    // ORDER?!?!?!?!?
    // how do i know which way they are facing if theyre hitting multiple keys
    // im just going to let this cascade and pick
    if(Input.GetButton(p_vertical) && Input.GetAxis(p_vertical) > 0)
    {
      dbgprint(5, "Player is facing: North");
      showSingleMesh((int) PlayerMat.StandN);
    }
    else if(Input.GetButton(p_vertical) && Input.GetAxis(p_vertical) < 0)
    {
      dbgprint(5, "Player is facing: South");
      showSingleMesh((int) PlayerMat.StandS);
    }
    else if(Input.GetButton(p_horizontal) && Input.GetAxis(p_horizontal) > 0)
    {
      dbgprint(5, "Player is facing: East");
      showSingleMesh((int) PlayerMat.StandE);
    }
    else if(Input.GetButton(p_horizontal) && Input.GetAxis(p_horizontal) < 0)
    {
      dbgprint(5, "Player is facing: West");
      showSingleMesh((int) PlayerMat.StandW);
    }

    dbgprint(5, "Player is facing: " + orientation.ToString());
  }

  private void Movement()
  {
    // only change movement when grounded
    if (controller.isGrounded)
    {
        // update which way we're facing
        //UpdateOrientation();

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

  // finds closest game object given a list
  private GameObject findClosestGameObject(string[] tags, float min_dist)
  {
    float closest_distance = Mathf.Infinity;
    GameObject closest_go = null;
    // find closest object
    for(int i = 0; i < tags.Length; i++)
    {
      GameObject[] gos = GameObject.FindGameObjectsWithTag(tags[i]);
      GameObject closest = null;
      bool new_closest = false;
      Vector3 position = transform.position;
      foreach (GameObject go in gos)
      {
        Vector3 diff = go.GetComponent<Transform>().position - position;
        float curDistance = diff.sqrMagnitude;
        if (curDistance < closest_distance && curDistance < min_dist)
        {
          new_closest = true;
          closest = go;
          closest_distance = curDistance;
        }
      }
      if(new_closest)
      {
        closest_go = closest;
      }
    }
    return closest_go;
  }

  // returns true if raw ingredient
  private bool isRawIngredient(GameObject go)
  {
    for(int i = 0; i < type_ingredient.Length; i++)
      if(go.tag == pickup_tags[type_ingredient[i]])
        return true;
    return false;
  }

  // returns true if processed ingredient
  private bool isProcessedIngredient(GameObject go)
  {
    for(int i = 0; i < type_ingredient.Length; i++)
      if(go.tag == pickup_tags[type_ingredient[i]+1])
        return true;
    return false;
  }

  // returns true if spoiled ingredient
  private bool isSpoiledIngredient(GameObject go)
  {
    for(int i = 0; i < type_spoiled.Length; i++)
      if(go.tag == pickup_tags[type_spoiled[i]+1])
        return true;
    return false;
  }

  // returns true if it is a midway finished plate
  private bool isMidPlate(GameObject go)
  {
    for(int i = 0; i < type_mid.Length; i++)
      if(go.tag == pickup_tags[type_mid[i]])
        return true;
    return false;
  }

  // returns true if processed ingredient
  private bool isFinalPlate(GameObject go)
  {
    for(int i = 0; i < type_finished.Length; i++)
      if(go.tag == pickup_tags[type_finished[i]])
        return true;
    return false;
  }

  // returns true if empty plate
  private bool isEmptyPlate(GameObject go)
  {
    if(go.tag == "Plate")
      return true;
    return false;
  } 

  // check for pick up items and return
  private bool checkIfPlaceable(GameObject go, GameObject held)
  {
    // no item found just return
    if(go == null)
      return true;
    dbgprint(1, "Item on table: " + go.tag);
    dbgprint(1, "Item held: " + held.tag);
    // some if logic to check if it's placeable
    // raw materials cant be placed on: ingredients of any sort, mid plates, finished plates
    if(isProcessedIngredient(held))
    {
      if(isRawIngredient(go) || isProcessedIngredient(go) || isFinalPlate(go))
      {
        dbgprint(1, "Cant place due to " + go.tag);
        return false;
      }
    }
    // raw materials cant be placed on: ingredients of any sort, mid plates, finished plates
    else if(isRawIngredient(held) || isSpoiledIngredient(held))
    {
      if(isRawIngredient(go) || isProcessedIngredient(go) || isEmptyPlate(go) || isMidPlate(go) || isFinalPlate(go))
      {
        dbgprint(1, "Cant place due to " + go.tag);
        return false;
      }
    }
    // plates can only be placed on empty tops,
    else if(isFinalPlate(held) || isMidPlate(held) || isEmptyPlate(held))
    {
      // go is NOT null due to above if statement, so return false
      return false;
    }
    // we can place it
    return true;
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
      // find closest game object to pick up
      player_item = findClosestGameObject(pickup_tags, min_dist_pickup);
      GameObject table = findClosestGameObject(putdown_tags, min_dist_putdown);

      if(player_item != null)
      {
        // is a spawner
        if(player_item.tag.Contains("_spawner"))
        {
          dbgprint(3, "Found spawner: " + player_item.name.ToString());
          player_item = cloneObject(player_item);
        }
        // if we picked it up from a table, tell the table it no longer has an item
        else if(table.tag == "normal_table" || table.tag == "Chopping_Board" || table.tag == "Cooking_Pan")
        {
          table.GetComponent<NormalTable>().removeOnTable();
          // tell the cooking pan we picked up the item
          if(table.tag == "Cooking_Pan")
          {
            table.GetComponent<CookingSwapper>().PlayerPickedUpItem();
          }
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
      // find closest game object to place item on
      GameObject table = findClosestGameObject(putdown_tags, min_dist_putdown);
      // pick up item
      if(table != null)
      {
        GameObject item_on_table = null;
        // check if anything is on counter
        if(table.tag == "normal_table" || table.tag == "Chopping_Board" || table.tag == "Cooking_Pan")
          item_on_table = table.GetComponent<NormalTable>().isOnTable();
        if(checkIfPlaceable(item_on_table, player_item) == false)
        {
          processing_pickup_putdown = false;
          dbgprint(1, "Can't place item here, object on table is incompatible");
          return; 
        }
        // location is a trash can
        if(table.tag == "trashcan")
        {
          // KILL THIS OBJECT
          Destroy(player_item);
        }
        else 
        {
          dbgprint(1, "Placing " + player_item.tag + " onto table " + table.tag + " (" + table.name + ")");
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
        // if it's a normal table we need to tell it we put something there
        // call the normal table
        else if(table.tag == "normal_table" || table.tag == "Chopping_Board" || table.tag == "Cooking_Pan")
        {
          table.GetComponent<NormalTable>().putOnTable(temp);
        }
        if(table.tag == "Chopping_Board")
        {
          var link_table = table.GetComponent<ChoppingBoardSwapper>();
          // set the processing table and item so we can call back into it in update
          processing_table = table;
          processing_item = temp;
          // set up the boolean as false first
          process_wait_time = link_table.PlayerStartedChopping(gameObject, temp);
          // probably cant chop something
          if(process_wait_time == 0f)
          {
            dbgprint(3, "Can't process " + temp.tag + " at " + link_table.tag);
          }
          // we can process it, start the time
          else
          {
            // set the status
            status = (int)PlayerStatus.Chopping;
            // start the time
            process_start_time = Time.time;
          }
        }
        // cooking is hands off, we dont need to set anything
        else if(table.tag == "Cooking_Pan")
        {
          var link_table = table.GetComponent<CookingSwapper>();
          // set up the boolean as false first
          link_table.PlayerStartedCooking(gameObject, temp);
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

      // purge processing just in case it introduces a bug later
      processing_item = null;
      processing_table = null;

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

    // the player can move
    if (status == (int)PlayerStatus.Normal)
    {
      iFrame();
      UpdateOrientation();
      Movement();
      ThrowObject();
      PickUp();
      PutDown();
    }

    // if we're processing
    else if(status == (int)PlayerStatus.Chopping)
    {
      // end processing
      if(Time.time >= process_start_time + process_wait_time)
      {
        if(status == (int)PlayerStatus.Chopping)
          processing_table.GetComponent<ChoppingBoardSwapper>().PlayerFinishedChopping(processing_item);
        // clear up status
        status = (int)PlayerStatus.Normal;
      }
    }

    // update held item position
    if(holding_item)
    {
      //vec = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
      player_item.transform.position = Vector3.SmoothDamp(player_item.transform.position, transform.position + new Vector3(0, 2, 0), ref AVelocity, smoothTime);
    }
  }

}
