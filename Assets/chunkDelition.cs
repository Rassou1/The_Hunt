using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chunkDelition : MonoBehaviour
{

    public globalTimer timer;
    // Start is called before the first frame update
    void Start()
    {
        timer.amountOfTime = 1f;
        timer.StartTimer();

    }

    // Update is called once per frame
    void Update()
    {
        if (timer.timerOver)
        {
            Destroy(this.gameObject);
        }

    }
}
