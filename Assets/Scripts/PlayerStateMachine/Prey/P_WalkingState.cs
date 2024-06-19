using System.Xml.Serialization;
using UnityEngine;

//Walking state - Love
public class P_WalkingState : P_BaseState
{
    float lerpTime;
    public P_WalkingState(P_StateManager currentContext, P_StateFactory p_StateFactory) : base(currentContext, p_StateFactory)
    {

    }

    public override void EnterState()
    {
        //_ctx.Animator.SetBool("isWalking", true);
        //_ctx.Animator.SetBool("isRunning", false);

        _ctx.RemoteAnimator.SetWalking(true);
        _ctx.RemoteAnimator.SetRunning(false);

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
        if (_ctx.IsSlidePressed && _currentSuperState != _factory.Ghost())
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