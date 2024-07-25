using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//This is called the state "factory" but operates like a library. Used to work like a factory but by the time I changed how it worked the name factory was used in so many places it wasn't worth changing the name - Love

//Enum for keeping the states in - Love
enum SP_H_States
{
    ground,
    air,
    idle,
    walk,
    run,
    slide
}

public class SP_H_StateFactory
{
    SP_H_StateManager _context;
    
    Dictionary<SP_H_States, SP_H_BaseState> _states = new Dictionary<SP_H_States, SP_H_BaseState>();

    //currentContext is the state manager - Love
    public SP_H_StateFactory(SP_H_StateManager currentContext)
    {
        _context = currentContext;
        //Initialize the states here - Love
        _states[SP_H_States.ground] = new SP_H_GroundedState(_context, this);
        _states[SP_H_States.air] = new SP_H_InAirState(_context, this);
        _states[SP_H_States.idle] = new SP_H_IdleState(_context, this);
        _states[SP_H_States.walk] = new SP_H_WalkingState(_context, this);
        _states[SP_H_States.run] = new SP_H_RunningState(_context, this);
        _states[SP_H_States.slide] = new SP_H_SlidingState(_context, this);
        
    }

    //Return state here when called - Love
    public SP_H_BaseState Ground()
    {
        return _states[SP_H_States.ground];
    }

    public SP_H_BaseState Air()
    {
        return _states[SP_H_States.air];
    }

    public SP_H_BaseState Idle()
    {
        return _states[SP_H_States.idle];
    }

    public SP_H_BaseState Walk()
    {
        return _states[SP_H_States.walk];
    }

    public SP_H_BaseState Run()
    {
        return _states[SP_H_States.run];
    }

    public SP_H_BaseState Slide()
    {
        return _states[SP_H_States.slide];
    }
    
}