using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime=60f;
    private float currentTime=0f;
    public Winner winPlayer;
    
    void Start()
    {
        currentTime = startTime;

        
    }

   void Update()
   {
        currentTime -= 1 * Time.deltaTime;
        if (currentTime <= 30)
        {
            timerText.color = Color.red;
        }
        timerText.text = currentTime.ToString("0");


        if (currentTime <= 0.0)
        {

            currentTime = 0;
            QuitGame();
        }



   }

    public void QuitGame()
    {
        winPlayer.win();

        Debug.Log("Quiting game...");
        
        Application.Quit();
        //SceneManager.LoadScene("Menu");
    }



}
