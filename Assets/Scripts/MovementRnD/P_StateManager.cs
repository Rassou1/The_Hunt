using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_StateManager : MonoBehaviour
{

    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isSprintingHash;
    int isFallingHash;

    P_BaseState _currentState;
    P_StateFactory _states;

    //Variables to store player inputs
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentSprintMovement;
    bool isMovementPressed;
    bool isSprintPressed;
    

    float rotationFactorPerFrame = 10f;
    float sprintMultiplier = 3f;

    //Put a lot of getters and setters here
    public P_BaseState CurrentState { get { return _currentState; } set { _currentState = value; } }



    //void Start()
    //{
        
    //}

    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isSprintingHash = Animator.StringToHash("isRunning");
        isFallingHash = Animator.StringToHash("isFalling");

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput; //This allows the game to realize we might be holding two buttons at once (based). It also allows for controler inputs (cringe)
        playerInput.CharacterControls.Sprint.started += onSprint;
        playerInput.CharacterControls.Sprint.canceled += onSprint;
        

        //setup state
        _states = new P_StateFactory(this);

        _currentState = _states.Ground();
        _currentState.EnterState();

    }

    void handleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
        
    }

    

    void onSprint(InputAction.CallbackContext context)
    {
        isSprintPressed = context.ReadValueAsButton();
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentSprintMovement.x = currentMovementInput.x * sprintMultiplier;
        currentSprintMovement.z = currentMovementInput.y * sprintMultiplier;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void handleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isSprinting = animator.GetBool(isSprintingHash);
        bool isFalling = animator.GetBool(isFallingHash);

        if (!characterController.isGrounded && !isFalling)
        {
            animator.SetBool(isFallingHash, true);
        }
        else if(characterController.isGrounded && isFalling)
        {
            animator.SetBool(isFallingHash, false);
        }

        if(isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if(!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if((isMovementPressed && isSprintPressed) && !isSprinting)
        {
            animator.SetBool(isSprintingHash, true);
        }
        else if((!isMovementPressed || !isSprintPressed) && isSprinting)
        {
            animator.SetBool(isSprintingHash, false);

        }

    }

    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            float groundedGravity = -.05f;
            currentMovement.y = groundedGravity;
            currentSprintMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity * Time.deltaTime;
            currentSprintMovement.y += gravity * Time.deltaTime;
        }

        

    }

    void Update()
    {
        handleRotation();
        handleAnimation();
        handleGravity();
        

        if (isSprintPressed)
        {
            characterController.Move(currentSprintMovement * Time.deltaTime);

        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }

        

        _currentState.UpdateStates();
    }


    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
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
