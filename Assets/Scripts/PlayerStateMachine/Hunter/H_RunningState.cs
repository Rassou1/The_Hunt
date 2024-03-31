using UnityEngine;

public class H_RunningState : H_BaseState
{
    public H_RunningState(H_StateManager currentContext, H_StateFactory h_StateFactory) : base(currentContext, h_StateFactory)
    {

    }
    public override void EnterState()
    {
        _ctx.Animator.SetBool(_ctx.IsWalkingHash, true);
        _ctx.Animator.SetBool(_ctx.IsSprintingHash, true);
    }

    public override void UpdateState()
    {
        _ctx.AppliedMovementX = _ctx.CurrentMovementInput.x * 4f; //Switch the float out for some momentum math. Probably have the math be done in StateManager and then applied in the correct state, with each state changing the numbers used in StateManager
        _ctx.AppliedMovementZ = _ctx.CurrentMovementInput.y * 4f;
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
        else if (_ctx.IsMovementPressed && !_ctx.IsSprintPressed)
        {
            SwitchState(_factory.Walk());
        }
    }

    public override void InitializeSubState()
    {
        //if (slide) -> slide
    }
}