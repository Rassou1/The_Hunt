using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//This is called the state "factory" but operates like a library. Used to work like a factory but by the time I changed how it worked the name factory was used in so many places it wasn't worth changing the name - Love

//Enum for keeping the states in - Love
enum P_States
{
    ground,
    air,
    idle,
    walk,
    run,
    slide,
    ghost
}

public class P_StateFactory
{
    P_StateManager _context;
    Dictionary<P_States, P_BaseState> _states = new Dictionary<P_States, P_BaseState>();

    //currentContext is the state manager - Love
    public P_StateFactory(P_StateManager currentContext)
    {
        _context = currentContext;
        //Initialize the states here - Love
        _states[P_States.ground] = new P_GroundedState(_context, this);
        _states[P_States.air] = new P_InAirState(_context, this);
        _states[P_States.idle] = new P_IdleState(_context, this);
        _states[P_States.walk] = new P_WalkingState(_context, this);
        _states[P_States.run] = new P_RunningState(_context, this);
        _states[P_States.slide] = new P_SlidingState(_context, this);
        _states[P_States.ghost] = new P_GhostState(_context, this);

    }

    //Return state here when called - Love
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

    public P_BaseState Run()
    {
        return _states[P_States.run];
    }

    public P_BaseState Slide()
    {
        return _states[P_States.slide];
    }

    public P_BaseState Ghost()
    {
        return _states[P_States.ghost];
    }

}