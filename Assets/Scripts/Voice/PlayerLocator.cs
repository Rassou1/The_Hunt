using System.Collections;
using System.Collections.Generic;
using Unity.Services.Vivox;
using UnityEngine;

public class PlayerLocator : MonoBehaviour
{
    float _nextPosUpdate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Time.time > _nextPosUpdate)
        {
            VivoxService.Instance.Set3DPosition(GameObject.FindWithTag("Player"), "ChannelName");
            _nextPosUpdate += 0.3f;
        }
    }
}
