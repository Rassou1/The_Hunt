using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script makes sure animations are synced up between players by invoking methods remotely which updates the bools assigned to each player globally
//It is called in both the state manager and the states in the player scripts - Love

public class P_Animations : AttributesSync
{

    public Animator _animator;
    

    private void Start()
    {
        //if (!gameObject.GetComponent<Alteruna.Avatar>().IsMe)
        //{
        //    enabled = false;
        //    return;
        //}
    }


    public void SetWalking(bool setBool)
    {
        InvokeRemoteMethod("WalkingRemote", UserId.All, setBool);
    }

    public void SetRunning(bool setBool)
    {
        InvokeRemoteMethod("RunningRemote", UserId.All, setBool);
    }

    public void SetFalling(bool setBool)
    {
        InvokeRemoteMethod("FallingRemote", UserId.All, setBool);
    }

    public void SetSliding(bool setBool)
    {
        InvokeRemoteMethod("SlidingRemote", UserId.All, setBool);
    }



    [SynchronizableMethod]
    public void WalkingRemote(bool setBool)
    {
        _animator.SetBool("isWalking", setBool);
    }

    [SynchronizableMethod]
    public void RunningRemote(bool setBool)
    {
        _animator.SetBool("isRunning", setBool);
    }

    [SynchronizableMethod]
    public void FallingRemote(bool setBool)
    {
        _animator.SetBool("isFalling", setBool);
    }

    [SynchronizableMethod]
    public void SlidingRemote(bool setBool)
    {
        _animator.SetBool("isSliding", setBool);
    }


}
