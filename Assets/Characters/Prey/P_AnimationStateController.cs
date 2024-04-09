using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AnimationStateController : MonoBehaviour
{

    Animator animator;
    int isWalkingHash; //Stores "isWalking" as a int for better performance
    int isRunningHash;
    int isClimbingHash;

    P_StateManager stateManager;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");  //Makes sure we know isWalkingHash = isWalking
        isRunningHash = Animator.StringToHash("isRunning");
        isClimbingHash = Animator.StringToHash("isClimbing");
        stateManager = GetComponent<P_StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isClimbing = animator.GetBool(isClimbingHash);

        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");
        bool climbPressed = stateManager.IsClimbingPressed;

        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }

        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if (!isRunning && forwardPressed && runPressed)
        {
            animator.SetBool(isRunningHash, true);
        }

        if (!forwardPressed || !runPressed)
        {
            animator.SetBool(isRunningHash, false);
        }

        if (!isClimbing && climbPressed)
        {
            animator.SetBool(isClimbingHash, true);
        }

        if (isClimbing && !climbPressed)
        {
            animator.SetBool(isClimbingHash, false);
        }

    }
}
