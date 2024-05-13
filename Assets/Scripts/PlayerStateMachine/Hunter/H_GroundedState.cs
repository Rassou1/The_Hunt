    using UnityEngine;

public class H_GroundedState : H_BaseState
{
    public H_GroundedState(H_StateManager currentContext, H_StateFactory h_StateFactory) : base(currentContext, h_StateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
        _ctx.CurrentMovementY = _ctx.GroundedGravity;
        _ctx.AppliedMovementY = _ctx.GroundedGravity;
        _ctx.Animator.SetBool(_ctx.IsFallingHash, false);
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
        if (_ctx.IsMovementPressed && !_ctx.IsSprintPressed)
        {
            SetSubState(_factory.Walk());
        }
        else if (_ctx.IsMovementPressed && _ctx.IsSprintPressed)
        {
            SetSubState(_factory.Run());
        }
        //else if (wallrunning) -> wallrun
        else
        {
            SetSubState(_factory.Idle());
        }

    }

    public override void CheckSwitchState()
    {
        if (_ctx.IsJumpPressed)
        {
            _ctx.CurrentMovementY += 10;
            _ctx.AppliedMovementY += 10;
            SwitchState(_factory.Air());
        }
        else if (!_ctx.CharacterController.isGrounded)
        {
            SwitchState(_factory.Air());
        }

    }

}
