using System.Xml.Serialization;
using UnityEngine;

//Walking state - Love
public class SP_H_WalkingState : SP_H_BaseState
{
    float lerpTime;
    public SP_H_WalkingState(SP_H_StateManager currentContext, SP_H_StateFactory p_StateFactory) : base(currentContext, p_StateFactory)
    {

    }

    public override void EnterState()
    {
        _ctx.ArmsAnimator.SetBool("isWalking", true);
        _ctx.ArmsAnimator.SetBool("isRunning", false);
        lerpTime = 0f;
    }

    //Lerp to walking speed for a smoother transition - Love
    public override void UpdateState()
    {
        
        _ctx.StateMagnitude = Mathf.Lerp(_ctx.ActualMagnitude, _ctx._moveSpeed, lerpTime);
        lerpTime += Time.deltaTime;

        CheckSwitchState();
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchState()
    {
        if (_ctx.IsSlidePressed)
        {
            SwitchState(_factory.Slide());
        }
        if (!_ctx.IsMovementPressed)
        {
            SwitchState(_factory.Idle());
        }
        else if (_ctx.IsMovementPressed && _ctx.IsSprintPressed)
        {
            SwitchState(_factory.Run());
        }
        

    }

    public override void InitializeSubState()
    {
        
    }
}