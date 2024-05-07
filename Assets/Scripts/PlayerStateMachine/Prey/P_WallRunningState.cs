using UnityEngine;

public class P_WallRunningState : P_BaseState
{
    public P_WallRunningState(P_StateManager currentContext, P_StateFactory p_StateFactory, SCR_abilityManager scr_pow) : base(currentContext, p_StateFactory, scr_pow)
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