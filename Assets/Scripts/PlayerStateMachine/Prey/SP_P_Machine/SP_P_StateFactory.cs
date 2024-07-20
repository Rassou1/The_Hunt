using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//This is called the state "factory" but operates like a library. Used to work like a factory but by the time I changed how it worked the name factory was used in so many places it wasn't worth changing the name - Love

//Enum for keeping the states in - Love
enum SP_P_States
{
    ground,
    air,
    idle,
    walk,
    run,
    slide,
    ghost
}

public class SP_P_StateFactory
{
    SP_P_StateMachine _context;
    Dictionary<SP_P_States, SP_P_BaseState> _states = new Dictionary<SP_P_States, SP_P_BaseState>();

    //currentContext is the state manager - Love
    public SP_P_StateFactory(SP_P_StateMachine currentContext)
    {
        _context = currentContext;
        //Initialize the states here - Love
        _states[SP_P_States.ground] = new SP_P_GroundedState(_context, this);
        _states[SP_P_States.air] = new SP_P_InAirState(_context, this);
        _states[SP_P_States.idle] = new SP_P_IdleState(_context, this);
        _states[SP_P_States.walk] = new SP_P_WalkingState(_context, this);
        _states[SP_P_States.run] = new SP_P_RunningState(_context, this);
        _states[SP_P_States.slide] = new SP_P_SlidingState(_context, this);
        _states[SP_P_States.ghost] = new SP_P_GhostState(_context, this);

    }

    //Return state here when called - Love
    public SP_P_BaseState Ground()
    {
        return _states[SP_P_States.ground];
    }

    public SP_P_BaseState Air()
    {
        return _states[SP_P_States.air];
    }

    public SP_P_BaseState Idle()
    {
        return _states[SP_P_States.idle];
    }

    public SP_P_BaseState Walk()
    {
        return _states[SP_P_States.walk];
    }

    public SP_P_BaseState Run()
    {
        return _states[SP_P_States.run];
    }

    public SP_P_BaseState Slide()
    {
        return _states[SP_P_States.slide];
    }

    public SP_P_BaseState Ghost()
    {
        return _states[SP_P_States.ghost];
    }

}