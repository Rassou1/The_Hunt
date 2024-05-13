using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

enum H_States
{
    ground,
    air,
    idle,
    walk,
    run,
    slide
}

public class H_StateFactory
{
    H_StateManager _context;
    Dictionary<H_States, H_BaseState> _states = new Dictionary<H_States, H_BaseState>();

    public H_StateFactory(H_StateManager currentContext)
    {
        _context = currentContext;
        _states[H_States.ground] = new H_GroundedState(_context, this);
        _states[H_States.air] = new H_InAirState(_context, this);
        _states[H_States.idle] = new H_IdleState(_context, this);
        _states[H_States.walk] = new H_WalkingState(_context, this);
        _states[H_States.run] = new H_RunningState(_context, this);
        _states[H_States.slide] = new H_SlidingState(_context, this);
        
    }

    public H_BaseState Ground()
    {
        return _states[H_States.ground];
    }

    public H_BaseState Air()
    {
        return _states[H_States.air];
    }

    public H_BaseState Idle()
    {
        return _states[H_States.idle];
    }

    public H_BaseState Walk()
    {
        return _states[H_States.walk];
    }

    public H_BaseState Run()
    {
        return _states[H_States.run];
    }

    public H_BaseState Slide()
    {
        return _states[H_States.slide];
    }
    
}