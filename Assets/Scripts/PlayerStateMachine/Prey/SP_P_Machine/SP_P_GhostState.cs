using UnityEngine;

//This state was added last minute as a debug tool as well as something to make it easier for me to record footage for the trailer - Love
public class SP_P_GhostState : SP_P_BaseState
{
    Vector3 direction;

    public SP_P_GhostState(SP_P_StateMachine currentContext, SP_P_StateFactory sp_p_StateFactory) : base(currentContext, sp_p_StateFactory)
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

    //UpdateState here get's the movement input from wasd in state manager. Jump and slide is now used for going up and down.
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

    //Only want one of run, walk and idle to be initialized when entering ghost state - Love
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

    //Only switch when the state manager bool "Ghost" is flipped to false - Love
    public override void CheckSwitchState()
    {
        if (!_ctx.Ghost)
        {
            SwitchState(_factory.Ground());
        }
    }

}
