using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Vivox;
using UnityEngine;

public class VivoxTest : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        await VivoxService.Instance.InitializeAsync();
        await VivoxService.Instance.LoginAsync();
        //await VivoxService.Instance.JoinEchoChannelAsync("EchoTest", ChatCapability.TextAndAudio);
        //await VivoxService.Instance.JoinGroupChannelAsync("ChannelName", ChatCapability.AudioOnly);
        Channel3DProperties props = new Channel3DProperties();
        await VivoxService.Instance.JoinPositionalChannelAsync("ChannelName",ChatCapability.AudioOnly,props);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
