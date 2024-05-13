using UnityEngine;

public class H_SlidingState : H_BaseState
{

    float totalMagnitude;
    float stateMag;

    public H_SlidingState(H_StateManager currentContext, H_StateFactory p_StateFactory) : base(currentContext, p_StateFactory)
    {

    }

    public override void EnterState()
    {
        _ctx._cameraPostion.transform.position -= new Vector3(0, 0.9f, 0);
        _ctx.CapsuleColliderHeight -= 1f;
        
        //IgnoreCollision(this, hunter, true)
        _ctx.SubStateDirSet = new Vector3(0, 0, 2);
        _ctx.HorMouseMod = 0.2f;
        _ctx.Animator.SetBool(_ctx.IsSlidingHash, true);
    }

    public override void UpdateState()
    {
        totalMagnitude = _ctx.ActualMagnitude;
        if (_ctx.SlopeAngle < 0)
        {
            stateMag = totalMagnitude + (_ctx.SlopeAngle - _ctx._slideResistance - (_ctx._slideResistance * totalMagnitude * 0.2f)) * Time.deltaTime;
        }
        else if (_ctx.SlopeAngle > 0)
        {
            stateMag = totalMagnitude + (_ctx.SlopeAngle - _ctx._slideResistance - (_ctx._slideResistance * totalMagnitude * 0.2f)) * Time.deltaTime;
        }
        else
        {
            if(totalMagnitude > 0)
            {
                stateMag = totalMagnitude - (_ctx._slideResistance + _ctx._slideResistance * totalMagnitude * 0.2f) * Time.deltaTime;
            }
            else
            {
                stateMag = totalMagnitude + Mathf.Abs((_ctx._slideResistance - _ctx._slideResistance * totalMagnitude * 0.2f) * Time.deltaTime);
            }
            
        }

        _ctx.StateMagnitude = Mathf.Clamp(stateMag, -40f, 40f);
        

        //Vector3.Dot(_ctx.RelForward, _ctx.AppliedMovement) >= 0

        //_ctx.StateMagnitude = Mathf.Clamp(_ctx.ActualMagnitude + (_ctx.SlopeAngle - _ctx._slideResistance) * Time.deltaTime, 0f, Mathf.Max(_ctx.SlopeAngle * 0.5f, 10f));

        //if (_ctx.StateMagnitude <= 0.01f)
        //{
        //    _ctx.StateMagnitude = 0f;
        //}
        
        CheckSwitchState();
    }

    public override void ExitState()
    {
        //IgnoreCollision(this, hunter, false)
        _ctx._cameraPostion.transform.position += new Vector3(0, 0.9f, 0);
        _ctx.CapsuleColliderHeight += 1f;
        
        _ctx.SubStateDirSet = new Vector3(0, 0, 0);
        _ctx.HorMouseMod = 1f;
        _ctx.Animator.SetBool(_ctx.IsSlidingHash, false);
    }

    public override void CheckSwitchState()
    {
        if (!_ctx.IsSlidePressed)
        {
            if (!_ctx.IsMovementPressed)
            {
                SwitchState(_factory.Idle());
            }
            else if (_ctx.IsMovementPressed && _ctx.IsSprintPressed)
            {
                SwitchState(_factory.Run());
            }
            else if (_ctx.IsMovementPressed && !_ctx.IsSprintPressed)
            {
                SwitchState(_factory.Walk());
            }
            
        }
    }

    public override void InitializeSubState()
    {

    }
}