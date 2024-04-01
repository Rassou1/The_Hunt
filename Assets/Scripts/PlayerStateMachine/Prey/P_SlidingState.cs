using UnityEngine;

public class P_SlidingState : P_BaseState
{

    //Make sliding into a sub-sub state, since it's just a modifier on how you lose and gain momentum

    public P_SlidingState(P_StateManager currentContext, P_StateFactory p_StateFactory) : base(currentContext, p_StateFactory)
    {

    }

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchState()
    {
        
    }

    public override void InitializeSubState()
    {

    }
}