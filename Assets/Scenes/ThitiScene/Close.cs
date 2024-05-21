using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close : MonoBehaviour
{
    public GameObject pic;

    public float targetTime;

    private void Awake()
    {
        //Sets target timer to 3 seconds every time the canvas is set to Active by the rolegiver.
        targetTime = 2.0f;
    }
    void Update()
    {
        //Counts down the time until it is less than or equal to 0. 
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

    }

    void timerEnded()
    {
        //Turns the canvas off.
        pic.SetActive(false);
    }
    
}





