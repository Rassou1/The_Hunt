using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Wallrunning : MonoBehaviour
{
    [Header("Wallrunning")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce;
    public float maxWallRunTime;
    private float wallRunTimer;

    [Header("Input")]
    private float horizontalInput;
    private float verticalInput;
    public KeyCode upwardsRunKey = KeyCode.LeftShift;
    public KeyCode downwardsRunKey = KeyCode.LeftControl;
    private bool upwardsRunning;
    private bool downwardsRunning;
    public float wallClimbSpeed;

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft;
    private bool wallRight;

    [Header("References")]
    public Transform orientation;
    private PlayerMovement pm;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForWall();
        StateMachine();
    }

    private void FixedUpdate()
    {
        if(pm.wallrunning)
        {
            wallrunMovement();
        }
    }

    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallCheckDistance, whatIsWall);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void StateMachine()
    {
        //Getting Inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        upwardsRunning = Input.GetKey(upwardsRunKey);
        downwardsRunning = Input.GetKey(downwardsRunKey);

        if((wallLeft || wallRight) && verticalInput > 0 && AboveGround())
        {
            //start wallrun
            if (!pm.wallrunning)
            {
                startWallrun();
            }

            else
            {
                if (pm.wallrunning)
                {
                    stopWallrun();
                }
            }
        }



    }

    public void startWallrun()
    {
        pm.wallrunning = true;
    }

    public void wallrunMovement()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }

        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);
        //pressing sprint while wallrunning disables gravity until another wallrun is engaged.

        

        //push playerobject towards wall
        if(!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
        {
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
        }

        if (upwardsRunning)
        {
            rb.velocity = new Vector3(rb.velocity.x, wallClimbSpeed, rb.velocity.z);
        }
        if (downwardsRunning)
        {
            rb.velocity = new Vector3(rb.velocity.x, -wallClimbSpeed, rb.velocity.z);
        }
    }

    public void stopWallrun()
    {
        pm.wallrunning = false;
    }
}
