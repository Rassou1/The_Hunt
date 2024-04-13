using UnityEngine;



public class P_ClimbingState : P_BaseState
{
    float lerpTime;
    //private P_StateManager stateManager;

    public P_ClimbingState(P_StateManager stateManager, P_StateFactory p_StateFactory) : base(stateManager, p_StateFactory)
    {
        this._ctx = stateManager;
       
    }

   

    public override void EnterState()
    {
        _ctx.Animator.SetBool(_ctx.IsClimbingHash, true);
        lerpTime = 0f;
       
       
    }

    public override void UpdateState()
    {
        
        if (Input.GetKey(KeyCode.E)) 
        {
            float verticalInput = 1f; 
            Vector3 move = Vector3.up * verticalInput * _ctx.climbspeed * Time.deltaTime;
            _ctx.Rigidbody.MovePosition(_ctx.transform.position + move);

            _ctx.StateMagnitude = Mathf.Lerp(_ctx.ActualMagnitude, _ctx.climbspeed, lerpTime);
            lerpTime += Time.deltaTime;
        }
        else
        {
            CheckSwitchState();
        }
    }

    public override void ExitState()
    {
        _ctx.Animator.SetBool(_ctx.IsClimbingHash, false);
       
    }

    public override void CheckSwitchState()
    {
        
        if (_ctx.IsClimbingPressed) 
        {
            SwitchState(_factory.Idle()); 
        }
        
    }

    public override void InitializeSubState()
    {
        
    }
}


