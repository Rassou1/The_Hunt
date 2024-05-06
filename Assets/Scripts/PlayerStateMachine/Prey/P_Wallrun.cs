using UnityEngine;

public class P_WallrunState : P_BaseState
{

    Vector3 direction;
    bool rightWall;
    //where the wall is. true is right, false is left.
    Vector3 wallMagnet = new Vector3(1,0,0);
    //the force that forces the player to stick to the wall while wallrunning.

    public P_WallrunState(P_StateManager currentContext, P_StateFactory p_StateFactory) : base(currentContext, p_StateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
        rightWall = _ctx.RightWall;
        _ctx.Animator.SetBool(_ctx.IsFallingHash, false);
        _ctx.Animator.SetBool(_ctx.IsWallRunningHash, true);
        //MAKE A WALLRUN HASH AND ADD IT HERE, SET TO TRUE!!
        _ctx.VertMagnitude = 0f;
        _ctx.ActualMagnitude += 3f;
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        direction = _ctx.SubStateDirSet;
        direction += new Vector3(_ctx.CurrentMovementInput.x, 0, _ctx.CurrentMovementInput.y);
        _ctx.AppliedMovementY = 0;
        _ctx.StateDirection = _ctx.AlignToSlope(direction);
        if(rightWall)
        {
            _ctx.Rigidbody.AddForce(wallMagnet, ForceMode.Force);
        }
        else if (!rightWall)
        {
            _ctx.Rigidbody.AddForce(wallMagnet * -1f, ForceMode.Force);
        }
    }

    public override void ExitState()
    {

    }


    public override void InitializeSubState()
    {
        if(!_ctx.IsMovementPressed)
        {
            _ctx.VertMagnitude = 0f;
            SwitchState(_factory.Air());
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
