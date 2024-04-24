using System.Xml.Serialization;
using UnityEngine;

public class P_WalkingState : P_BaseState
{
    float lerpTime;
    public P_WalkingState(P_StateManager currentContext, P_StateFactory p_StateFactory, SCR_abilityManager scr_pow) : base(currentContext, p_StateFactory, scr_pow)
    {

    }

    public override void EnterState()
    {
        _ctx.Animator.SetBool(_ctx.IsWalkingHash, true);
        _ctx.Animator.SetBool(_ctx.IsSprintingHash, false);
        lerpTime = 0f;
    }

    public override void UpdateState()
    {
        
        _ctx.StateMagnitude = Mathf.Lerp(_ctx.ActualMagnitude, _ctx._moveSpeed, lerpTime);
        lerpTime += Time.deltaTime;

        CheckSwitchState();
        _pow.CheckDash(ref _ctx);
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchState()
    {
        //if (_ctx.IsSlidePressed)
        //{
        //    SwitchState(_factory.Slide());
        //}
        if (!_ctx.IsMovementPressed)
        {
            SwitchState(_factory.Idle());
        }
        
        

    }

    public override void InitializeSubState()
    {
        
    }

}