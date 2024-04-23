using UnityEngine;

public class P_GroundedState : P_BaseState
{
    public P_GroundedState(P_StateManager currentContext, P_StateFactory p_StateFactory) : base(currentContext, p_StateFactory)
    {
        IsRootState = true;
        
    }

    public override void EnterState()
    {
        InitializeSubState();
        //_ctx.VertMagnitude = -2f;
        _ctx.Animator.SetBool(_ctx.IsFallingHash, false);
    }

    public override void UpdateState()
    {
        _ctx.StateDirection = _ctx.SubStateDirSet;
        _ctx.StateDirection += new Vector3(_ctx.CurrentMovementInput.x, 0, _ctx.CurrentMovementInput.y);
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
