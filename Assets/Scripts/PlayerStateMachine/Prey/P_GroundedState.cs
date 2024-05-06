using UnityEngine;

public class P_GroundedState : P_BaseState
{
    Vector3 direction;
    
    public P_GroundedState(P_StateManager currentContext, P_StateFactory p_StateFactory, SCR_abilityManager scr_pow) : base(currentContext, p_StateFactory, scr_pow)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
        _ctx.Animator.SetBool(_ctx.IsFallingHash, false);
        _ctx.ActualMagnitude += Mathf.Abs(_ctx.VertMagnitude);
        _ctx.VertMagnitude = -0.1f;
        
        _ctx.Pow.AB_jumpsLeft = _ctx.Pow.AB_jumpsTotal;
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        direction = _ctx.SubStateDirSet;
        direction += new Vector3(_ctx.CurrentMovementInput.x, 0, _ctx.CurrentMovementInput.y);
        _ctx.StateDirection = _ctx.AlignToSlope(direction);
    }

    public override void ExitState()
    {

    }


    public override void InitializeSubState()
    {
        if (_ctx.IsSlidePressed)
        {
            SetSubState(_factory.Slide());
        }
        else if(_ctx.IsMovementPressed && _ctx.IsSprintPressed)
        {
            SetSubState(_factory.Run());
        }
        else if (_ctx.IsMovementPressed && !_ctx.IsSprintPressed)
        {
            SetSubState(_factory.Walk());
        }
        else
        {
            SetSubState(_factory.Idle());
        }

    }

    public override void CheckSwitchState()
    {
        if (_ctx.IsJumpPressed)
        {
            _ctx.VertMagnitude = 5f;
            _ctx.IsGrounded = false;
            SwitchState(_factory.Air());
        }
        else if (!_ctx.IsGrounded)
        {
            SwitchState(_factory.Air());
        }
        
    }

}
