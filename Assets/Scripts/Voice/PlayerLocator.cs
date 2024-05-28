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
    bool readyToChange = false;
    // Start is called before the first frame update
    void Start()
    {
        _avatar = GetComponentInParent<Alteruna.Avatar>();
        multiplayer = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<Multiplayer>();
        VivoxService.Instance.ChannelJoined += JoinedChannel;
        VivoxService.Instance.ChannelLeft += LeftChannel;
        
    }

    //Make a event that enables a bool to broadcast the player position to prevent a bunch of errors when starting

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.GetChild(0).position;
        if (_avatar.IsMe && VivoxService.Instance != null && VivoxService.Instance.IsLoggedIn && readyToChange)
        {
            if (Time.time > _nextPosUpdate)
            {
                VivoxService.Instance.Set3DPosition(transform.gameObject, multiplayer.CurrentRoom.Name);
                _nextPosUpdate += 0.3f;
                Debug.Log("Updated Player Location");
            }
        }
    }

    void JoinedChannel(string channelName)
    {
        readyToChange = true;
    }

    void LeftChannel(string channelName)
    {
        readyToChange = false;
    }
}
