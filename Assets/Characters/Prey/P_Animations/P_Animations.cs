using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Animations : AttributesSync
{

    public Animator _animator;
   // public Animator _armsAnimator;

    public void SetWalking(bool setBool)
    {
        _animator.SetBool("isWalking", setBool);
        //_armsAnimator.SetBool("isWalking", setBool);
    }

    public void SetRunning(bool setBool)
    {
        _animator.SetBool("isRunning", setBool);
        //_armsAnimator.SetBool("isRunning", setBool);
    }

    public void SetFalling(bool setBool)
    {
        _animator.SetBool("isFalling", setBool);
        //_armsAnimator.SetBool("isFalling", setBool);    
    }

    public void SetSliding(bool setBool)
    {
        _animator.SetBool("isSliding", setBool);
        //_armsAnimator.SetBool("isSliding", setBool);
    }

}
