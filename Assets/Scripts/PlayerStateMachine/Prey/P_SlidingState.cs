using UnityEngine;

public class P_SlidingState : P_BaseState
{

    

    public P_SlidingState(P_StateManager currentContext, P_StateFactory p_StateFactory) : base(currentContext, p_StateFactory)
    {

    }

    public override void EnterState()
    {
        //IgnoreCollision(this, hunter, true)
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void ExitState()
    {
        //IgnoreCollision(this, hunter, false)
    }

    public override void CheckSwitchState()
    {
        
    }

    public override void InitializeSubState()
    {

    }
}