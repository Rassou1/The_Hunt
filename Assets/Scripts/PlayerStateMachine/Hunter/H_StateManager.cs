using Alteruna.Scoreboard;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class H_StateManager : MonoBehaviour
{
    public Alteruna.Avatar _avatar;
    //I'm using "_" for every variable that's declared in the class and not using it for the ones declared in methods. Should make it easier to see which one belongs where at a glance. Please follow this convention to the best of your abilities.
    PlayerInput _playerInput;

    public bool amIHunter;

    CapsuleCollider _capsuleCollider;
    Bounds _bounds;

    int _maxBounces = 5;
    float _skindWidth = 0.1f;

    

    public float _mouseSens;
    public float _maxSlopeAngle;
    public float _softCap;
    public float _sprintResistance;
    public float _slideResistance;


    H_BaseState _currentState;
    H_StateFactory _states;

    //Variables to store player inputs
    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    Vector3 _currentSprintMovement;
    Vector3 _appliedMovement;
    Vector2 _currentLookInput;

    public Rigidbody _rigidbody;
    public Transform _cameraOrientation;
    public Transform _cameraPostion;
    public H_Animations _animator;
    public interacter _interacter;
    //Transform _thisCharacter;

    float _mouseRotationX;
    float _mouseRotationY;

    Vector3 _moveForward;
    Vector3 _moveRight;

    bool _isMovementPressed;
    bool _isSprintPressed;
    bool _isJumpPressed;
    bool _isJumpReleased;
    bool _isDashPressed;
    bool _isDashReleased;
    bool _isSlidePressed;

    bool _isAttacking;

    bool _isGrounded = false;

    bool _dashCoolingDown;

    //New Stuff
    Vector3 _slopeNormal;
    float _slopeAngle;
    float _realSlopeAngle;
    Vector3 _stateDirection;
    Vector3 _preCollideMovement;

    

    float _vertMagnitude;
    float _horMouseMod = 1f;
    Vector3 _subStateDirSet;
    Vector3 _relForward;

    float _remainingDashCooldown;

    float _stateMagnitude;
    public float _finalMagnitude;
    public float _actualMagnitude;

    Vector3 _botSphere;
    Vector3 _topSphere;

    Vector3 _gravDir = Vector3.down;

    Vector3 _dashDirection = Vector3.zero;

    IEnumerator _dashCooldownCoroutine;
    IEnumerator _dashDurationCoroutine;
    IEnumerator _attackDurationCoroutine;

    Vector3 _resetPosition;

    //PlayerWalking walking;


    public int _dashCooldown;
    public float _dashDuraiton;
    public float _dashSpeed;

    public float _moveSpeed;
    public float climbspeed;

    public float finalAngle;

    protected int _caughtPrey;
    public int CaughtPrey { get { return _caughtPrey; } }

    //Put a lot of getters and setters here
    public H_BaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Rigidbody Rigidbody { get { return _rigidbody; } }
    public H_Animations Animator { get { return _animator; } }
   
    

    public Vector3 StateDirection { get { return _stateDirection; } set { _stateDirection = value; } }
    public float StateMagnitude { get { return _stateMagnitude; } set { _stateMagnitude = value; } }

    public Vector3 SubStateDirSet { get { return _subStateDirSet; } set { _subStateDirSet = value; } }
    public float VertMagnitude { get { return _vertMagnitude; } set { _vertMagnitude = value; } }
    public float ActualMagnitude { get { return _actualMagnitude; } set { _actualMagnitude = value; } }

    public float HorMouseMod { get { return _horMouseMod; } set { _horMouseMod = value; } }

    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } set { _currentMovementInput = value; } }
    public Vector3 CurrentMovement { get { return _currentMovement; } set { _currentMovement = value; } }
    public Vector3 CurrentSprintMovement { get { return _currentSprintMovement; } set { _currentSprintMovement = value; } }
    public Vector3 AppliedMovement { get { return _appliedMovement; } set { _appliedMovement = value; } }
    public Vector3 RelForward { get { return _relForward; } }

    public Vector3 BotSphere { get { return _botSphere; } }
    public Vector3 TopSphere { get { return _topSphere; } }

    public Vector3 PreCollideMovement { get { return _preCollideMovement; } }

    public float CurrentMovementY { get { return _currentMovement.y; } set { _currentMovement.y = value; } }
    public float AppliedMovementY { get { return _appliedMovement.y; } set { _appliedMovement.y = value; } }
    public float AppliedMovementX { get { return _appliedMovement.x; } set { _appliedMovement.x = value; } }
    public float AppliedMovementZ { get { return _appliedMovement.z; } set { _appliedMovement.z = value; } }



    public Vector3 SlopeNormal { get { return _slopeNormal; } }
    public float SlopeAngle { get { return _slopeAngle; } }
    public float RealSlopeAngle { get { return _realSlopeAngle; } }

    public bool IsMovementPressed { get { return _isMovementPressed; } }
    public bool IsSprintPressed {  get { return _isSprintPressed; } }
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    public bool IsJumpReleased { get { return _isJumpReleased; } }
    public bool IsDashPressed{ get { return _isDashPressed; } }
    public bool IsDashReleased { get { return _isDashReleased; } }
    public bool IsSlidePressed { get { return _isSlidePressed; } }

    public bool IsGrounded {  get { return _isGrounded; } set { _isGrounded = value; } }

    
    public Vector3 GravDirection { get { return _gravDir; } set { _gravDir = value; } }

    public static H_StateManager Instance { get; internal set; }

    public float CapsuleColliderHeight { get { return _capsuleCollider.height; } set { _capsuleCollider.height = value; } }
    public Vector3 OrientationPos { get { return transform.position; } set { transform.position = value; } }

    public float RemainingDashCooldown { get { return _remainingDashCooldown;} }
    public bool DashCoolingDown { get { return _dashCoolingDown; } }
    public bool IsAttacking { get { return _isAttacking; } }
    

    void Start()    
    {
        //_avatar = GetComponentInParent<Alteruna.Avatar>();
    }




    private void Awake()
    {
        
        //walking = gameObject.GetComponentInParent<PlayerWalking>();
        _playerInput = new PlayerInput();
        _capsuleCollider = GetComponent<CapsuleCollider>();


        _bounds = _capsuleCollider.bounds;
        _bounds.Expand(-2 * _skindWidth);


        //This gets the inputs from the new input system
        _playerInput.HunterControls.Move.started += OnMovementInput;
        _playerInput.HunterControls.Move.canceled += OnMovementInput;
        _playerInput.HunterControls.Move.performed += OnMovementInput; //This allows the game to realize we might be holding two buttons at once (based). It also allows for controler inputs (cringe)
        _playerInput.HunterControls.Sprint.started += OnSprint;
        _playerInput.HunterControls.Sprint.canceled += OnSprint;
        _playerInput.HunterControls.Jump.started += OnJumpPress;
        _playerInput.HunterControls.Jump.canceled += OnJumpPress;
        _playerInput.HunterControls.Dash.started += OnDashPress;
        _playerInput.HunterControls.Slide.started += OnSlide;
        _playerInput.HunterControls.Slide.canceled += OnSlide;
        _playerInput.HunterControls.Slide.performed += OnSlide;
        _playerInput.HunterControls.Look.started += OnLookInput;
        _playerInput.HunterControls.Look.canceled += OnLookInput;
        _playerInput.HunterControls.Look.performed += OnLookInput;
        _playerInput.HunterControls.SetReset.started += OnSetReset;
        _playerInput.HunterControls.Reset.started += OnReset;
        _playerInput.HunterControls.Attack.started += OnAttack;
        _playerInput.HunterControls.SensUp.started += OnSensUp;
        _playerInput.HunterControls.SensDown.started += OnSensDown;
        


        //setup state
        
        _states = new H_StateFactory(this);
        _currentState = _states.Ground();
        _currentState.EnterState();


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _resetPosition =  _rigidbody.transform.position;
        
    }

    

    void Update()
    {
        //Add a Way so a remote avatar still makes sounds

        if (!_avatar.IsMe)
        {
            return;
        }
        else
        {



            //if (_isMovementPressed && _isGrounded && !_isSprintPressed)
            //{
            //    walking.PlayWalkSound();
            //}

            //if (_isMovementPressed && _isGrounded && _isSprintPressed)
            //{
            //    walking.PlayRunSound();
            //}


            _botSphere = _capsuleCollider.transform.position + new Vector3(0, _capsuleCollider.radius, 0);
            _topSphere = _capsuleCollider.transform.position + new Vector3(0, _capsuleCollider.height - _capsuleCollider.radius, 0);
            GroundCheck();
            if (!_isGrounded)
            {
                AntiClipCheck();
            }

            Debug.Log(Rigidbody.position);

            SetCameraOrientation();

            RotateBodyY();
            _relForward = CamRelHor(Vector3.forward);

            Debug.Log("Current Hunter State:" + CurrentState);

            _currentState.UpdateStates();

            _appliedMovement = AlignToSlope(_stateDirection);
            _preCollideMovement = _appliedMovement;
            _finalMagnitude = _stateMagnitude;

            Debug.DrawRay(_rigidbody.transform.position, _relForward, Color.green, Time.deltaTime);
            _appliedMovement = CamRelHor(_appliedMovement);

            _appliedMovement = _appliedMovement.normalized;
            _appliedMovement *= _finalMagnitude;

            _appliedMovement += _dashDirection;
            _actualMagnitude = _finalMagnitude;
            _appliedMovement *= Time.deltaTime;



            Debug.DrawRay(_rigidbody.transform.position, _appliedMovement / Time.deltaTime, Color.red, Time.deltaTime);
            _appliedMovement = CollideAndSlide(_appliedMovement, _capsuleCollider.transform.position, 0, false, _appliedMovement);
            _appliedMovement += CollideAndSlide(_gravDir * -_vertMagnitude * Time.deltaTime, _capsuleCollider.transform.position + _appliedMovement, 0, true, _gravDir * -_vertMagnitude * Time.deltaTime);
            Debug.DrawRay(_rigidbody.transform.position, _appliedMovement / Time.deltaTime, Color.blue, Time.deltaTime);
            _rigidbody.transform.position += _appliedMovement;

        }
    }


    //This function  based on this YT video https://www.youtube.com/watch?v=YR6Q7dUz2uk which in turn is based on this paper https://www.peroxide.dk/papers/collision/collision.pdf
    Vector3 CollideAndSlide(Vector3 vel, Vector3 startPos, int depth, bool gravityPass, Vector3 velInit)
    {
        
        if (depth >= _maxBounces)
        {
            return Vector3.zero;
        }
        float dist = vel.magnitude + _skindWidth;
        RaycastHit hit;
        

        if (Physics.CapsuleCast(_botSphere, _topSphere, _bounds.extents.x, vel.normalized, out hit, dist))
        {
            Vector3 snapToSurface = vel.normalized * (hit.distance - _skindWidth);
            Vector3 leftover = vel - snapToSurface;
            
            

            float angle = Vector3.Angle(_gravDir * -1, hit.normal);

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
                    leftover = ProjectAndScale(new Vector3(leftover.x, 0, leftover.z), new Vector3(hit.normal.x, 0, hit.normal.z));
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


    void GroundCheck()
    {
        RaycastHit hit;
        Physics.SphereCast(_botSphere, _bounds.extents.x, _gravDir, out hit, _skindWidth * 2f);
        if (hit.transform != null && Vector3.Angle(hit.normal, Vector3.up) <= _maxSlopeAngle)
        {
            _isGrounded = true;
            _slopeNormal = hit.normal;
            _slopeAngle = 90f - Vector3.Angle(_relForward, _slopeNormal);
            _realSlopeAngle = Vector3.Angle(-_gravDir, _slopeNormal);
            finalAngle = _slopeAngle;
            //Debug.Log("slope angle" + _slopeAngle);
        }
        else
        {
            _isGrounded = false;
            _slopeNormal = Vector3.zero;
            _slopeAngle = 0f;
            _realSlopeAngle = 0f;
        }
    }

    void AntiClipCheck()
    {
        RaycastHit hit;
        Physics.Raycast(_rigidbody.transform.position, _gravDir, out hit, _capsuleCollider.height / 2);
        if (hit.transform != null)
        {
            _rigidbody.transform.position = hit.point + _gravDir.normalized * -1 * (0.05f + _capsuleCollider.height / 2f);
            _isGrounded = true;
            _slopeNormal = hit.normal;
            _slopeAngle = 90f - Vector3.Angle(_relForward, _slopeNormal);
            _realSlopeAngle = Vector3.Angle(-_gravDir, _slopeNormal);
        }
    }

    public Vector3 AlignToSlope(Vector3 inputDirection)
    {
        var slopeRotation = Quaternion.AngleAxis(_slopeAngle, Vector3.right);
        return slopeRotation * inputDirection;
    }


    Vector3 CamRelHor(Vector3 input)
    {
        Vector3 camRelativeHor;
        //input = new Vector3(input.x, 0, input.z);
        camRelativeHor = _moveForward.normalized * input.z + _moveRight.normalized * input.x;
        camRelativeHor = new Vector3(camRelativeHor.x, input.y, camRelativeHor.z);
        return camRelativeHor;
    }

    
    public void ResetHunterStats()
    {
        _caughtPrey = 0;
    }


    public void OnJumpPress(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
    }


    public void OnDashPress(InputAction.CallbackContext context)
    {
        if (_dashCoolingDown) return;
        _dashDirection = CamRelHor(new Vector3(0, 0, _dashSpeed));
        _vertMagnitude = 0f;
        _actualMagnitude += _dashSpeed;
        _currentState.HasDoubleJumped = false;
        _dashCoolingDown = true;
        _dashCooldownCoroutine = DashCooldown();
        _dashDurationCoroutine = DashDuration();
        StartCoroutine(_dashCooldownCoroutine);
        StartCoroutine(_dashDurationCoroutine);
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
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
        
    }

    void OnLookInput(InputAction.CallbackContext context)
    {
        _currentLookInput = context.ReadValue<Vector2>();
        _mouseRotationX -= _currentLookInput.y * Time.deltaTime * _mouseSens;
        _mouseRotationY += _currentLookInput.x * Time.deltaTime * _mouseSens * _horMouseMod;
        _mouseRotationX = Mathf.Clamp(_mouseRotationX, -89f, 89f);
    }

    void OnSetReset(InputAction.CallbackContext context)
    {
        _resetPosition = _rigidbody.transform.position;
    }

    void OnReset(InputAction.CallbackContext context)
    {
        _actualMagnitude = 0;
        _appliedMovement = Vector3.zero;
        _preCollideMovement = Vector3.zero;
        _rigidbody.transform.position = _resetPosition;
    }

    void OnAttack(InputAction.CallbackContext context)
    {
        if (_isAttacking || _currentState.CurrentSubState == _states.Slide()) return;
        _isAttacking = true;
        //_animator.SetPunching(true);
        _attackDurationCoroutine = AttackDuration();
        StartCoroutine(_attackDurationCoroutine);
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
        var forward = _cameraOrientation.forward;
        forward.y = 0;
        _rigidbody.transform.rotation = Quaternion.LookRotation(forward, _rigidbody.transform.up);
    }


    //Usíng coroutines for the dash cooldown and duration. I don't know if it actually is an appropriate thing to do but it seems to work. Dash Cooldown has to be in
    IEnumerator DashCooldown()
    {
        _remainingDashCooldown = _dashCooldown;
        for (float i = 0; i < _dashCooldown; i += Time.deltaTime)
        {
            _remainingDashCooldown -= Time.deltaTime;
            yield return null;
            //Debug.Log(_remainingDashCooldown);
        }
        _remainingDashCooldown = 0;
        _dashCoolingDown = false;
    }

    IEnumerator DashDuration()
    {
        yield return new WaitForSeconds(_dashDuraiton);
        _dashDirection = Vector3.zero;
    }

    IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(0.1f);
        for(float i = 0; i < 0.5f; i += Time.deltaTime)
        {
            _interacter.InteractionRay();
            yield return null;
        }
        //_animator.SetPunching(false);
        yield return new WaitForSeconds(0.35f);
        _isAttacking = false;
    }

    void OnSensUp(InputAction.CallbackContext context)
    {
        _mouseSens += 2;
    }

    void OnSensDown(InputAction.CallbackContext context)
    {
        _mouseSens -= 2;
    }

    void OnEnable()
    {
        _playerInput.HunterControls.Enable();
    }

    void OnDisable()
    {
        _playerInput.HunterControls.Disable();
    }


}
