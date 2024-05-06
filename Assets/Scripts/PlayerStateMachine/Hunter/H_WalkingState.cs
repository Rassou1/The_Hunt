using System.Xml.Serialization;
using UnityEngine;

public class H_WalkingState : H_BaseState
{
    float lerpTime;
    public H_WalkingState(H_StateManager currentContext, H_StateFactory h_StateFactory) : base(currentContext, h_StateFactory)
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
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchState()
    {
        
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