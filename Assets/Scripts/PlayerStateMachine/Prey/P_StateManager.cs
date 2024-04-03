using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_StateManager : MonoBehaviour
{
    //I'm using "_" for every variable that's declared in the class and not using it for the ones declared in methods. Should make it easier to see which one belongs where at a glance. Please follow this convention to the best of your abilities.
    PlayerInput _playerInput;
    
    
    CapsuleCollider _capsuleCollider;
    LayerMask whatIsGround;

    public float _mouseSens;
    
    

    int _isWalkingHash;
    int _isSprintingHash;
    int _isFallingHash;
    int _isWallRunningHash;
    int _isSlidingHash;

    P_BaseState _currentState;
    P_StateFactory _states;

    //Variables to store player inputs
    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    Vector3 _currentSprintMovement;
    Vector3 _appliedMovement;
    Vector2 _currentLookInput;

    public Rigidbody _rigidbody;
    public Transform _cameraOrientation;
    public Animator _animator;
    //Transform _thisCharacter;

    float _mouseRotationX;
    float _mouseRotationY;

    Vector3 _moveForward;
    Vector3 _moveRight;

    bool _isMovementPressed;
    bool _isSprintPressed;
    bool _isJumpPressed;
    bool _isSlidePressed;

    bool _isGrounded = false;
    bool _isStuck = false;
    bool _wallRight = false;
    bool _wallLeft = false;


    float _gravity = -8f;
    float _groundedGravity = -8f;

    public float _moveSpeed;
    public float _sprintMultiplier;
    

    //Put a lot of getters and setters here
    public P_BaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Rigidbody Rigidbody { get { return _rigidbody; } }
    public Animator Animator { get { return _animator; } }
    public int IsWalkingHash { get { return _isWalkingHash; } }
    public int IsSprintingHash { get { return _isSprintingHash; } }
    public int IsFallingHash { get { return _isFallingHash; } }
    public int IsWallRunningHash { get { return _isWallRunningHash; } }
    public int IsSlidingHash {  get { return _isSlidingHash; } }



    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } set { _currentMovementInput = value; } }
    public Vector3 CurrentMovement { get { return _currentMovement; } set { _currentMovement = value; } }
    public Vector3 CurrentSprintMovement { get { return _currentSprintMovement; } set { _currentSprintMovement = value; } }
    public Vector3 AppliedMovement {  get { return _appliedMovement; } set { _appliedMovement = value; } }

    public float CurrentMovementY { get { return _currentMovement.y; } set { _currentMovement.y = value; } }
    public float AppliedMovementY { get { return _appliedMovement.y; } set { _appliedMovement.y = value; } }
    public float AppliedMovementX { get { return _appliedMovement.x; } set {  _appliedMovement.x = value; } }
    public float AppliedMovementZ { get { return _appliedMovement.z; } set { _appliedMovement.z = value; } }
    
    

    public bool IsMovementPressed { get { return _isMovementPressed; } }
    public bool IsSprintPressed {  get { return _isSprintPressed; } }
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    public bool IsSlidePressed { get { return _isSlidePressed; } }

    public bool IsGrounded {  get { return _isGrounded; } }
    public float Gravity { get { return _gravity; } set { _gravity = value; } }
    public float GroundedGravity { get { return _groundedGravity; } set { _groundedGravity = value; } }

    //multiplayer stuff (ask tyron)
    //private Alteruna.Avatar _avatar;
    //void Start()
    //{
    //    //multiplayer stuff (ask tyron)
    //    _avatar = GetComponent<Alteruna.Avatar>();
    //    if (!_avatar.IsMe)
    //        return;
    //}




        private void Awake()
    {

        _playerInput = new PlayerInput();
        //_rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        //_animator = GetComponent<Animator>();


        _isWalkingHash = Animator.StringToHash("isWalking");
        _isSprintingHash = Animator.StringToHash("isRunning");
        _isFallingHash = Animator.StringToHash("isFalling");
        _isWallRunningHash = Animator.StringToHash("isWallRunning");
        _isSlidingHash = Animator.StringToHash("isSliding");

        //This gets the inputs from the new input system
        _playerInput.PreyControls.Move.started += OnMovementInput;
        _playerInput.PreyControls.Move.canceled += OnMovementInput;
        _playerInput.PreyControls.Move.performed += OnMovementInput; //This allows the game to realize we might be holding two buttons at once (based). It also allows for controler inputs (cringe)
        _playerInput.PreyControls.Sprint.started += OnSprint;
        _playerInput.PreyControls.Sprint.canceled += OnSprint;
        _playerInput.PreyControls.Jump.started += OnJump;
        _playerInput.PreyControls.Jump.canceled += OnJump;
        _playerInput.PreyControls.Slide.started += OnSlide;
        _playerInput.PreyControls.Slide.canceled += OnSlide;
        _playerInput.PreyControls.Look.started += OnLookInput;
        _playerInput.PreyControls.Look.canceled += OnLookInput;
        _playerInput.PreyControls.Look.performed += OnLookInput;


        //setup state
        _states = new P_StateFactory(this);

        _currentState = _states.Ground();
        _currentState.EnterState();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        //multiplayer stuff (ask tyron)
        //if (!_avatar.IsMe)
        //    return;

        _isGrounded = Physics.Raycast(_capsuleCollider.transform.position, Vector3.down, 0.3f);
        //GroundStuck();
        DetectWall();

        Debug.Log("Right Wall: " + _wallRight);
        Debug.Log("Left Wall: " + _wallLeft);

        _currentState.UpdateStates();
        SetCameraOrientation();
        RotateBodyY();
        RelativeMovement();
        _rigidbody.transform.position += _appliedMovement * Time.deltaTime;
    }

    void GroundStuck()
    {
        _isStuck = Physics.Raycast(_capsuleCollider.transform.position + new Vector3(0, 1, 0), Vector3.down, 1f);
        if (_isStuck)
        {
            _rigidbody.transform.position += new Vector3(0, 0.1f, 0);
        }
    }
    

    void DetectWall()
    {

        //The direction isn't correct yet, but it's getting there...
        _wallRight = Physics.CapsuleCast(_capsuleCollider.transform.position + new Vector3(0, .5f, 0), _capsuleCollider.transform.position + new Vector3(0, 2.5f, 0), 0.25f, Vector3.Scale(Vector3.right, new Vector3(_mouseRotationX, _mouseRotationY, 0f)), 0.3f);
        _wallLeft = Physics.CapsuleCast(_capsuleCollider.transform.position + new Vector3(0, .5f, 0), _capsuleCollider.transform.position + new Vector3(0, 2.5f, 0), 0.25f, Vector3.Scale(Vector3.left, new Vector3(_mouseRotationX, _mouseRotationY)), 0.3f);


    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _capsuleCollider.gameObject)
        {
            return;
        }
        _isGrounded = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _capsuleCollider.gameObject)
        {
            return;
        }
        _isGrounded = false;
    }

    void RelativeMovement()
    {
        float preRelativeY = _appliedMovement.y;
        
        _appliedMovement = _moveForward.normalized * _appliedMovement.z + _moveRight.normalized * _appliedMovement.x;
        _appliedMovement.y = preRelativeY;
        Debug.Log("applied movement final: " + _appliedMovement);
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
        _currentMovement.x = _currentMovementInput.x * _moveSpeed;
        _currentMovement.z = _currentMovementInput.y * _moveSpeed;
        _currentSprintMovement.x = _currentMovement.x * _sprintMultiplier;
        _currentSprintMovement.z = _currentMovement.y * _sprintMultiplier;  //We set z=y here since we're getting a Vector2 as the input and z is sideways in Vector3
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
        Debug.Log("Current movement in input: " + _currentMovement);
    }

    void OnLookInput(InputAction.CallbackContext context)
    {
        _currentLookInput = context.ReadValue<Vector2>();
        _mouseRotationX -= _currentLookInput.y * Time.deltaTime * _mouseSens;
        _mouseRotationY += _currentLookInput.x * Time.deltaTime * _mouseSens;
        _mouseRotationX = Mathf.Clamp(_mouseRotationX, -89f, 89f);
    }

    public void SetCameraOrientation()
    {
        _cameraOrientation.rotation = Quaternion.Euler(_mouseRotationX, _mouseRotationY, 0);
        _moveForward = _cameraOrientation.forward;
        _moveRight = _cameraOrientation.right;
        _moveForward.y = 0;
        _moveRight.y = 0;
    }

    void RotateBodyY()
    {
        //Will add some kind of "only rotate when angle above x or moving" if case when i understand Quaternions
        var forward = _cameraOrientation.forward;
        forward.y = 0;
        _rigidbody.transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }

    void CheckGrounded()
    {
        
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
        //If we want to calculate momentum in the air in an entierly different way. If we're just switching the values of some numbers we can remove this and just change the numbers using the P_InAirState script
    }


    void OnEnable()
    {
        _playerInput.PreyControls.Enable();
    }

    void OnDisable()
    {
        _playerInput.PreyControls.Disable();
    }



    public void SwitchState(P_BaseState state)
    {
        _currentState = state;
        state.EnterState();
    }
}
