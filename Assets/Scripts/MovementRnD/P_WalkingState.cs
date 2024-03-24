using System.Xml.Serialization;
using UnityEngine;

public class P_WalkingState : P_BaseState
{
    public P_WalkingState(P_StateManager currentContext, P_StateFactory p_StateFactory) : base(currentContext, p_StateFactory)
    {

    }

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        //Set movement to input


        CheckSwitchState(); //This should always be last
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
