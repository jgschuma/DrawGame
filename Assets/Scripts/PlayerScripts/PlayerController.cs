using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    public float Velocity;
    public float WalkSpeed;
    public float RunSpeed;
    public float CrouchMoveSpeed;
    private float MoveSpeed;
    public bool isSprint;
    public Rigidbody rb;
    
    [Header("Jump Check variables")]  
    public LayerMask GroundLayer;
    public bool isGrounded;
    public float JumpForce;
    public float JumpForceStand;
    public float JumpForceSlide;

    [Header("Crouch variables")]
    public float StandHeight;
    public float CrouchHeight;
    private float DesiredHeight;
    public float CrouchSpeed;
    public float toSlideSpeed;
    public float SlideForce;
    public float MaxSlideTimer;
    private float SlideTimer;
    private bool isCrouched;
    private bool isSlide;
    
    
    CapsuleCollider PlayerCollider;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerCollider = GetComponent<CapsuleCollider>();
        JumpForce = JumpForceStand;
    }

    void Update()
    {
        CheckGrounded();
        Jump();
        CheckSprint();
        CheckCrouch();
    }

    void FixedUpdate()
    {
        Movement();
        if(isSlide == true){
            SlidingMovement();
        }
    }

    void Movement()
    {
        // MoveSpeed will be equal to sprint speed so side to side only ever gets walk speed
        float xMove = Input.GetAxis("Horizontal") * MoveSpeed;
        float zMove = Input.GetAxis("Vertical") * MoveSpeed;
        if(isSlide == false){
            // Actually apply our movement to our Rigidbody's velocity;
            rb.velocity = transform.TransformDirection( new Vector3(xMove, rb.velocity.y, zMove));

            Vector3 FlatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            // Normalize velocity
            if (FlatVel.magnitude > MoveSpeed){
                Vector3 LimitedVel = FlatVel.normalized;
                rb.velocity = new Vector3(LimitedVel.x * MoveSpeed, rb.velocity.y, LimitedVel.z * MoveSpeed);
            }
            Velocity = rb.velocity.magnitude;
        }
    }

    void CheckSprint()
    {
        // Check to see if the player is moving forward and pressing shift, if so make MoveSpeed = RunSpeed;
        if(Input.GetAxis("Vertical") > 0 && Input.GetKeyDown("left shift") || isSprint == true && Input.GetAxis("Vertical") > 0){
            MoveSpeed = RunSpeed;
            isSprint = true;
        // }else if(Input.GetAxis("Vertical") > 0 && isSprint == true){
        //     return;
        }else{
            MoveSpeed = WalkSpeed;
            isSprint = false;
        }
    }

    void CheckCrouch()
    {
        if(Input.GetButton("Fire1") && isSprint == false && isGrounded == true)
        {
            isCrouched = true;
            DesiredHeight = CrouchHeight;
            MoveSpeed = CrouchMoveSpeed;
        }
        else if(Input.GetKeyDown("left ctrl") && isSprint == true && isGrounded == true && isSlide == false)
        {
            // isSprint = false;
            StartSlide();
        }
        else
        {   
            if(Physics.Raycast(transform.position, transform.up, StandHeight-transform.position.y) == false)
            {
                isCrouched = false;
                DesiredHeight = StandHeight;
            }
        }

        if(PlayerCollider.height != DesiredHeight && isSlide == false){
            AdjustHeight(DesiredHeight, CrouchSpeed);
        }
    }

    void AdjustHeight(float height, float speed)
    {
        // Vector3 NewHeight = new Vector3(transform.localScale.x, height, transform.localScale.z);
        // this.gameObject.transform.localScale = Vector3.Lerp(transform.localScale, NewHeight, CrouchSpeed);

        PlayerCollider.height = Mathf.Lerp(PlayerCollider.height, height, speed); 
    }

    void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(transform.position, GetComponent<CapsuleCollider>().height/2 + .2f , GroundLayer);
    }

    void Jump()
    {
        if(Input.GetKeyDown("space") && isGrounded){
            // Reset y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // Apply JumpForce
            rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
        }
    }

    // Start the slide and reset slide timer;
    void StartSlide()
    {
        isSlide = true;
        isSprint = false;
        AdjustHeight(CrouchHeight, toSlideSpeed);
        SlideTimer = MaxSlideTimer;
        JumpForce = JumpForceSlide;
    }

    // Called from fixed update, adds slide force and decremnents timer
    void SlidingMovement()
    {
        rb.AddForce(transform.forward * SlideForce, ForceMode.Force);
        SlideTimer -= Time.deltaTime;

        if(SlideTimer <= 0){
            StopSlide();
        }
    }
    
    // Stop the slide
    void StopSlide()
    {
        isSlide = false;
        JumpForce = JumpForceStand;
        // If player is crouching at the end of the slide, keep them crouched
        if(Input.GetButton("Fire1") != true){
            AdjustHeight(StandHeight, toSlideSpeed);
        }
        // If player is holding sprint at the end of the slide set them sprinting
        if(Input.GetKey("left shift")){
            isSprint = true;
        }
    }

}
