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
        if(_ctx.SlopeAngle >= 0)
        {
            _ctx.StateMagnitude = Mathf.Max((_ctx.SlopeAngle * 0.125f) - _ctx._sprintResistance, 0f) + _ctx._softCap;
        }
        else
        {
            _ctx.StateMagnitude = Mathf.Min((_ctx.SlopeAngle * 0.125f) + _ctx._sprintResistance, 0f) + _ctx._softCap;
        }
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