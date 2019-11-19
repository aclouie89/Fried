using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Winner : MonoBehaviour
{
    public TextMeshProUGUI Win;


    public Score winner;
    IEnumerator WaitToDestroy()
    {
        Debug.Log("Waiting to Destroy");
        yield return new WaitForSeconds(10f);
        
    }
    public void win()
    {
        
        if (winner.player1_score > winner.player2_score)
        {
            Win.text = "Game Over Player1 Win, after 10s Back to Main Menu";
            
            Debug.Log("Player1 Score: " + winner.player1_score);
            Debug.Log("Player2 Score: " + winner.player2_score);
            WaitToDestroy();
            SceneManager.LoadScene("Player1Win");
        }
        else if (winner.player1_score < winner.player2_score)
        {
            Win.text = "Game Over Player2 Win,, after 10s Back to Main Menu";
            WaitToDestroy();
            Debug.Log("Player1 Score: " + winner.player1_score);
            Debug.Log("Player2 Score: " + winner.player2_score);
            SceneManager.LoadScene("Player2Win");
        }
        else
        {
            Win.text = "Game Over Tied, after 10s Back to Main Menu ";
            WaitToDestroy();
            Debug.Log("Player1 Score: " + winner.player1_score);
            Debug.Log("Player2 Score: " + winner.player2_score);
            SceneManager.LoadScene("Tied");
        }

    }
 
}
