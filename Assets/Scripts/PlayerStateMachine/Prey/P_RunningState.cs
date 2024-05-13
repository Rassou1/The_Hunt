using UnityEngine;

public class P_RunningState : P_BaseState
{
    float totalMagnitude;
    float sprintMagnitude;
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
        totalMagnitude = _ctx.ActualMagnitude;
        if (!_ctx.IsGrounded)
        {
            totalMagnitude += Mathf.Abs(_ctx.VertMagnitude * 0.4f) * Time.deltaTime;
        }

        if (_ctx.SlopeAngle >= 0)
        {
            sprintMagnitude = totalMagnitude + _ctx.SlopeAngle - _ctx._sprintResistance - (_ctx._sprintResistance * totalMagnitude * 0.5f);
            _ctx.StateMagnitude += sprintMagnitude * Time.deltaTime;
            _ctx.StateMagnitude = Mathf.Max(_ctx.StateMagnitude, _ctx._softCap);
        }
        else
        {
            sprintMagnitude = totalMagnitude - _ctx.SlopeAngle + _ctx._sprintResistance;
            _ctx.StateMagnitude += sprintMagnitude * Time.deltaTime;
            _ctx.StateMagnitude = Mathf.Min(_ctx.StateMagnitude, _ctx._softCap);
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