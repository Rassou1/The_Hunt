using UnityEngine;

public class P_GhostState : P_BaseState
{
    Vector3 direction;

    public P_GhostState(P_StateManager currentContext, P_StateFactory p_StateFactory) : base(currentContext, p_StateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
        //_ctx.Animator.SetFalling(false);
        _ctx.ActualMagnitude = _ctx.AppliedMovement.magnitude / Time.deltaTime;

        _ctx.VertMagnitude = 0f;

    }

    public override void UpdateState()
    {
        CheckSwitchState();
        direction = new Vector3(_ctx.CurrentMovementInput.x, 0, _ctx.CurrentMovementInput.y);
        _ctx.StateDirection = direction;
        if (_ctx.IsJumpPressed)
        {
            _ctx.VertMagnitude = 2;
        }
        else if (_ctx.IsSlidePressed)
        {
            _ctx.VertMagnitude = -2;
        }
        else
        {
            _ctx.VertMagnitude = 0;
        }
    }

    public override void ExitState()
    {

    }


    public override void InitializeSubState()
    {
        if (_ctx.IsMovementPressed && _ctx.IsSprintPressed)
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
        if (!_ctx.Ghost)
        {
            SwitchState(_factory.Ground());
        }
    }

}
