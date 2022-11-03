using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Movement Variables")]
    public float Sensitivity;
    public Rigidbody rb;
    public Transform PlayerBody;
    public GameObject Player;
    public GameObject Cam;
    float xRotation = 0f;


    void Start()
    {
        // Lock the cursor to our game screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        Look();
    }

    // This method moves the camera so it is alligned with the player

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


}
