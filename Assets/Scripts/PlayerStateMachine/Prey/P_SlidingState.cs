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
        _ctx.SubStateDirModifier = new Vector3(0.5f, 1, 1);
    }

    public override void UpdateState()
    {
        totalMagnitude = _ctx.ActualMagnitude;
        if (!_ctx.IsGrounded)
        {
            totalMagnitude += Mathf.Abs(_ctx.VertMagnitude) * Time.deltaTime;
        }

        float slideResult;
        if (_ctx.SlopeAngle <= 0)
        {
            _ctx.StateMagnitude = totalMagnitude + (_ctx.SlopeAngle - _ctx._slideResistance * 1.5f) * Time.deltaTime;
            slideResult = _ctx.SlopeAngle - _ctx._slideResistance * 2;
        }
        else
        {
            _ctx.StateMagnitude = Mathf.Min(totalMagnitude + (_ctx.SlopeAngle - _ctx._slideResistance) * Time.deltaTime, Mathf.Max(_ctx.SlopeAngle * 0.8f, 18f));
            slideResult = _ctx.SlopeAngle - _ctx._slideResistance;
        }


        //_ctx.StateMagnitude = Mathf.Clamp(_ctx.ActualMagnitude + (_ctx.SlopeAngle - _ctx._slideResistance) * Time.deltaTime, 0f, Mathf.Max(_ctx.SlopeAngle * 0.5f, 10f));
        
        //if (_ctx.StateMagnitude <= 0.01f)
        //{
        //    _ctx.StateMagnitude = 0f;
        //}
        Debug.Log("Slide result: " + slideResult);
        CheckSwitchState();
    }

    public override void ExitState()
    {
        //IgnoreCollision(this, hunter, false)
        _ctx.SubStateDirModifier = new Vector3(1, 1, 1);
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