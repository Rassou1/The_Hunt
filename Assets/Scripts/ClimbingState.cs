using Unity.VisualScripting;
using UnityEngine;



public class P_ClimbingState : P_BaseState
{
    float lerpTime;
    //private P_StateManager stateManager;
    public CheckLadderCollision cl = new CheckLadderCollision();

    public P_ClimbingState(P_StateManager stateManager, P_StateFactory p_StateFactory) : base(stateManager, p_StateFactory)
    {
        IsRootState = true;
        
    }



    public override void EnterState()
    {
        //_ctx.Animator.SetBool(_ctx.IsClimbingHash, true);
        _ctx.Rigidbody.useGravity = false; 
        //_ctx.Rigidbody.velocity = Vector3.zero; // Mevcut hızı sıfırla
    }

    //CheckLadderCollision ladderCollision = new CheckLadderCollision();
    public override void UpdateState()
    {
        if (cl.isTouchingLadder)
        {
            Vector3 climbVelocity = Vector3.up * 5f; // Örneğin saniyede 5 birim yukarı
            _ctx.Rigidbody.MovePosition(_ctx.Rigidbody.position + climbVelocity * Time.deltaTime);
            Debug.Log("Climbing... Current position: " + _ctx.Rigidbody.position);
            CheckSwitchState();
            //Vector3 climbDirection = Vector3.up * 5f; // Tırmanma hızını ayarlayın
            //_ctx.Rigidbody.MovePosition(_ctx.Rigidbody.position + climbDirection * Time.deltaTime);
            //Vector3 climbDirection = Vector3.up * 5f; // Yukarı yön, hızı ayarlayabilirsiniz.
            //_ctx.StateDirection = climbDirection;

            //// Rigidbody'i kullanarak karakterin pozisyonunu güncelleyin
            //_ctx.Rigidbody.MovePosition(_ctx.Rigidbody.position + _ctx.StateDirection * Time.deltaTime);
            //Debug.Log("Is in climb: " + climbDirection);
            //Debug.Log("gravity: " + _ctx.Gravity);
            //CheckSwitchState();
            ////_ctx.StateDirection = new Vector3(0, 5f, 0).normalized;
            ///
            Debug.Log("Is in climb" + _ctx.StateDirection);
            Debug.Log("gravity" + _ctx.Rigidbody.useGravity);
            //CheckSwitchState();
            //if (_ctx._IsClimbPressed)
            //{
            //    ExitState();
            //}
    }


}
    public override void ExitState()
    {
        _ctx.Animator.SetBool(_ctx.IsClimbingHash, false);
        _ctx.Rigidbody.useGravity = true;
        //Debug.Log("workk");
    }

    public override void CheckSwitchState()
    {
        
        

    }

    public override void InitializeSubState()
    {
        if (_ctx.IsSlidePressed)
        {
            SetSubState(_factory.Slide());
        }
        else if (_ctx.IsMovementPressed && !_ctx.IsSprintPressed)
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
}


