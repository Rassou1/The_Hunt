using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

enum P_States
{
    ground,
    air,
    idle,
    walk,
    slide,
    wallRun,
}

public class P_StateFactory
{
    P_StateManager _context;
    Dictionary<P_States, P_BaseState> _states = new Dictionary<P_States, P_BaseState>();

    public P_StateFactory(P_StateManager currentContext)
    {
        _context = currentContext;
        _states[P_States.ground] = new P_GroundedState(_context, this);
        _states[P_States.air] = new P_InAirState(_context, this);
        _states[P_States.idle] = new P_IdleState(_context, this);
        _states[P_States.walk] = new P_WalkingState(_context, this);
        _states[P_States.slide] = new P_SlidingState(_context, this);
        _states[P_States.wallRun] = new P_WallRunningState(_context, this);
    }

    public P_BaseState Ground()
    {
        return _states[P_States.ground];
    }

    public P_BaseState Air()
    {
        return _states[P_States.air];
    }

    public P_BaseState Idle()
    {
        return _states[P_States.idle];
    }

    public P_BaseState Walk()
    {
        return _states[P_States.walk];
    }

    

    public P_BaseState Slide()
    {
        return _states[P_States.slide];
    }

    public P_BaseState WallRun()
    {
        return _states[P_States.wallRun];
    }

    
}