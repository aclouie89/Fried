using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum player_tag:int { None = 0, Player1, Player2, Both }

public class NormalTable : MonoBehaviour
{

  // all NormalTables can contain one item
  private GameObject item;
  private bool item_exists = false;

  // debug table number
  public int debug_table_num = 0;
  // player tags
  private string player1_tag = "Player1";
  private string player2_tag = "Player2";
  private GameObject player;
  // minimum distance
  private float min_dist = 2.0f;

  void update_playerX_points(int player, int points)
  {

  }

  int check_for_player()
  {
    int player_found = (int)player_tag.None;
    //GameObject[] player1 = GameObject.FindGameObjectsWithTag(player1_tag);
    //GameObject[] player2 = GameObject.FindGameObjectsWithTag(player2_tag);
    GameObject player1 = GameObject.FindWithTag(player1_tag);
    GameObject player2 = GameObject.FindWithTag(player2_tag);
    //Debug.Log("check_for_player()");
    float dist_p1 = Vector3.Distance(player1.transform.position, transform.position);
    float dist_p2 = Vector3.Distance(player2.transform.position, transform.position);

    if(dist_p1 < min_dist && dist_p2 < min_dist)
    {
      player_found = (int)player_tag.Both;
      //Debug.Log("Both players in table zone");
    }
    else if(dist_p1 < min_dist)
    {
      player_found = (int)player_tag.Player1;
      //Debug.Log("Player 1 in export zone");
    }
    else if(dist_p2 < min_dist)
    {
      player_found = (int)player_tag.Player2;
      //Debug.Log("Player 2 in export zone");
    }

    return player_found;
  }

  // Start is called before the first frame update
  void Start()
  {
      
  }

  // Update is called once per frame
  void Update()
  {
    int player = check_for_player();
    // if player is in region
    /*if(player != (int)player_tag.None)
    {
      Debug.Log("Table " + debug_table_num.ToString() + " got hit by a player");
    }*/
  }
}
