using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalTimer : MonoBehaviour
{
    // Start is called before the first frame update
    public float _time=0f;
    bool starttimer=false;
    public float amountOfTime;
    public bool timerOver=false;
    public void StartTimer()
    {
        starttimer = true;
    }

    public void StopTimer()
    {
        starttimer = false;
        _time = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (starttimer)
        {
            _time += Time.deltaTime;
        }
        if(amountOfTime<=_time)
        {
            StopTimer();
        }
    }
}