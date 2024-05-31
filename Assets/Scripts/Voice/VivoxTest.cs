using Alteruna;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Vivox;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VivoxTest : MonoBehaviour
{
    // This script starts up the Vivox Voicechat -Jonathan
    Multiplayer multiplayer;
    string currentVoicechatLobby;
    public Image muteicon;
    async void Start()
    {
        multiplayer = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<Multiplayer>();
        if (VivoxService.Instance == null)
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            await VivoxService.Instance.InitializeAsync();
            VivoxService.Instance.LoggedIn += UserLoggedIn;
            multiplayer.OnRoomJoined.AddListener(LogInToVivox);
            multiplayer.OnRoomLeft.AddListener(LeftRoom);
        }
    }

    /// <summary>
    /// This Method is triggerd when the player has joined a Alteruna Room
    /// </summary>
    /// <param name="arg0">The Multiplayer Component from Alteruna</param>
    /// <param name="arg1">The Room Component from Alteruna</param>
    /// <param name="arg2">The User Component from Alteruna</param>
    async void LogInToVivox(Multiplayer arg0,Room arg1,User arg2)
    {
        if (!VivoxService.Instance.IsLoggedIn)
        {
            await VivoxService.Instance.LoginAsync();
        }
        
    }

    /// <summary>
    /// This Method is triggerd when Vixox is finished logging in the player to Unity player accounts
    /// </summary>
    void UserLoggedIn()
    {
        JoinChannelAsync(multiplayer.CurrentRoom.Name);
    }

    /// <summary>
    /// Joins or creates a vivox voice chat server.
    /// </summary>
    /// <param name="name">The name of the channel that the user is going to join</param>
    async void JoinChannelAsync(string name)
    {
        VivoxService.Instance.MuteOutputDevice();
        VivoxService.Instance.MuteInputDevice();
        Channel3DProperties props = new Channel3DProperties(32,10,1.0f, AudioFadeModel.ExponentialByDistance);
        await VivoxService.Instance.JoinPositionalChannelAsync(name,ChatCapability.AudioOnly,props);
        //await VivoxService.Instance.JoinGroupChannelAsync(name, ChatCapability.AudioOnly);
        //await VivoxService.Instance.JoinEchoChannelAsync(name, ChatCapability.AudioOnly);
        currentVoicechatLobby = name;
        Debug.Log("Joined Channel: " + name);
        
    }

    async void LeftRoom(Multiplayer arg0)
    {
        await VivoxService.Instance.LeaveChannelAsync(currentVoicechatLobby);
        VivoxService.Instance.LogoutAsync();
    }

    private void Update()
    {
        //All of this is just too toggle the mute function -Jonathan
        if (VivoxService.Instance.IsLoggedIn)
        {
            if (!VivoxService.Instance.IsInputDeviceMuted)
            {
                if (muteicon != null)
                {
                    muteicon.gameObject.active = false;
                }
            }
            else
            {
                if (muteicon != null)
                {
                    muteicon.gameObject.active = true;
                }
            }


            if (Input.GetKeyDown(KeyCode.P))
            {
                if (!VivoxService.Instance.IsInputDeviceMuted)
                {
                    VivoxService.Instance.MuteOutputDevice();
                    VivoxService.Instance.MuteInputDevice();
                    Debug.Log("Muting Player");
                }
                else
                {
                    VivoxService.Instance.UnmuteInputDevice();
                    VivoxService.Instance.UnmuteOutputDevice();
                    Debug.Log("Unmuting Player");
                }
            }
        }
        
    }
}
