using UnityEngine;

public class H_IdleState : H_BaseState
{
    float lerpTime;
    public H_IdleState(H_StateManager currentContext, H_StateFactory h_StateFactory) : base(currentContext, h_StateFactory)
    {

    }

    public override void EnterState()
    {
        _ctx.Animator.SetBool(_ctx.IsWalkingHash, false);
        _ctx.Animator.SetBool(_ctx.IsSprintingHash, false);
        lerpTime = 0f;
    }



    public override void UpdateState()
    {
        _ctx.StateMagnitude = Mathf.Lerp(_ctx.ActualMagnitude, 0, lerpTime);
        lerpTime += Time.deltaTime;
        CheckSwitchState();
    }

    public override void ExitState()
    {
        
    }

    public override void CheckSwitchState()
    {
       if (_ctx.IsMovementPressed && _ctx.IsSprintPressed)
        {
            SwitchState(_factory.Run());
        }
        else if (_ctx.IsMovementPressed)
        {
            SwitchState(_factory.Walk());
        }
    }

    public override void InitializeSubState()
    {
        
    }
}