using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeText;
    public bool isPaused = false;
    float timeToDisplay = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeToDisplay += 1;
        if(!isPaused) DisplayTime();
    }

    void DisplayTime()
    {
        float seconds = Mathf.FloorToInt(timeToDisplay / 60 % 60);  
        float milliseconds = Mathf.FloorToInt(timeToDisplay % 60);
        float minutes = Mathf.FloorToInt(timeToDisplay / (60*60));

        timeText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}
