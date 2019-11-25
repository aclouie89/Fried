﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime = 3f;
    private float currentTime = 0f;
    //public Audio sound;
    

    void Start()
    {
        //sound.playStartSound();
        currentTime = startTime;


    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        
        timerText.text = currentTime.ToString("0");


        if (currentTime <= 0.0)
        {

            currentTime = 3f;
        
            SceneManager.LoadScene("Scene 1");
        }
    }
}
