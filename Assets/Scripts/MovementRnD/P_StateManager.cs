using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_StateManager : MonoBehaviour
{
    //I'm using "_" for every variable that's declared in the class and not using it for the ones declared in methods. Should make it easier to see which one belongs where at a glance. Please follow this convention to the best of your abilities.
    PlayerInput _playerInput;
    CharacterController _characterController;
    Animator _animator;

    int _isWalkingHash;
    int _isSprintingHash;
    int _isFallingHash;

    P_BaseState _currentState;
    P_StateFactory _states;

    //Variables to store player inputs
    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    Vector3 _currentSprintMovement;
    bool _isMovementPressed;
    bool _isSprintPressed;

    bool _isJumpPressed = false;

    float _rotationFactorPerFrame = 10f;
    float _sprintMultiplier = 3f;

    //Put a lot of getters and setters here
    public P_BaseState CurrentState { get { return _currentState; } set { _currentState = value; } }



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

        _playerInput.CharacterControls.Move.started += OnMovementInput;
        _playerInput.CharacterControls.Move.canceled += OnMovementInput;
        _playerInput.CharacterControls.Move.performed += OnMovementInput; //This allows the game to realize we might be holding two buttons at once (based). It also allows for controler inputs (cringe)
        _playerInput.CharacterControls.Sprint.started += OnSprint;
        _playerInput.CharacterControls.Sprint.canceled += OnSprint;
        _playerInput.CharacterControls.Jump.started += OnJump;
        _playerInput.CharacterControls.Jump.canceled += OnJump;
        

        //setup state
        _states = new P_StateFactory(this);

        _currentState = _states.Ground();
        _currentState.EnterState();

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

    void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.z = _currentMovementInput.y;
        _currentSprintMovement.x = _currentMovementInput.x * _sprintMultiplier;
        _currentSprintMovement.z = _currentMovementInput.y * _sprintMultiplier;
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }

    void HandleAnimation()
    {
        bool isWalking = _animator.GetBool(_isWalkingHash);
        bool isSprinting = _animator.GetBool(_isSprintingHash);
        bool isFalling = _animator.GetBool(_isFallingHash);

        if (!_characterController.isGrounded && !isFalling)
        {
            _animator.SetBool(_isFallingHash, true);
        }
        else if(_characterController.isGrounded && isFalling)
        {
            _animator.SetBool(_isFallingHash, false);
        }

        if(_isMovementPressed && !isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
        }
        else if(!_isMovementPressed && isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }

        if(_isMovementPressed && _isSprintPressed && !isSprinting)
        {
            _animator.SetBool(_isSprintingHash, true);
        }
        else if((!_isMovementPressed || !_isSprintPressed) && isSprinting)
        {
            _animator.SetBool(_isSprintingHash, false);

        }

    }

    void HandleGravity()
    {
        if (_characterController.isGrounded)
        {
            float groundedGravity = -.05f;      //isGrounded only returns true if the character is touching the ground and has a downwards momentum
            _currentMovement.y = groundedGravity;
            _currentSprintMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            _currentMovement.y += gravity * Time.deltaTime;
            _currentSprintMovement.y += gravity * Time.deltaTime;
        }

        

    }

    void HandleJump()
    {
        if(_isJumpPressed && _characterController.isGrounded)
        {
            _currentMovement.y = 4;
        }
    }

    void Update()
    {
        HandleRotation();
        HandleAnimation();
        HandleJump();

        if (_isSprintPressed)
        {
            _characterController.Move(_currentSprintMovement * Time.deltaTime);

        }
        else
        {
            _characterController.Move(_currentMovement * Time.deltaTime);
        }

        

        HandleGravity();
        _currentState.UpdateStates();
    }


    void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
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
