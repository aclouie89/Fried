using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum player_tag { None = 0, Player1, Player2, Both }
enum order_status { Invalid = 0, Valid }

public class OutputTable : MonoBehaviour
{
  /*public int player1_points;
  public int player2_points;*/
  public Score1 p1_score;
  public Score2 p2_score;

  // minimum distance settings for player/order detection
  private float player_min_dist = 2.0f;
  private float order_min_dist_y = 0.5f;
  // final plate list, EDIT THIS FOR EACH LEVEL
  public string[] final_plates = new string[] {"Plate_Salad_Right"};

  // Game tags and identifiers
  private string[] cur_orders = new string[3];
  private string player1_tag = "Player1";
  private string player2_tag = "Player2";

  // debug variable
  private bool DEBUG_IN_ZONE;

  // randomly picks order from all possible orders
  string new_random_order()
  {
    System.Random rnd = new System.Random();
    int rnd_index = rnd.Next(final_plates.Length);
    return final_plates[rnd_index];
  }
  // updates the acceptable orders
  void update_to_list(int index)
  {
    cur_orders[index] = new_random_order();
    Debug.Log("Adding " + cur_orders[index] + " to acceptable orders");
  }

  int get_index(string order)
  {
    for(int i = 0; i < cur_orders.Length; i++)
      if(order == cur_orders[i])
        return i;
    return -1;
  }

  void update_playerX_points(int player_found, string order)
  {
    if(player_found != (int)player_tag.None && order != "")
    {
      int order_index = get_index(order);
      if(order_index != -1)
      {
        if(player_found == (int)player_tag.Player1)
        {
          update_to_list(order_index);
          p1_score.Score();
        }
        if(player_found == (int)player_tag.Player2)
        {
          update_to_list(order_index);
          p2_score.Score();
        }
      }
    }
  }

  // get the object name on top of the table
  string check_for_order()
  {
    return "";
  }

  // check if player is near the table
  int check_for_player()
  {
    int player_found = (int)player_tag.None;
    GameObject player1 = GameObject.FindWithTag(player1_tag);
    GameObject player2 = GameObject.FindWithTag(player2_tag);
    //Debug.Log("check_for_player()");
    float dist_p1 = Vector3.Distance(player1.transform.position, transform.position);
    float dist_p2 = Vector3.Distance(player2.transform.position, transform.position);

    if(dist_p1 < player_min_dist && dist_p2 < player_min_dist)
    {
      player_found = (int)player_tag.Both;
      if(DEBUG_IN_ZONE == true)
        Debug.Log("Both Players in export zone");
      DEBUG_IN_ZONE = false;
    }
    else if(dist_p1 < player_min_dist)
    {
      player_found = (int)player_tag.Player1;
      if(DEBUG_IN_ZONE == true)
        Debug.Log("Player 1 in export zone");
      DEBUG_IN_ZONE = false;
    }
    else if(dist_p2 < player_min_dist)
    {
      player_found = (int)player_tag.Player2;
      if(DEBUG_IN_ZONE == true)
        Debug.Log("Player 2 in export zone");
      DEBUG_IN_ZONE = false;
    }
    else
    {
      if(DEBUG_IN_ZONE == false)
        Debug.Log("Player left zone");
      DEBUG_IN_ZONE = true;
    }

    return player_found;
  }

  // Start is called before the first frame update
  void Start()
  {
    // set up allowable orders
    for(int i = 0; i < 2; i++)
    {
      update_to_list(i);
    }
  }

  // Update is called once per frame
  void Update()
  {
    int player_found = check_for_player();
    //int obj_found = 0;
    string obj_found = check_for_order();
    update_playerX_points(player_found, obj_found);
  }
}
