using UnityEngine;

public class P_OnWallState : P_BaseState
{

    public P_OnWallState(P_StateManager currentContext, P_StateFactory p_StateFactory, SCR_abilityManager scr_pow) : base(currentContext, p_StateFactory, scr_pow)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
        
        
    }

    public override void UpdateState()
    {
        
        CheckSwitchState();
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

    public override void CheckSwitchState()
    {
        if (_ctx.IsJumpPressed)
        {
            _ctx.VertMagnitude = 6f;
            SwitchState(_factory.Air());
        }
        else if (!_ctx.IsGrounded)
        {
            SwitchState(_factory.Air());
        }
        
    }

}
