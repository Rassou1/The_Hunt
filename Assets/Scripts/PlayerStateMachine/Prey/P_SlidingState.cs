using UnityEngine;

public class P_SlidingState : P_BaseState
{

    float totalMagnitude;

    public P_SlidingState(P_StateManager currentContext, P_StateFactory p_StateFactory) : base(currentContext, p_StateFactory)
    {

    }

    public override void EnterState()
    {
        //IgnoreCollision(this, hunter, true)
        _ctx.SubStateDirSet = new Vector3(0, 0, 2);
        _ctx.HorMouseMod = 0.2f;
    }

    public override void UpdateState()
    {
        totalMagnitude = _ctx.ActualMagnitude;

        
        if (_ctx.SlopeAngle < 0)
        {
            _ctx.StateMagnitude = totalMagnitude + (_ctx.SlopeAngle - _ctx._slideResistance - (_ctx._slideResistance * totalMagnitude * 0.2f)) * Time.deltaTime;
        }
        else if (_ctx.SlopeAngle > 0)
        {
            _ctx.StateMagnitude = totalMagnitude + (_ctx.SlopeAngle - _ctx._slideResistance - (_ctx._slideResistance * totalMagnitude * 0.2f)) * Time.deltaTime;
        }
        else
        {
            if(totalMagnitude > 0)
            {
                _ctx.StateMagnitude = totalMagnitude - (_ctx._slideResistance + _ctx._slideResistance * totalMagnitude * 0.2f) * Time.deltaTime;
            }
            else
            {
                _ctx.StateMagnitude = totalMagnitude + Mathf.Abs((_ctx._slideResistance - _ctx._slideResistance * totalMagnitude * 0.2f) * Time.deltaTime);
            }
            
        }


        

        //Vector3.Dot(_ctx.RelForward, _ctx.AppliedMovement) >= 0

        //_ctx.StateMagnitude = Mathf.Clamp(_ctx.ActualMagnitude + (_ctx.SlopeAngle - _ctx._slideResistance) * Time.deltaTime, 0f, Mathf.Max(_ctx.SlopeAngle * 0.5f, 10f));

        //if (_ctx.StateMagnitude <= 0.01f)
        //{
        //    _ctx.StateMagnitude = 0f;
        //}
        
        CheckSwitchState();
    }

    public override void ExitState()
    {
        //IgnoreCollision(this, hunter, false)
        _ctx.SubStateDirSet = new Vector3(0, 0, 0);
        _ctx.HorMouseMod = 1f;
    }

    public override void CheckSwitchState()
    {
        if (!_ctx.IsSlidePressed)
        {
            if (!_ctx.IsMovementPressed)
            {
                SwitchState(_factory.Idle());
            }
            else if (_ctx.IsMovementPressed && !_ctx.IsSprintPressed)
            {
                SwitchState(_factory.Walk());
            }
            else if (_ctx.IsMovementPressed && _ctx.IsSprintPressed)
            {
                SwitchState(_factory.Run());
            }
        }
    }

    public override void InitializeSubState()
    {

    }
}