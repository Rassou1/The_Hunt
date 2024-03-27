using UnityEngine;

public class P_InAirState : P_BaseState
{
    public P_InAirState(P_StateManager currentContext, P_StateFactory p_StateFactory) : base(currentContext, p_StateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchState()
    {
        CheckSwitchState();
    }

    public override void InitializeSubState()
    {
        //if(!anyMovementPressed)
        //SetSubState(_factory.Idle());
        //else if(wasd && !leftshift)
        //SetSubState(_factory.Walk());
        //else
        //SetSubState(_factory.Run());
        //etc
    }
}