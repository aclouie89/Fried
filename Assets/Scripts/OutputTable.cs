using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum player_tag { None = 0, Player1, Player2, Both }
enum order_status { Invalid = 0, Valid }

public class OutputTable : MonoBehaviour
{
  /* DEBUG levels
   * 1 - Actions only
   * 2 - Search level
   * 3 - Verbose
   * 4 - Hyperverbose
   */
  int DEBUG = 3;

    // final plate list, EDIT THIS FOR EACH LEVEL
    private string[] final_tag = { "plate_tomato_lettuce_cheese", "plate_cheeseburger" ,"plate_burger" };
  // point list MUST match final_tag list in length
  // each plate can be worth different amounts
  private int[] final_points = {1};

    public Score score;
    //private Score score;
    /*public Score1 p1_score;
    public Score2 p2_score;*/
    //display order
    public DisplayOrder displayOrder;

  // minimum distance settings for player/order detection
  private float player_min_dist = 2.0f;
  private float order_min_dist_y = 0.5f;

  // Game tags and identifiers
  private string[] cur_orders = {"", "", ""};
  private string player1_tag = "Player1";
  private string player2_tag = "Player2";

  // debug variable
  private bool DEBUG_IN_ZONE;

  void dbgprint(int level, string text)
  {
    if(DEBUG >= level)
      Debug.Log(text);
  }
  
  IEnumerator waitForSeedChange()
  {
    yield return new WaitForSeconds(1);
  }

  // randomly picks order from all possible orders
  // Seed could be improved
  private string NewRandomOrder(int index)
  {
    System.Random rnd = new System.Random(index * index * index + (int)Time.time);
    int rnd_index = rnd.Next(final_tag.Length);
    StartCoroutine(waitForSeedChange());
    dbgprint(3, "NewRandomOrder() generated random number: " + rnd_index.ToString());
    return final_tag[rnd_index];
  }
  // updates the acceptable orders
  private void UpdateToList(int index)
  {
    cur_orders[index] = NewRandomOrder(index);
    dbgprint(2, "Adding " + cur_orders[index] + " to acceptable orders");
    displayOrder.display(cur_orders[index]);
  }

  public int GetIndex(string order)
  {
    for(int i = 0; i < cur_orders.Length; i++)
      if(order == cur_orders[i])
        return i;
    return -1;
  }

  private int GetFinalIndex(string order)
  {
    for(int i = 0; i < final_tag.Length; i++)
      if(order == final_tag[i])
        return i;
    return -1;  
  }

  public void playerPlaced(int player, GameObject order)
  {
    UpdatePlayerXPoints(player, order);
  }

  // BURN BABY BURN
  IEnumerator WaitToDestroy(GameObject order)
  {
    dbgprint(3, "Waiting to Destroy");
    yield return new WaitForSeconds(2);
    Destroy(order);
  }

  void UpdatePlayerXPoints(int player_found, GameObject order)
  {
    if(player_found != (int)player_tag.None && order != null)
    {
      int order_index = GetIndex(order.tag);
      if(order_index != -1)
      {
        if(player_found != (int)player_tag.None)
        {
          int points = -9999;
          points = final_points[GetFinalIndex(order.tag)];
          dbgprint(1, "Player " + player_found.ToString() + " scored " + points.ToString());
          if(points == -9999)
            dbgprint(0, "DEVELOPER, DID YOU FORGET TO ADD TO THE FINAL_POINTS STRING IN OUTPUTTABLE.CS?!");
          // get new item in our list
          UpdateToList(order_index);
          // score our player
          score.ScorePlayer(player_found, points);
          score.UpdatePoint();
          // prevent the user from picking up this plate and using it to score
          order.tag = "final_scored";
          // wait to destroy, we can do something else here like animation
          StartCoroutine(WaitToDestroy(order));
          displayOrder.DestoryOrder(order.tag);
        }
      }
      else
      {
        dbgprint(1, "Order placed is not on current list: " + order.tag);
      }
    }
  }

  // get the object name on top of the table
  GameObject CheckForOrder()
  {
    GameObject placed_final = null;
    float closest_distance = Mathf.Infinity;
    // find closest object
    for(int i = 0; i < final_tag.Length; i++)
    {
      GameObject[] gos = GameObject.FindGameObjectsWithTag(final_tag[i]);
      GameObject closest = null;
      bool new_closest = false;
      Vector3 position = transform.position;
      foreach (GameObject go in gos)
      {
        Vector3 diff = go.GetComponent<Transform>().position - position;
        float curDistance = diff.sqrMagnitude;
        if (curDistance < closest_distance && curDistance < order_min_dist_y)
        {
          new_closest = true;
          closest = go;
          dbgprint(3, "Found Final Order: " + closest.name);
          closest_distance = curDistance;
        }
      }
      if(new_closest)
      {
        placed_final = closest;
        dbgprint(3, "Found Final Order: " + placed_final.name);
      }
    }

    return placed_final;
  }

  // check if player is near the table
  int CheckForPlayer()
  {
    int player_found = (int)player_tag.None;
    GameObject player1 = GameObject.FindWithTag(player1_tag);
    GameObject player2 = GameObject.FindWithTag(player2_tag);
    //Debug.Log("CheckForPlayer()");
    float dist_p1 = Vector3.Distance(player1.transform.position, transform.position);
    float dist_p2 = Vector3.Distance(player2.transform.position, transform.position);

    if(dist_p1 < player_min_dist && dist_p2 < player_min_dist)
    {
      player_found = (int)player_tag.Both;
      if(DEBUG_IN_ZONE == true)
        dbgprint(3, "Both Players in export zone");
      DEBUG_IN_ZONE = false;
    }
    else if(dist_p1 < player_min_dist)
    {
      player_found = (int)player_tag.Player1;
      if(DEBUG_IN_ZONE == true)
        dbgprint(3, "Player 1 in export zone");
      DEBUG_IN_ZONE = false;
    }
    else if(dist_p2 < player_min_dist)
    {
      player_found = (int)player_tag.Player2;
      if(DEBUG_IN_ZONE == true)
        dbgprint(3, "Player 2 in export zone");
      DEBUG_IN_ZONE = false;
    }
    else
    {
      if(DEBUG_IN_ZONE == false)
        dbgprint(3, "Player left zone");
      DEBUG_IN_ZONE = true;
    }

    return player_found;
  }

  // Start is called before the first frame update
  void Start()
  {
    //score = new Score();
    for(int i = 0; i < 3; i++)
    {
      UpdateToList(i);
    }
  }

  // Update is called once per frame
  void Update()
  {
    // Following two lines are no longer used as we have a better way to determine
    // which player dropped the plate
    /* int player_found = CheckForPlayer();
    GameObject obj_found = CheckForOrder();
    */
  }
}