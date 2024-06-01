using UnityEngine;

//Substate for running/sprinting - Love
public class H_RunningState : H_BaseState
{
    float totalMagnitude;
    float sprintMagnitude;
    public H_RunningState(H_StateManager currentContext, H_StateFactory p_StateFactory) : base(currentContext, p_StateFactory)
    {

    }
    public override void EnterState()
    {
        //_ctx.Animator.SetWalking(true);
        //_ctx.Animator.SetRunning(true);
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