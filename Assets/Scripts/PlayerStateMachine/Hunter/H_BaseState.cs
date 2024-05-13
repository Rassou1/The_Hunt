using UnityEngine;

public abstract class H_BaseState
{
    private bool _isRootState = false;
    protected H_StateManager _ctx;
    protected H_StateFactory _factory;
    protected H_BaseState _currentSubState;
    private H_BaseState _currentSuperState;

    protected bool hasDoubleJumped;
    public bool HasDoubleJumped { get { return hasDoubleJumped; } set { hasDoubleJumped = value; } }
    protected bool IsRootState { set { _isRootState = value; } }
    public H_BaseState CurrentSubState { get { return _currentSubState; } }
    //protected H_StateManager Ctx { get { return _ctx; } }
    //protected H_StateFactory Factory { get { return _factory; } }

    public H_BaseState(H_StateManager currentContext, H_StateFactory p_StateFactory)
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

    protected void SwitchState(H_BaseState newState)
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

    protected void SetSuperState(H_BaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(H_BaseState newSubState)
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