using System;
using UnityEngine;

//Root-state for being in the air - Love
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
        //_ctx.Animator.SetBool("isFalling", true);
        _ctx.RemoteAnimator.SetFalling(true);
        _ctx.ArmsAnimator.SetBool("isFalling", true);
        hasDoubleJumped = false;
        buttonReleased = false;
        direction = _ctx.PreCollideMovement;
        direction += new Vector3(_ctx.CurrentMovementInput.x, 0, _ctx.CurrentMovementInput.y) * 0.5f;

        _ctx.StateDirection = direction;
        _ctx.VertMagnitude -= 18f * Time.deltaTime;
    }

    //Kinda sloppy implementation of the double jump, but it works. Make sure to not allow double jump until player has let go of jump to avoid instantly double jumping from not pressing jump only one frame - Love
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

        //Setting the vert speed instead of adding to it, makes it more rewarding to be patient with the double jump - Love
        if (!hasDoubleJumped && _ctx.IsJumpPressed && buttonReleased)
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
        if (_ctx.Ghost)
        {
            SwitchState(_factory.Ghost());
        }
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

}