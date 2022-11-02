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
    
    [Header("Jump Check variables")]  
    public LayerMask GroundLayer;
    public bool isGrounded;
    public float JumpForce;

    [Header("Crouch variables")]
    public float StandHeight;
    public float CrouchHeight;
    private float DesiredHeight;
    public float CrouchSpeed;
    private bool isCrouched;
    public GameObject Arms;

    [Header("Camera Movement Variables")]
    public float Sensitivity;
    public Rigidbody rb;
    public Transform PlayerBody;
    public GameObject Cam;
    float xRotation = 0f;


    


    void Start()
    {
        // Lock the cursor to our game screen
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Look();
        CheckGrounded();
        Jump();
    }

    void FixedUpdate()
    {
        Movement();
        Look();
    }

    // This method manages the Camera Movement of our FPS Player
    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * Sensitivity; //* Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Sensitivity; // * Time.deltaTime; 

        xRotation -= mouseY;
        // Prevents the player from spinning on the x axis rotation
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        Cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * mouseX);
        
    }

    void Movement()
    {
        CheckSprint();
        CheckCrouch();

        // MoveSpeed will be equal to sprint speed so side to side only ever gets walk speed
        float xMove = Input.GetAxis("Horizontal") * MoveSpeed;
        float zMove = Input.GetAxis("Vertical") * MoveSpeed;
        
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

    void CheckSprint()
    {
        // Check to see if the player is moving forward and pressing shift, if so make MoveSpeed = RunSpeed;
        if(Input.GetAxis("Vertical") > 0 && Input.GetKey("left shift")){
            MoveSpeed = RunSpeed;
            isSprint = true;
        }else{
            MoveSpeed = WalkSpeed;
            isSprint = false;
        }
    }

    void CheckCrouch()
    {
        if(Input.GetButton("Fire1") && isSprint == false)
        {
            isCrouched = true;
            DesiredHeight = CrouchHeight;
            MoveSpeed = CrouchMoveSpeed;
        }
        else{
            isCrouched = false;
            DesiredHeight = StandHeight;
        }

        if(transform.localScale.y != DesiredHeight){
            AdjustHeight(DesiredHeight);
        }
    }

    void AdjustHeight(float height)
    {
        Vector3 NewHeight = new Vector3(transform.localScale.x, height, transform.localScale.z);
        this.gameObject.transform.localScale = Vector3.Lerp(transform.localScale, NewHeight, CrouchSpeed);
    }

    void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(transform.position, GetComponent<CapsuleCollider>().height + .1f , GroundLayer);
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
}
