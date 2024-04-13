using Alteruna;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Vivox;
using UnityEngine;

public class PlayerLocator : MonoBehaviour
{
    float _nextPosUpdate;
    Alteruna.Avatar _avatar;
    Multiplayer multiplayer;
    // Start is called before the first frame update
    void Start()
    {
        _avatar = GetComponent<Alteruna.Avatar>();
        multiplayer = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<Multiplayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_avatar.IsMe)
        {
            if (Time.time > _nextPosUpdate)
            {
                VivoxService.Instance.Set3DPosition(transform.gameObject, multiplayer.CurrentRoom.Name);
                _nextPosUpdate += 0.3f;
                Debug.Log("Updated Player Location");
            }
        }
        
    }
}
