using System.Collections;
using UnityEngine;

public class P_SlidingState : P_BaseState
{
    public float momentumMultiplierMax = 10f;
    //PLACEHOLDER MAXIMUM POSSIBLE multiplier  for the momentum gain and loss.
    public float momentumFadeTimer = 3f;
    //PLACEHOLDER Time taken for momentum multiplier to fade down to 1.
    public Vector3 inputDirection;
    //PLACEHOLDER direction of input during slide.
    public Vector3 movementDir;
    //PLACEHOLDER direction of current movement.
    public bool fading;
    //if the momentum mult should fade or not. MAYBE NEEDS TO BE ACCESSIBLE ELSEWHERE TO MAKE TRUE!
    public float momentumMultiplierCurrent = 2f;
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
        momentumMultiplierCurrent = momentumMultiplierMax;

        //_ctx._rigidbody.AddForce(new Vector3(_ctx.CurrentMovementInput.x,0,_ctx.CurrentMovementInput.y).normalized * 5, ForceMode.Force);
    }

    public override void UpdateState()
    {
        multiplierFade();
        //_ctx.AppliedMovementX = _ctx.CurrentMovementInput.x * momentumMultiplierCurrent * _ctx._moveSpeed;
        //_ctx.AppliedMovementZ = _ctx.CurrentMovementInput.y * momentumMultiplierCurrent * _ctx._moveSpeed;
        CheckSwitchState();
    }

    public override void ExitState()
    {
        fading = false;
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

        momentumMultiplierCurrent = Mathf.Lerp(momentumMultiplierCurrent, 3f, 0.01f);

        _ctx.AppliedMovementX = _ctx.CurrentMovementInput.x * momentumMultiplierCurrent * _ctx._moveSpeed;
        _ctx.AppliedMovementZ = _ctx.CurrentMovementInput.y * momentumMultiplierCurrent * _ctx._moveSpeed;

        if (momentumMultiplierCurrent <= 3)
        {
            momentumMultiplierCurrent = 3;   
        }
    }


}