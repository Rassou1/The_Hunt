using Alteruna;
using UnityEngine;

public abstract class P_BaseState 
{
    private bool _isRootState = false;
    protected P_StateManager _ctx;
    protected P_StateFactory _factory;
    protected P_BaseState _currentSubState;
    private P_BaseState _currentSuperState;

    protected bool hasDoubleJumped;
    public bool HasDoubleJumped { get { return hasDoubleJumped; } set { hasDoubleJumped = value; } }
    protected bool IsRootState { set { _isRootState = value; } }
    //protected P_StateManager Ctx { get { return _ctx; } }
    //protected P_StateFactory Factory { get { return _factory; } }

    public P_BaseState(P_StateManager currentContext, P_StateFactory p_StateFactory)
    {
        _ctx = currentContext;
        _factory = p_StateFactory;
        
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchState();

    public abstract void InitializeSubState();

    public void UpdateStates()
    {
        UpdateState();
        if (_currentSubState != null)
        {
            _currentSubState.UpdateStates();
        }
    }

    protected void SwitchState(P_BaseState newState)
    {
        //Current state exits state
        ExitState();

        //New state enters state
        newState.EnterState();

        if (_isRootState)
        {
            //Switch current state of context
            _ctx.CurrentState = newState;
        }
        else if (_currentSuperState != null)
        {
            //Set the current super states sub state to the new state
            _currentSuperState.SetSubState(newState);
        }

    }

    protected void SetSuperState(P_BaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(P_BaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }

    //public void ExitStates()
    //{
    //    ExitState();
    //    if (_currentSubState != null)
    //    {
    //        _currentSubState.ExitStates();
    //    }
    //}
}