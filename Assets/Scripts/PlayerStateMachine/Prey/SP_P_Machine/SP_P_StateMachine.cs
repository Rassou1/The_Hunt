
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//This version of the script has been changed to remove all references to multiplayer functionality so it can be used for singleplayer.
// I didn't want to bog down the script with a bunch of checks if you were in multiplayer or singleplayer so I just created a copy instead of using the MP one - Love
public class SP_P_StateMachine : MonoBehaviour
{
    
    //I'm using "_" for every variable that's declared in the class and not using it for the ones declared in methods. Should make it easier to see which one belongs where at a glance. Please follow this convention to the best of your abilities.
    PlayerInput _playerInput;


    CapsuleCollider _capsuleCollider;
    Bounds _bounds;

    int _maxBounces = 5;
    float _skindWidth = 0.05f;

    //LayerMask whatIsGround;




    public float _mouseSens;
    public float _maxSlopeAngle;
    public float _softCap;
    public float _sprintResistance;
    public float _slideResistance;



    SP_P_BaseState _currentState;
    SP_P_StateFactory _states;

    //Variables to store player inputs
    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    Vector3 _currentSprintMovement;
    Vector3 _appliedMovement;
    Vector2 _currentLookInput;

    public Rigidbody _rigidbody;
    public Transform _cameraOrientation;
    public Transform _cameraPostion;
    public Animator _animator;
    public Animator _armsAnimator;
    
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

    Vector3 _resetPosition;

    PlayerWalking playerSounds;
    PlayerTest otherPlayerSounds;



    public int _dashCooldown;
    public float _dashDuraiton;
    public float _dashSpeed;

    public float _moveSpeed;

    protected bool _caught;
    protected bool _escaped;
    protected int _diamondsTaken;

    bool _ghost = false;


    public bool Escaped { get { return _escaped; } set { _escaped = value; } }
    public bool Caught { get { return _caught; } set { _caught = value; } }
    public int DiamondsTaken { get { return _diamondsTaken; } set { _diamondsTaken = value; } }
    public bool Ghost { get { return _ghost; } set { _ghost = value; } }


    //Put a lot of getters and setters here
    public SP_P_BaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Rigidbody Rigidbody { get { return _rigidbody; } }
    public Animator Animator { get { return _animator; } }
    public Animator ArmsAnimator { get { return _armsAnimator; } }
    



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
    public bool IsSprintPressed { get { return _isSprintPressed; } }
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    public bool IsJumpReleased { get { return _isJumpReleased; } }
    public bool IsDashPressed { get { return _isDashPressed; } }
    public bool IsDashReleased { get { return _isDashReleased; } }
    public bool IsSlidePressed { get { return _isSlidePressed; } }

