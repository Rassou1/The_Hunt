using UnityEngine;

public class H_GroundedState : H_BaseState
{

    Vector3 direction;
    
    public H_GroundedState(H_StateManager currentContext, H_StateFactory h_StateFactory) : base(currentContext, h_StateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
        _ctx.Animator.SetBool(_ctx.IsFallingHash, false);
        _ctx.ActualMagnitude += Mathf.Abs(_ctx.VertMagnitude);
        _ctx.VertMagnitude = -0.1f;
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
       
        if (_ctx.IsMovementPressed && !_ctx.IsSprintPressed)
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
            _ctx.VertMagnitude = 7f;
            _ctx.IsGrounded = false;
            SwitchState(_factory.Air());
        }
        else if (!_ctx.IsGrounded)
        {
            SwitchState(_factory.Air());
        }
        
    }

}
