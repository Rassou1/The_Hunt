using UnityEngine;

//The idle state, not a lot happens here naturally - Love
public class P_IdleState : P_BaseState
{
    float lerpTime;
    public P_IdleState(P_StateManager currentContext, P_StateFactory p_StateFactory) : base(currentContext, p_StateFactory)
    {

    }

    public override void EnterState()
    {
        //_ctx.Animator.SetBool("isWalking", false);
        //_ctx.Animator.SetBool("isRunning", false);

        _ctx.RemoteAnimator.SetWalking(false);
        _ctx.RemoteAnimator.SetRunning(false);

        _ctx.ArmsAnimator.SetBool("isWalking", false);
        _ctx.ArmsAnimator.SetBool("isRunning", false);
        lerpTime = 0f;
    }


    //Quickly lerp to 0 speed just to not make going from high speed to nothing too instant. - Love
    public override void UpdateState()
    {
        _ctx.StateMagnitude = Mathf.Lerp(_ctx.ActualMagnitude, 0, lerpTime);
        lerpTime += Time.deltaTime;
        CheckSwitchState();
    }

    public override void ExitState()
    {
        
    }

    public override void CheckSwitchState()
    {
        if (_ctx.IsSlidePressed && _currentSuperState != _factory.Ghost())
        {
            SwitchState(_factory.Slide());
        }
        else if (_ctx.IsMovementPressed)
        {
            SwitchState(_factory.Walk());
        }
    }

    public override void InitializeSubState()
    {
        
    }
}