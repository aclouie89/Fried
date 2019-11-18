using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Winner : MonoBehaviour
{
    public TextMeshProUGUI Win;


    public Score winner;
    public void win()
    {
        
        if (winner.player1_score > winner.player2_score)
        {
            Win.text = "Player1 Win";
            SceneManager.LoadScene("Player1Win");
        }
        else if (winner.player1_score < winner.player2_score)
        {
            Win.text = "Player2 Win";
            SceneManager.LoadScene("Player2Win");
        }
        else
        {
            Win.text = "Tied ";
            SceneManager.LoadScene("Tied");
        }

    }
 
}
