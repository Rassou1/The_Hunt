using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Timer class. I didn't want to run some "if(_timerActive)" check every frame so I decided to have it work like the door scripts which just
//run when they exist and then get deleted when not needed anymore - Love
public class SP_Timer : MonoBehaviour
{
    private float _time;

    void Awake()
    {
        _time = 0;
    }

    void Update()
    {
        _time += Time.deltaTime;
    }

    public float GetTime()
    {
        return _time;
    }
}
