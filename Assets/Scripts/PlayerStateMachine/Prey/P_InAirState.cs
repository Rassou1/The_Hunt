using System;
using UnityEngine;

public class P_InAirState : P_BaseState
{

    Vector3 direction;
    bool buttonReleased;

    

    public P_InAirState(P_StateManager currentContext, P_StateFactory p_StateFactory) : base(currentContext, p_StateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
        _ctx.Animator.SetFalling(true);
        hasDoubleJumped = false;
        buttonReleased = false;
        direction = _ctx.PreCollideMovement;
        direction += new Vector3(_ctx.CurrentMovementInput.x, 0, _ctx.CurrentMovementInput.y) * 0.5f;

        _ctx.StateDirection = direction;
        _ctx.VertMagnitude -= 18f * Time.deltaTime;
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        direction = _ctx.StateDirection;
        direction += new Vector3(_ctx.CurrentMovementInput.x, 0, _ctx.CurrentMovementInput.y) * 0.7f;

        _ctx.StateDirection = direction;
        _ctx.VertMagnitude -= 18f * Time.deltaTime;
        _ctx.VertMagnitude = Mathf.Clamp(_ctx.VertMagnitude, -40, 40);
        if (!_ctx.IsJumpPressed)
        {
            buttonReleased = true;
        }

        if (!hasDoubleJumped && _ctx.IsJumpPressed && buttonReleased && _currentSubState != _factory.Slide())
        {
            _ctx.VertMagnitude = 7.5f;
            hasDoubleJumped = true;
        }
    }

    public override void ExitState()
    {
        
    }

    public override void CheckSwitchState()
    {
        if (_ctx.IsGrounded)
        {
            SwitchState(_factory.Ground());
        }
    }

    public override void InitializeSubState()
    {
        if (_ctx.IsSlidePressed)
        {
            SetSubState(_factory.Slide());
        }
        else if (_ctx.IsMovementPressed && !_ctx.IsSprintPressed)
        {
            SetSubState(_factory.Walk());
        }
        else if (_ctx.IsMovementPressed && _ctx.IsSprintPressed)
        {
            SetSubState(_factory.Run());
        }
        else
        {
            SetSubState(_factory.Idle());
        }
    }

    //void HandleGravity()
    //{
    //    bool goingDown = _ctx.CurrentMovementY <= 2.0f || !_ctx.IsJumpPressed;
    //    float fallMultiplier = 2f;

    //    if (goingDown)
    //    {
    //        float previousYVelocity = _ctx.CurrentMovementY;
    //        _ctx.CurrentMovementY = _ctx.CurrentMovementY + (_ctx.Gravity * fallMultiplier * Time.deltaTime);
    //        _ctx.AppliedMovementY = Mathf.Max((previousYVelocity + _ctx.CurrentMovementY) * .5f, -10.0f);
    //    }
    //    else
    //    {
    //        float previousYVelocity = _ctx.CurrentMovementY;
    //        _ctx.CurrentMovementY = _ctx.CurrentMovementY + (_ctx.Gravity * Time.deltaTime);
    //        _ctx.AppliedMovementY = (previousYVelocity + _ctx.CurrentMovementY) * .5f;
    //    }
    //}
}