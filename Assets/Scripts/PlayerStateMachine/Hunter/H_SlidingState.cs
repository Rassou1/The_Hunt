using UnityEngine;

public class H_SlidingState : H_BaseState
{

    //Make sliding into a sub-sub state, since it's just a modifier on how you lose and gain momentum

    public H_SlidingState(H_StateManager currentContext, H_StateFactory h_StateFactory) : base(currentContext, h_StateFactory)
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