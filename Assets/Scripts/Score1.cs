using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score1 : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score = 0;



    public void Score()
    {
        score = score + 1;
        scoreText.text = score.ToString();
        print(score);

    }
    public void Wrong()
    {
        score = score - 1;
        scoreText.text = score.ToString();
        print(score);
    }
}
