using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Processors;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    public Transform orientation;
    private bool isGrounded;
    public AudioSource walkingSource;
    public AudioClip walking;
    public AudioSource jumpingSource;
    public AudioClip jumping;
    private float jumpForce = 5f;
    private float movementSpeed = 10;
    private UnityEngine.Vector3 playerVelocity;
    public Camera cam;
    public float jumpHeight = 2f;
    public bool sprinting = false;
    public bool doubleJump = false;
    private bool usedDubJump = false;
    private bool vaultReset = true;
    public GameObject player;
    public Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;

    public float groundCheckRadius = 0.5f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;
    public float castOffset = 4f;
     public float crouchScaleMultiplier = 0.5f; // The multiplier to reduce the scale by when crouching
    public float crouchSpeed = 5f; // The speed at which the character crouches
    private Vector3 originalScale;
    private bool isCrouching = false;


    void Update()
    {
        // Get input from the player
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        isGrounded = GroundCheck();
        
        if(isGrounded)
        {
            exitingSlope = false;
        }

    float targetScaleY = isCrouching ? originalScale.y * crouchScaleMultiplier : originalScale.y;
        Vector3 targetScale = new Vector3(originalScale.x, targetScaleY, originalScale.z);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, crouchSpeed * Time.deltaTime);
        // Do something based on whether the player is grounded or not
        Sprint();
    }
    public Vector3 moveDirectionSlope;
    public float maxSpeed = 10f;
    void FixedUpdate() //Raycast to normalize to slope vector
    {
        bool Iswalking;
        Vector3 moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if(rb.velocity.x > 0 || rb.velocity.z > 0)
        { 
            Iswalking = true;
        }
        else 
        {
            Iswalking = false;
        }
        
        if (Iswalking && !walkingSource.isPlaying)
        {
            walkingSource.PlayOneShot(walking);
        }
            else if (!Iswalking && walkingSource.isPlaying)
        {
            walkingSource.Stop();
        }
        
        SpeedControl();
        // on slope
        

        if (OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection() * movementSpeed * 20f, ForceMode.Acceleration);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        else if(isGrounded)
        {
            rb.AddForce(moveDirection.normalized * movementSpeed * 10f, ForceMode.Acceleration);
        }

        moveDirectionSlope = moveDirection;
        //print(OnSlope() + "  " + GetSlopeMoveDirection());
    }

       private void SpeedControl()
    {
        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > movementSpeed)
                rb.velocity = rb.velocity.normalized * movementSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > movementSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * movementSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cam = Camera.main;
        originalScale = transform.localScale;

    }
    //receive input from manger and put into controller
   public float maxSlopeAngle;
   private RaycastHit slopeHit;


   private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit,5f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirectionSlope, slopeHit.normal).normalized;
    }
    
    bool GroundCheck()
    {   

        Vector3 rayOrigin = transform.position - Vector3.up * castOffset;
        float rayDistance = groundCheckDistance + groundCheckRadius;

        // Perform a sphere cast downwards to check for ground contact
        bool isGrounded = Physics.SphereCast(rayOrigin, groundCheckRadius, Vector3.down, out RaycastHit hitInfo, rayDistance, groundLayer);

        // Optionally, draw a debug line to visualize the sphere cast
        Debug.DrawLine(rayOrigin, rayOrigin + Vector3.down * rayDistance, isGrounded ? Color.green : Color.red);

        return isGrounded;
    }
    private bool notJumping;
    private bool exitingSlope;
    public void Jump()
    {
        
        if (isGrounded)
        {
            vaultReset = true;
        }
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            usedDubJump = false;
            exitingSlope = false;
            if (!jumpingSource.isPlaying) jumpingSource.PlayOneShot(jumping);
            
        }else if (!isGrounded && !usedDubJump && doubleJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);;
            usedDubJump = true;
            
            
        }  else if (isGrounded && usedDubJump)
        {
            usedDubJump = false;
            doubleJump = false;     
            
        } 
        
    }

    public void Crouch()
    {
        isCrouching = !isCrouching;
    }
    public void Sprint()
    {
        while (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = 30;
            break;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movementSpeed = 10;
        }
    }
    /*public void JVaultLedge(bool canVault)

    {
        if (!isGrounded && canVault == true && vaultReset)
        {
           
            playerVelocity.y = Mathf.Sqrt((jumpHeight * 0.6f) * -3.0f * gravity);
            vaultReset = false;
        }
    
    }*/

}
