using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_StateManager : MonoBehaviour
{
    //private Alteruna.Avatar _avatar;
    //I'm using "_" for every variable that's declared in the class and not using it for the ones declared in methods. Should make it easier to see which one belongs where at a glance. Please follow this convention to the best of your abilities.
    PlayerInput _playerInput;
    
    
    CapsuleCollider _capsuleCollider;
    Bounds _bounds;
    
    int _maxBounces = 5;
    float _skindWidth = 0.015f;

    LayerMask whatIsGround;



    public float _mouseSens;
    public float _maxSlopeAngle;
    

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

    //void Start()
    //{
    //    _avatar  = GetComponentInParent<Alteruna.Avatar>();

    //}




    private void Awake()
    {


        _playerInput = new PlayerInput();
        //_rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        //_animator = GetComponent<Animator>();
        
        _bounds = _capsuleCollider.bounds;
        _bounds.Expand(-2 * _skindWidth);


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
        //if (!_avatar.IsMe)
        //    return;

        
        

        //Debug.Log("Right Wall: " + _wallRight);
        //Debug.Log("Left Wall: " + _wallLeft);

        _currentState.UpdateStates();
        SetCameraOrientation();
        RotateBodyY();
        RelativeMovement();
        Debug.Log("applied movement pre: " + _appliedMovement);
        Debug.Log("IsGrounded: " + _isGrounded);
        _appliedMovement *= Time.deltaTime;
        _appliedMovement = CollideAndSlide(_appliedMovement, _capsuleCollider.transform.position, 0, false, _appliedMovement);
        _appliedMovement += CollideAndSlide(_appliedMovement, _capsuleCollider.transform.position + _appliedMovement, 0, true, new Vector3(0, _appliedMovement.y,0));
        _rigidbody.transform.position += _appliedMovement;
        //Debug.Log("applied movement final: " + _bounds.extents.x);
        _isGrounded = Physics.Raycast(_capsuleCollider.transform.position, Vector3.down, 0.02f);
    }


    //This function is based on this YT video https://www.youtube.com/watch?v=YR6Q7dUz2uk which in turn is based on this paper https://www.peroxide.dk/papers/collision/collision.pdf
    Vector3 CollideAndSlide(Vector3 vel, Vector3 startPos, int depth, bool gravityPass, Vector3 velInit)
    {
        Vector3 botSphere = startPos + new Vector3(0, _capsuleCollider.radius, 0);
        Vector3 topSphere = startPos + new Vector3(0, _capsuleCollider.height - _capsuleCollider.radius, 0);
        //Debug.Log("StartPos: " + startPos);
        //Debug.Log("BotSphere: " + botSphere);
        //Debug.Log("TopSphere: " + topSphere);
        if (depth >= _maxBounces)
        {
            return Vector3.zero;
        }
        float dist = vel.magnitude + _skindWidth;
        RaycastHit hit;
        //Debug.Log("Direction: " + vel.normalized);
        //Debug.Log("Distance: " + dist);
        if (Physics.CapsuleCast(botSphere, topSphere, _bounds.extents.x, vel.normalized, out hit, dist))
        {
            Vector3 snapToSurface = vel.normalized * (hit.distance - _skindWidth);
            Vector3 leftover = vel - snapToSurface;
            float angle = Vector3.Angle(Vector3.up, hit.normal);

            if (snapToSurface.magnitude <= _skindWidth)
            {
                snapToSurface = Vector3.zero;
            }

            if (angle <= _maxSlopeAngle) //Normal ground / slope
            {
                if (gravityPass)
                {
                    return snapToSurface;
                }
                leftover = ProjectAndScale(leftover, hit.normal);
            }
            else //Wall or steep slope
            {
                float scale = 1 - Vector3.Dot(new Vector3(hit.normal.x, 0, hit.normal.z).normalized, -new Vector3(velInit.x, 0, velInit.z).normalized);

                if (_isGrounded && !gravityPass)
                {
                    leftover = ProjectAndScale(new Vector3(leftover.x, 0, leftover.z), new Vector3(hit.normal.x, 0, hit.normal.z)).normalized;
                    leftover *= scale;
                }
                else
                {
                    leftover = ProjectAndScale(leftover, hit.normal) * scale;
                }
            }

            return snapToSurface + CollideAndSlide(leftover, startPos + snapToSurface, depth + 1, gravityPass, velInit);
        }

        return vel;
    }
    //Part of CollideAndSlide
    Vector3 ProjectAndScale(Vector3 vec, Vector3 normal)
    {
        float magnitude = vec.magnitude;
        vec = Vector3.ProjectOnPlane(vec, normal).normalized;
        vec *= magnitude;
        return vec;
    }


    
    //Could use colliders attached to the character to detect walls instead. Also the direction isn't correct yet
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
        //_currentMovement.x = _currentMovementInput.x * _moveSpeed;
        //_currentMovement.z = _currentMovementInput.y * _moveSpeed;
        //_currentSprintMovement.x = _currentMovement.x * _sprintMultiplier;
        //_currentSprintMovement.z = _currentMovement.y * _sprintMultiplier;  //We set z=y here since we're getting a Vector2 as the input and z is sideways in Vector3
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
