using UnityEngine;

//The sliding substate is the most complicated one, but it's just some addition and subrtraction at the end of the day - Love
public class SP_P_SlidingState : SP_P_BaseState
{

    float totalMagnitude;
    float stateMag;

    public SP_P_SlidingState(SP_P_StateMachine currentContext, SP_P_StateFactory sp_p_StateFactory) : base(currentContext, sp_p_StateFactory)
    {

    }

    //Change the position of the camera as well as the size of the collider when entering and exiting the state - Love
    //Also make the horizontal mouse sensitivity lower to make the slide feel less maneuverable, also creates a forward direction modifier to the root state that gives your sideways inputs less impact for the same reason - Love
    public override void EnterState()
    {
        _ctx._cameraPostion.transform.position -= new Vector3(0, 0.7f, 0);
        _ctx.CapsuleColliderHeight -= 0.8f;
        
        _ctx.SubStateDirSet = new Vector3(0, 0, 2);
        _ctx.HorMouseMod = 0.4f;

        //_ctx.Animator.SetBool("isSliding", true);
        
        _ctx.ArmsAnimator.SetBool("isSliding", true);
    }

    public override void UpdateState()
    {
        //Get current speed from state manager - Love
        totalMagnitude = _ctx.ActualMagnitude;
        //When on a downwards slope gain extra speed and when on a upwards slope loose it. Used to have slightly diffferent math for both but they're now the same - Love
        if (_ctx.SlopeAngle < 0)
        {
            stateMag = totalMagnitude + (_ctx.SlopeAngle - _ctx._slideResistance - (_ctx._slideResistance * totalMagnitude * 0.1f)) * Time.deltaTime;
        }
        else if (_ctx.SlopeAngle > 0)
        {
            stateMag = totalMagnitude + (_ctx.SlopeAngle - _ctx._slideResistance - (_ctx._slideResistance * totalMagnitude * 0.1f)) * Time.deltaTime;
        }
        else //When on flat ground the math works slightly differently - Love
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

        //Clamp speed to -40 and 40 to not get too absurd since too high speeds start creating edge case bugs - Love
        _ctx.StateMagnitude = Mathf.Clamp(stateMag, -40f, 40f);
        

        
        CheckSwitchState();
    }

    public override void ExitState()
    {
        _ctx._cameraPostion.transform.position += new Vector3(0, 0.7f, 0);
        _ctx.CapsuleColliderHeight += 0.8f;
        _ctx.SubStateDirSet = new Vector3(0, 0, 0);
        _ctx.HorMouseMod = 1f;

        //_ctx.Animator.SetBool("isSliding", false);
        
        _ctx.ArmsAnimator.SetBool("isSliding", false);
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