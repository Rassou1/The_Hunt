using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//THIS HAS JUST BEEN STRAIGHT UP COPIED FROM THE PREY STATEMANAGER, SAME FOR THE OTHER HUNTER SCRIPTS.
//It's just kinda placeholder for now

public class H_StateManager : MonoBehaviour
{
    //I'm using "_" for every variable that's declared in the class and not using it for the ones declared in methods. Should make it easier to see which one belongs where at a glance. Please follow this convention to the best of your abilities.
    PlayerInput _playerInput;
    CharacterController _characterController;
    Animator _animator;

    int _isWalkingHash;
    int _isSprintingHash;
    int _isFallingHash;
    int _isWallRunningHash;
    int _isSlidingHash;

    H_BaseState _currentState;
    H_StateFactory _states;

    //Variables to store player inputs
    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    Vector3 _currentSprintMovement;
    Vector3 _appliedMovement;

    bool _isMovementPressed;
    bool _isSprintPressed;
    bool _isJumpPressed;
    bool _isSlidePressed;

    float _gravity = -12f;
    float _groundedGravity = -.05f;

    float _rotationFactorPerFrame = 10f;
    float _sprintMultiplier = 3f;

    //Put a lot of getters and setters here
    public H_BaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public CharacterController CharacterController { get { return _characterController; } }
    public Animator Animator { get { return _animator; } }
    public int IsWalkingHash { get { return _isWalkingHash; } }
    public int IsSprintingHash { get { return _isSprintingHash; } }
    public int IsFallingHash { get { return _isFallingHash; } }
    public int IsWallRunningHash { get { return _isWallRunningHash; } }
    public int IsSlidingHash { get { return _isSlidingHash; } }



    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } set { _currentMovementInput = value; } }
    public Vector3 CurrentMovement { get { return _currentMovement; } set { _currentMovement = value; } }
    public Vector3 CurrentSprintMovement { get { return _currentSprintMovement; } set { _currentSprintMovement = value; } }
    public Vector3 AppliedMovement { get { return _appliedMovement; } set { _appliedMovement = value; } }

    public float CurrentMovementY { get { return _currentMovement.y; } set { _currentMovement.y = value; } }
    public float AppliedMovementY { get { return _appliedMovement.y; } set { _appliedMovement.y = value; } }
    public float AppliedMovementX { get { return _appliedMovement.x; } set { _appliedMovement.x = value; } }
    public float AppliedMovementZ { get { return _appliedMovement.z; } set { _appliedMovement.z = value; } }

    public bool IsMovementPressed { get { return _isMovementPressed; } }
    public bool IsSprintPressed { get { return _isSprintPressed; } }
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    public bool IsSlidePressed { get { return _isSlidePressed; } }

    public float Gravity { get { return _gravity; } set { _gravity = value; } }
    public float GroundedGravity { get { return _groundedGravity; } set { _groundedGravity = value; } }


    //void Start()
    //{

    //}

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _isWalkingHash = Animator.StringToHash("isWalking");
        _isSprintingHash = Animator.StringToHash("isRunning");
        _isFallingHash = Animator.StringToHash("isFalling");
        _isWallRunningHash = Animator.StringToHash("isWallRunning");
        _isSlidingHash = Animator.StringToHash("isSliding");

        //This get's the inputs from the new input system
        _playerInput.HunterControls.Move.started += OnMovementInput;
        _playerInput.HunterControls.Move.canceled += OnMovementInput;
        _playerInput.HunterControls.Move.performed += OnMovementInput; //This allows the game to realize we might be holding two buttons at once (based). It also allows for controler inputs (cringe)
        _playerInput.HunterControls.Sprint.started += OnSprint;
        _playerInput.HunterControls.Sprint.canceled += OnSprint;
        _playerInput.HunterControls.Jump.started += OnJump;
        _playerInput.HunterControls.Jump.canceled += OnJump;
        _playerInput.HunterControls.Slide.started += OnSlide;
        _playerInput.HunterControls.Slide.canceled += OnSlide;


        //setup state
        _states = new H_StateFactory(this);

        _currentState = _states.Ground();
        _currentState.EnterState();

    }

    void Update()
    {
        HandleRotation();
        _characterController.Move(_appliedMovement * Time.deltaTime);
        _currentState.UpdateStates();
    }


    public void GetCameraInput()
    {

    }

    void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = _currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = _currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (_isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationFactorPerFrame * Time.deltaTime);
        }

    }

    void OnJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
    }

    void OnSprint(InputAction.CallbackContext context)
    {
        _isSprintPressed = context.ReadValueAsButton();
    }

    void OnSlide(InputAction.CallbackContext context)
    {
        _isSlidePressed = context.ReadValueAsButton();
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.z = _currentMovementInput.y;
        _currentSprintMovement.x = _currentMovementInput.x * _sprintMultiplier;
        _currentSprintMovement.z = _currentMovementInput.y * _sprintMultiplier;  //We set z=y here since we're getting a Vector2 as the input and z is sideways in Vector3
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }

    //Use this as a cooldown for the mechanic of not losing momentum for a little bit when first entering a wallrun
    IEnumerator WallRunBuffer()
    {
        yield return new WaitForSeconds(2f);
        //getNoMomentumLoss = true
    }


    void CalculateMomentumGrounded()
    {
        //Switch name to just "CalculateMomentum" if we remove CalculateMomentumAir
    }

    void CalculateMomentumAir()
    {
        //If we want to calculate momentum in the air in an entierly different way. If we're just switching the values of some numbers we can remove this and just change the numbers using the H_InAirState script
    }


    void OnEnable()
    {
        _playerInput.HunterControls.Enable();
    }

    void OnDisable()
    {
        _playerInput.HunterControls.Disable();
    }



    public void SwitchState(H_BaseState state)
    {
        _currentState = state;
        state.EnterState();
    }
}
