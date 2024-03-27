using UnityEngine;

public class H_WallClimbingState : H_BaseState
{
    public H_WallClimbingState(H_StateManager currentContext, H_StateFactory h_StateFactory) : base(currentContext, h_StateFactory)
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