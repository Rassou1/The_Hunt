using Alteruna;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Vivox;
using UnityEngine;

public class PlayerLocator : MonoBehaviour
{
    //This locates where the player is for the voicechat -Jonathan
    float _nextPosUpdate;
    Alteruna.Avatar _avatar;
    Multiplayer multiplayer;
    bool readyToChange = false;
    void Start()
    {
        _avatar = GetComponentInParent<Alteruna.Avatar>();
        multiplayer = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<Multiplayer>();
        VivoxService.Instance.ChannelJoined += JoinedChannel;
        VivoxService.Instance.ChannelLeft += LeftChannel;
    }


    
    void Update()
    {
        //This gets the rotation and position of the hunter of prey so the other players voice goes out the right direction -Jonathan
        if (transform.parent.GetChild(0).gameObject.activeSelf)
        {
            transform.position = transform.parent.GetChild(0).position;
            transform.rotation = transform.parent.GetChild(0).rotation;
        }
        else
        {
            transform.position = transform.parent.GetChild(1).position;
            transform.rotation = transform.parent.GetChild(1).rotation;
        }

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
