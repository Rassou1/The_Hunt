using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

enum H_States
{
    ground,
    air,
    idle,
    walk,
    run
}

public class H_StateFactory
{
    H_StateManager H_context;
    Dictionary<H_States, H_BaseState> H_states = new Dictionary<H_States, H_BaseState>();

    public H_StateFactory(H_StateManager currentContext)
    {
        H_context = currentContext;
        H_states[H_States.ground] = new H_GroundedState(H_context, this);
        H_states[H_States.air] = new H_InAirState(H_context, this);
        H_states[H_States.idle] = new H_IdleState(H_context, this);
        H_states[H_States.walk] = new H_WalkingState(H_context, this);
        H_states[H_States.run] = new H_RunningState(H_context, this);
    }

    public H_BaseState Ground()
    {
        return H_states[H_States.ground];
    }

    public H_BaseState Air()
    {
        return H_states[H_States.air];
    }

    public H_BaseState Idle()
    {
        return H_states[H_States.idle];
    }

    public H_BaseState Walk()
    {
        return H_states[H_States.walk];
    }

    public H_BaseState Run()
    {
        return H_states[H_States.run];
    }
    
}