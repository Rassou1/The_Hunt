using System.Collections;
using UnityEngine;

public class P_SlidingState : P_BaseState
{
    public float momentumMultiplierMax = 3f;
    //PLACEHOLDER MAXIMUM POSSIBLE multiplier  for the momentum gain and loss.
    public float momentumFadeTimer = 5f;
    //PLACEHOLDER Time taken for momentum multiplier to fade down to 1.
    public Vector3 inputDirection;
    //PLACEHOLDER direction of input during slide.
    public Vector3 movementDir;
    //PLACEHOLDER direction of current movement.
    public bool fading;
    //if the momentum mult should fade or not. MAYBE NEEDS TO BE ACCESSIBLE ELSEWHERE TO MAKE TRUE!
    public float momentumMultiplierCurrent = 1f;
    //current multiplier on momentum.

    //Make sliding into a sub-sub state, since it's just a modifier on how you lose and gain momentum

    public P_SlidingState(P_StateManager currentContext, P_StateFactory p_StateFactory) : base(currentContext, p_StateFactory)
    {

    }

    public override void EnterState()
    {
        _ctx.Animator.SetBool(_ctx.IsWalkingHash, false);
        _ctx.Animator.SetBool(_ctx.IsSprintingHash, false);
        _ctx.Animator.SetBool(_ctx.IsSlidingHash, true);
        fading = true;

        //_ctx._rigidbody.AddForce(_ctx.CurrentMovementInput.normalized * 5, ForceMode.Impulse);
    }

    public override void UpdateState()
    {
        multiplierFade();
        if(_ctx.CurrentMovement != Vector3.zero)
        {
            _ctx.AppliedMovement = _ctx.AppliedMovement * momentumMultiplierCurrent;
        }
        CheckSwitchState();
    }

    public override void ExitState()
    {
        fading = false;
        _ctx.Animator.SetBool(_ctx.IsWalkingHash, true);
        _ctx.Animator.SetBool(_ctx.IsSprintingHash, true);
        _ctx.Animator.SetBool(_ctx.IsSlidingHash, false);
    }

    public override void CheckSwitchState()
    {
        if (!_ctx.IsSlidePressed && _ctx.IsSprintPressed)
        {
            SwitchState(_factory.Run());
        }
        else if (!_ctx.IsMovementPressed)
        {
            SwitchState(_factory.Idle());
        }
        else if (!_ctx.IsSlidePressed && _ctx.IsMovementPressed && !_ctx.IsSprintPressed)
        {
            SwitchState(_factory.Walk());
        }
    }

    public override void InitializeSubState()
    {

    }

    public void multiplierFade()
    {
        if (fading)
        {
            momentumMultiplierCurrent = Mathf.Lerp(momentumMultiplierMax, 1f, Time.deltaTime / momentumFadeTimer);
        }
        if(momentumMultiplierCurrent < 1f)
        {
            momentumMultiplierCurrent = 1f;
            fading = false;
        }
    }

}