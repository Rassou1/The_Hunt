using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalTimer : MonoBehaviour
{
    public float time = 0f;
    bool startTimer = false;
    public float amountOfTime;
    public bool timerOver = false;

    void Start()
    {
        // Optional initialization
    }

    void Update()
    {
        if (startTimer)
        {
            time += Time.deltaTime;

            if (time >= amountOfTime)
            {
                StopTimer();
                timerOver = true; // Set the timer as over
            }
        }
    }

    public void StartTimer()
    {
        startTimer = true;
        timerOver = false; // Reset timerOver when starting
    }

    public void StopTimer()
    {
        startTimer = false;
        // Do not reset time here if you want to preserve the elapsed time
    }

    public void ResetTimer()
    {
        time = 0f;
        timerOver = false;
    }
}
