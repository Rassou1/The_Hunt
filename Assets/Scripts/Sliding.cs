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
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;

    private float slideYscale = 0.5f;
    private float startYscale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;

    //IMPORTANT: I AM VERY UNHAPPY WITH HOW SLIDING WORKS AND WILL PERSONALLY BE UPDATING IT TO BE ENTIRELY MOMENTUM BASED IN THE FUTURE. THIS WILL DO FOR OUR CURRENT SLICE THOUGH.

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();

        startYscale = playerObj.transform.localScale.y;
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
        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYscale, playerObj.localScale.z);

        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    private void slidingMovement()
    {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (!pm.OnSlope() || rb.velocity.y > -0.1f)
        {
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

            slideTimer -= Time.deltaTime;
            if (slideTimer <= 0)
            {
                stopSlide();
            }
        }
        //sliding down slope
        else
        {
            rb.AddForce(pm.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
        }
       
    }

    private void stopSlide()
    {
        pm.sliding = false;
        playerObj.localScale = new Vector3(playerObj.localScale.x, startYscale, playerObj.localScale.z);
    }
}
