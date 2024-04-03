using UnityEngine;

public class P_RunningState : P_BaseState
{
    public P_RunningState(P_StateManager currentContext, P_StateFactory p_StateFactory) : base(currentContext, p_StateFactory)
    {

    }
    public override void EnterState()
    {
        _ctx.Animator.SetBool(_ctx.IsWalkingHash, true);
        _ctx.Animator.SetBool(_ctx.IsSprintingHash, true);
    }

    public override void UpdateState()
    {
        _ctx.AppliedMovementX = _ctx.CurrentMovementInput.x * _ctx._moveSpeed * _ctx._sprintMultiplier; //Switch the float out for some momentum math. Probably have the math be done in StateManager and then applied in the correct state, with each state changing the numbers used in StateManager
        _ctx.AppliedMovementZ = _ctx.CurrentMovementInput.y * _ctx._moveSpeed * _ctx._sprintMultiplier;
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
        else if (!_ctx.IsMovementPressed)
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
       
    }
}