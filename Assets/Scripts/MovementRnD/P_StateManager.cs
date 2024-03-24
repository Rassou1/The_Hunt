using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_StateManager : MonoBehaviour
{

    PlayerInput playerInput;

    P_BaseState _currentState;
    P_StateFactory _states;


    //Put a lot of getters and setters here
    public P_BaseState CurrentState { get { return _currentState; } set { _currentState = value; } }



    void Start()
    {
        
    }

    private void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.CharacterControls.Move.started += context => { Debug.Log(context.ReadValue<Vector2>()); };

        //setup state
        _states = new P_StateFactory(this);

        _currentState = _states.Ground();
        _currentState.EnterState();

    }

    void Update()
    {



        _currentState.UpdateStates();
    }


    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInput?.CharacterControls.Disable();
    }



    public void SwitchState(P_BaseState state)
    {
        _currentState = state;
        state.EnterState();
    }


    public void GetCameraInput()
    {

    }
}
