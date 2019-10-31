﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
  /* DEBUG levels
   * 1 - Actions only
   * 2 - Search level
   * 3 - Verbose
   * 4 - Hyperverbose
   */
  int DEBUG = 1;

  private int player1_score = 0;
  private int player2_score = 0;
  public TextMeshProUGUI player1_scoreText;
  public TextMeshProUGUI player2_scoreText;

  void dbgprint(int level, string text)
  {
    if(DEBUG >= level)
      Debug.Log(text);
  }

  public void ScorePlayer(int player_id, int points_to_add)
  {
    if(player_id == (int)PlayerNum.One)
      player1_score += points_to_add;
    else if(player_id == (int)PlayerNum.Two)
      player2_score += points_to_add;
    else
      dbgprint(1, "ERROR, no player_id specified in Score()");

    //player1_scoreText.text = player1_score.ToString();
    //player2_scoreText.text = player2_score.ToString();

  }
}