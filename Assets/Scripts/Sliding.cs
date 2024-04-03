using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Sliding : MonoBehaviour
{
    [Header("Refernces")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("sliding")]
    //public float maxSlideTime;
    public float slideForce;
    //private float slideTimer;
    public Vector3 inputDirection;

    //private float slideYscale = 0.5f;
    //private float startYscale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;


    //IMPORTANT: I AM VERY UNHAPPY WITH HOW SLIDING WORKS AND WILL PERSONALLY BE UPDATING IT TO BE ENTIRELY MOMENTUM BASED IN THE FUTURE. THIS WILL DO FOR OUR CURRENT SLICE THOUGH.

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
        inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //startYscale = playerObj.transform.localScale.y;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0))
        {
            startSlide();
        }

        if (Input.GetKeyUp(slideKey) && pm.sliding)
        {
            //StopCoroutine(slideToCrouchSpeedLerp());
            stopSlide();
        }
    }

    private void FixedUpdate()
    {
        if(pm.sliding)
        {
            slidingMovement();
        }
    }

    private void startSlide()
    {
        pm.sliding = true;
        //playerObj.localScale = new Vector3(playerObj.localScale.x, slideYscale, playerObj.localScale.z);

        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        
        rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Impulse);
        //slideTimer = maxSlideTime;
    }

    private void slidingMovement()
    {
        
        if (!pm.OnSlope() || rb.velocity.y > -0.1f)
        {
            
            //initial spike in force/momentum. after we lerp the speed down till it hits crouch speed.

            //StartCoroutine(slideToCrouchSpeedLerp());
            if (pm.moveSpeed == pm.crouchSpeed)
            {
                stopSlide();
            }


            //slideTimer -= Time.deltaTime;
            //if (slideTimer <= 0)
            //{
            //    stopSlide();
            //}

        }
        //sliding down slope
        else
        {
            rb.AddForce(pm.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
        }
       
    }


    //private IEnumerator slideToCrouchSpeedLerp()
    //{
    //    //lerp slide to crouch speed.
    //    float time = 0;
    //    float difference = Mathf.Abs(pm.crouchSpeed - pm.getMoveSpeed());
    //    float startValue = pm.getMoveSpeed();

    //    while (time < difference)
    //    {
    //        pm.moveSpeed = Mathf.Lerp(startValue, pm.crouchSpeed, time / difference);
    //        time += Time.deltaTime;
    //        yield return null;
    //    }

    //    pm.moveSpeed = pm.crouchSpeed;
    //}

    private void stopSlide()
    {
        pm.sliding = false;
        //playerObj.localScale = new Vector3(playerObj.localScale.x, startYscale, playerObj.localScale.z);
    }

    public float applySlideMultiplier(float currentSpeed, float newDesiredSpeed, float multiplier)
    {
        // Check if the new speed is greater than the current speed
        if (newDesiredSpeed > currentSpeed)
        {
            // Calculate the difference between the new speed and the current speed
            float speedIncrease = newDesiredSpeed - currentSpeed;

            // Apply the multiplier to the speed increase
            float modifiedIncrease = speedIncrease * multiplier;

            // Add the modified increase to the current speed
            return currentSpeed + modifiedIncrease;
        }
        else
        {
            // If the new speed is not greater than the current speed, return the new speed as is
            return newDesiredSpeed;
        }
    }
}
