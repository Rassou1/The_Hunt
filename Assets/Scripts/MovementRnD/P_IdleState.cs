using UnityEngine;

public class P_IdleState : P_BaseState
{
    public P_IdleState(P_StateManager currentContext, P_StateFactory p_StateFactory) : base(currentContext, p_StateFactory)
    {

    }

    public override void EnterState()
    {
        //Set the animator bools here

        //AppliedMovevement = 0
    }

    KeyCode forward;


    public override void UpdateState()
    {
        CheckSwitchState();
    }
   
    public override void ExitState()
    {

    }

    public override void CheckSwitchState()
    {
        //if(wasd && !leftshift)
        //SetSubState(_factory.Walk());
        //else
        //SetSubState(_factory.Run());
        //etc

        //Fix for other substates as well
    }

    public override void InitializeSubState()
    {
        
    }
}