    public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }


    public Vector3 GravDirection { get { return _gravDir; } set { _gravDir = value; } }

    public static P_StateManager Instance { get; internal set; }

    public float CapsuleColliderHeight { get { return _capsuleCollider.height; } set { _capsuleCollider.height = value; } }
    public Vector3 OrientationPos { get { return transform.position; } set { transform.position = value; } }

    public float RemainingDashCooldown { get { return _remainingDashCooldown; } }
    public bool DashCoolingDown { get { return _dashCoolingDown; } }


    void Start()
    {
        

    }




    private void Awake()
    {
        //_avatar = gameObject.GetComponent<Alteruna.Avatar>();
        playerSounds = gameObject.GetComponentInParent<PlayerWalking>();
        otherPlayerSounds = gameObject.GetComponentInParent<PlayerTest>();
        _playerInput = new PlayerInput();
        _capsuleCollider = GetComponent<CapsuleCollider>();


        //Getting the bounds of the capsule collider and reduce it slightly to use for collisions later - Love
        _bounds = _capsuleCollider.bounds;
        _bounds.Expand(-2 * _skindWidth);

        

        //This gets the inputs from unity's new input system - Love
        _playerInput.PreyControls.Move.started += OnMovementInput;
        _playerInput.PreyControls.Move.canceled += OnMovementInput;
        _playerInput.PreyControls.Move.performed += OnMovementInput; //This allows the game to realize we might be holding two buttons at once (based). It also allows for controler inputs (cringe)
        _playerInput.PreyControls.Sprint.started += OnSprint;
        _playerInput.PreyControls.Sprint.canceled += OnSprint;
        _playerInput.PreyControls.Jump.started += OnJumpPress;
        _playerInput.PreyControls.Jump.canceled += OnJumpPress;
        _playerInput.PreyControls.Dash.started += OnDashPress;
        _playerInput.PreyControls.Slide.started += OnSlide;
        _playerInput.PreyControls.Slide.canceled += OnSlide;
        _playerInput.PreyControls.Slide.performed += OnSlide;
        _playerInput.PreyControls.Look.started += OnLookInput;
        _playerInput.PreyControls.Look.canceled += OnLookInput;
        _playerInput.PreyControls.Look.performed += OnLookInput;
        _playerInput.PreyControls.SetReset.started += OnSetReset;
        _playerInput.PreyControls.Reset.started += OnReset;
        _playerInput.PreyControls.SensUp.started += OnSensUp;
        _playerInput.PreyControls.SensDown.started += OnSensDown;
        _playerInput.PreyControls.GhostTest.started += OnGhost;


        //setup state states here - Love

        _states = new SP_P_StateFactory(this);
        _currentState = _states.Ground();
        _currentState.EnterState();


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Set the resetposition of the character to wherever they spawn so I don't have to set it each time I debug - Love
        _resetPosition = _rigidbody.transform.position;

        //To make sure the collisions script doesn't try to stop the character when running into a trigger collider - Love
        Physics.queriesHitTriggers = false;

    }



    void Update()
    {
        



        
        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    DiamondsTaken++;
        //}

        if (gameObject.GetComponentInParent<PlayerWalking>() != null)
        {
            if (_isMovementPressed && _isGrounded && !_isSprintPressed && !_isSlidePressed)
            {
                playerSounds.PlayWalkSound();
            }

            if (_isMovementPressed && _isGrounded && _isSprintPressed && !_isSlidePressed)
            {
                playerSounds.PlayRunSound();
            }
        }




        //Update positions of where the centre of the "spheres" at the edges of the capsule collider are for use in collisions
        _botSphere = _capsuleCollider.transform.position + new Vector3(0, _capsuleCollider.radius, 0);
        _topSphere = _capsuleCollider.transform.position + new Vector3(0, _capsuleCollider.height - _capsuleCollider.radius, 0);

        //Here we start by setting the rotation of the player camera based on mouse inputs - Love
        SetCameraOrientation();
        //We follow that up by rotating the character to face the same direciton as the camera is looking as well a creating a horizontal forward vector - Love
        RotateBodyY();
        _relForward = CamRelHor(Vector3.forward);
        //Here we update the state machine, this is where the variables like _stateDirection and _stateMagnitude get updated - Love
        _currentState.UpdateStates();

        //This is from my quick and dirty implementaiton of a "ghost mode" or spectator mode which I used for recording parts of the trailer and to make debugging easier - Love
        if (_currentState != _states.Ghost())
        {
            //Calling ground check function, followed by an emergency function that gets called when not grounded to make sure you're not falling through the ground - Love
            GroundCheck();
            if (!_isGrounded)
            {
                AntiClipCheck();
            }
            //Align the characters movement direction to the ground underneath it. We are not rotating the character itself, only the vector that decides its movement - Love
            _appliedMovement = AlignToSlope(_stateDirection);
            //Saving a couple of values that we use to calculate momentum etc in some of the states.
            _preCollideMovement = _appliedMovement;
            _finalMagnitude = _stateMagnitude;

            //The movement before this has been relative to the world coordinates, now it is relative to the players camera - Love
            _appliedMovement = CamRelHor(_appliedMovement);

            //Normalize the direction and then multiply it by the magnitude from the state machine - Love
            _appliedMovement = _appliedMovement.normalized;
            _appliedMovement *= _finalMagnitude;

            //This is a really quick and honestly bad implementation of the dash I quickly made towards the end of the project. _dashDirection is usually (0,0,0) but set to a different value for a short duration when dashing - Love
            _appliedMovement += _dashDirection;
            //I think _actualMagnitude is a variable from earlier in the project that was used when I actually changed _finalMagnitude in this part of the code - Love
            _actualMagnitude = _finalMagnitude;
            _appliedMovement *= Time.deltaTime;



            //First collision pass gives us the collision of the "normal" movement - Love
            _appliedMovement = CollideAndSlide(_appliedMovement, _capsuleCollider.transform.position, 0, false, _appliedMovement);
            //Second pass gives us the collision of the movement resulting from gravity - Love
            _appliedMovement += CollideAndSlide(_gravDir * -_vertMagnitude * Time.deltaTime, _capsuleCollider.transform.position + _appliedMovement, 0, true, _gravDir * -_vertMagnitude * Time.deltaTime);
            //Move the rigidbody of the character with the movement after the collision passes - Love
            _rigidbody.transform.position += _appliedMovement;
        }
        else
        {
            //The code that is ran when in ghost mode doesn't include collision nor aligning to slopes
            _appliedMovement = CamRelHor(_stateDirection);
            _appliedMovement = _appliedMovement + _gravDir * _vertMagnitude * -1;
            _appliedMovement = _appliedMovement.normalized;
            _appliedMovement *= _stateMagnitude;
            _rigidbody.transform.position += _appliedMovement * Time.deltaTime;
        }

        

    }



    //This function  based on this YT video https://www.youtube.com/watch?v=YR6Q7dUz2uk which in turn is based on this paper https://www.peroxide.dk/papers/collision/collision.pdf
    //It essentially takes collisions and lets the movement vector go across the surface you collided with. This is done up to 5 times - Love
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

    //This method checks if you're grounded or not and then set the values of some relevant variables which is used when aligning the movement to the ground - Love
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
        }
        else
        {
            _isGrounded = false;
            _slopeNormal = Vector3.zero;
            _slopeAngle = 0f;
            _realSlopeAngle = 0f;
        }
    }

    public void TeleportPlayer(Vector3 pos)
    {
        _rigidbody.transform.position = pos;
    }

    //This is an emergency method for snapping the player up to the surface they where standing on if the start to clip through. Kind of brute forcing the problem but it works most of the time - Love
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

    //Here I use the accursed quaternions to align the direction of the input vector (the movement direction of the player) to the angle of the slope beneath it - Love
    public Vector3 AlignToSlope(Vector3 inputDirection)
    {
        var slopeRotation = Quaternion.AngleAxis(_slopeAngle, Vector3.right);
        return slopeRotation * inputDirection;
    }

    //Set the horizontal movement of an incoming vector to be relative to the camera - Love
    Vector3 CamRelHor(Vector3 input)
    {
        Vector3 camRelativeHor;
        //input = new Vector3(input.x, 0, input.z);
        camRelativeHor = _moveForward.normalized * input.z + _moveRight.normalized * input.x;
        camRelativeHor = new Vector3(camRelativeHor.x, input.y, camRelativeHor.z);
        return camRelativeHor;
    }
    //Reset how many prey the hunter has caught - Love
    public void ResetPreyStats()
    {
        _escaped = false;
        _caught = false;
        _ghost = false;
    }

    //All "On_Press" methods are tied to the input system - Love
    public void OnJumpPress(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
        if (context.started && gameObject.GetComponentInParent<PlayerWalking>() != null)
        {
            if (_isGrounded)
            {
                playerSounds.PlayJumpStartSound();
            }
        }

    }

    //The dash uses coroutines to count down the time both for the duration of the dash and for the duration of the cooldown - Love
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
        if (gameObject.GetComponentInParent<PlayerWalking>() != null)
        {
            playerSounds.PlayDashSound();
        }

    }



    void OnSprint(InputAction.CallbackContext context)
    {
        _isSprintPressed = context.ReadValueAsButton();
    }

    void OnSlide(InputAction.CallbackContext context)
    {
        _isSlidePressed = context.ReadValueAsButton();
        if (gameObject.GetComponentInParent<PlayerWalking>() != null)
        {
            if (context.started && _isGrounded)
            {
                playerSounds.PlaySlidingSound();
            }

            if (context.canceled)
            {
                playerSounds.AudioManager.Stop();
            }
        }

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
        _mouseRotationX = Mathf.Clamp(_mouseRotationX, -89f, 89f); //When rotating to -90 and 90 on the x axis the rotation of the player model got confused so i just clamped it to -89 and 89 - Love
    }

    //The following two methods are purely for debug purposes and not for gameplay - Love
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

    //Rotates the camera according to the mouse rotation from the "OnLookInput" method
    public void SetCameraOrientation()
    {
        _cameraOrientation.rotation = Quaternion.Euler(_mouseRotationX, _mouseRotationY, 0);
        _moveForward = _cameraOrientation.forward;
        _moveRight = _cameraOrientation.right;
        _moveForward.y = 0;
        _moveRight.y = 0;
    }


    //Rotates the player to where the camera is pointing while keeping the orientation of the player upright
    void RotateBodyY()
    {
        var forward = _cameraOrientation.forward;
        forward.y = 0;
        _rigidbody.transform.rotation = Quaternion.LookRotation(forward, _rigidbody.transform.up);
    }


    //Us�ng coroutines for the dash cooldown and duration. I don't know if it actually is an appropriate thing to do but it seems to work - Love

    IEnumerator DashCooldown()
    {
        _remainingDashCooldown = _dashCooldown;
        for (float i = 0; i < _dashCooldown; i += Time.deltaTime)
        {
            _remainingDashCooldown -= Time.deltaTime;
            yield return null;
        }
        _remainingDashCooldown = 0;
        _dashCoolingDown = false;
    }

    IEnumerator DashDuration()
    {
        yield return new WaitForSeconds(_dashDuraiton);
        _dashDirection = Vector3.zero;
    }



    void OnSensUp(InputAction.CallbackContext context)
    {
        _mouseSens += 2;
    }

    void OnSensDown(InputAction.CallbackContext context)
    {
        _mouseSens -= 2;
    }

    void OnGhost(InputAction.CallbackContext context)
    {
        _ghost = !_ghost;
    }



    void OnEnable()
    {
        _playerInput.PreyControls.Enable();
    }

    void OnDisable()
    {
        _playerInput.PreyControls.Disable();
    }


}
