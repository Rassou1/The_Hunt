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

    async void LogInToVivox(Multiplayer arg0,Room arg1,User arg2)
    {
        if (!VivoxService.Instance.IsLoggedIn)
        {
            await VivoxService.Instance.LoginAsync();
        }
        
    }

    void UserLoggedIn()
    {
        JoinChannelAsync(multiplayer.CurrentRoom.Name);
    }

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
        if (Input.GetKeyDown(KeyCode.P) && VivoxService.Instance.IsLoggedIn)
        {
            if (!VivoxService.Instance.IsInputDeviceMuted)
            {
                VivoxService.Instance.MuteOutputDevice();
                VivoxService.Instance.MuteInputDevice();
                Debug.Log("Muting Player");
                if (muteicon != null)
                {
                    muteicon.gameObject.active = true;
                }
            }
            else
            {
                VivoxService.Instance.UnmuteInputDevice();
                VivoxService.Instance.UnmuteOutputDevice();
                Debug.Log("Unmuting Player");
                if (muteicon != null)
                {
                    muteicon.gameObject.active = false;
                }
            }

        }
    }
}
