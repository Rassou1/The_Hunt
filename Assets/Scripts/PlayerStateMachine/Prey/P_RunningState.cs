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
        _ctx.Animator.SetBool("isWalking", true);
        _ctx.Animator.SetBool("isRunning", true);
        _ctx.AnimatorArms.SetBool("isWalking", true);
        _ctx.AnimatorArms.SetBool("isRunning", true);
    }

    public override void UpdateState()
    {
        totalMagnitude = _ctx.ActualMagnitude;
        //Gain extra speed when falling - Love
        if (!_ctx.IsGrounded)
        {
            totalMagnitude += Mathf.Abs(_ctx.VertMagnitude * 0.4f) * Time.deltaTime;
        }

        //when sprinting up a slope your max speed is capped at the "soft cap" - Love
        if (_ctx.SlopeAngle >= 0)
        {
            sprintMagnitude = totalMagnitude + _ctx.SlopeAngle - _ctx._sprintResistance - (_ctx._sprintResistance * totalMagnitude * 0.5f);
            _ctx.StateMagnitude += sprintMagnitude * Time.deltaTime;
            _ctx.StateMagnitude = Mathf.Max(_ctx.StateMagnitude, _ctx._softCap);
        }
        else //When sprinting down a slope your minimum speed is capped at the "soft cap". Go faster when going down steep hills. - Love
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
        if (_ctx.IsSlidePressed && _currentSuperState != _factory.Ghost())
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