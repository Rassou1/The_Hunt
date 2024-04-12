using Alteruna;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VivoxTest : MonoBehaviour
{
    Multiplayer multiplayer;
    // Start is called before the first frame update
    async void Start()
    {
        multiplayer = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<Multiplayer>();
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        await VivoxService.Instance.InitializeAsync();
        VivoxService.Instance.LoggedIn += UserLoggedIn;
        multiplayer.OnRoomJoined.AddListener(LogInToVivox);

        //await VivoxService.Instance.JoinEchoChannelAsync("EchoTest", ChatCapability.TextAndAudio);
        //await VivoxService.Instance.JoinGroupChannelAsync("ChannelName", ChatCapability.AudioOnly);
        //Channel3DProperties props = new Channel3DProperties();
        //await VivoxService.Instance.JoinPositionalChannelAsync("ChannelName",ChatCapability.AudioOnly,props);
    }

    async void LogInToVivox(Multiplayer arg0,Room arg1,User arg2)
    {
        await VivoxService.Instance.LoginAsync();
    }

    void UserLoggedIn()
    {
        JoinChannelAsync(multiplayer.CurrentRoom.Name);
    }

    async void JoinChannelAsync(string name)
    {
        Channel3DProperties props = new Channel3DProperties();
        await VivoxService.Instance.JoinPositionalChannelAsync(name,ChatCapability.AudioOnly,props);
        Debug.LogError("Joined Channel: " + name);
    }

    // Update is called once per frame
    void Update()
    {
        
       

    }
}
