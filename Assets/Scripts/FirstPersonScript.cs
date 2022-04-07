using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonScript : MonoBehaviour {
    Transform PlayerCamera;
    CharacterController PlayerController;

    float CameraYaw = 0.0f;
    float CameraPitch = 0.0f;
    float MouseSensitivityX = 1000.0f;
    float MouseSensitivityY = 1000.0f;

    [SerializeField] [Range(0.0f, 1.0f)] float MoveSmooth = 0.3f;
    Vector3 CurrentDirection = Vector2.zero;
    Vector3 CurrentDirVelocity = Vector2.zero;

    [SerializeField] float PlayerSpeed = 10.0f;

    bool LockCursor = true;

    // Variables for jump
    bool isGrounded;
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask GroundMask;
    [SerializeField] float gravity;
    [SerializeField] float JumpHeight;
    Vector3 UpVelovity;

    void Start() {
        PlayerCamera = GetComponentInChildren<Camera>().transform;
        PlayerController = GetComponent<CharacterController>();

        if (LockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


    void Update() {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, GroundMask);

        if (isGrounded && UpVelovity.y <= 0) {
            UpVelovity.y = -2.0f;
        }

        // Reading mouse x movement and applying to the character yaw
        CameraYaw = Input.GetAxis("Mouse X") * MouseSensitivityX * Time.deltaTime;
        transform.Rotate(Vector3.up * CameraYaw);

        //Reading mouse Y movemnt and applying to camera pitch
        CameraPitch -= Input.GetAxis("Mouse Y") * MouseSensitivityY * Time.deltaTime;
        CameraPitch = Mathf.Clamp(CameraPitch, -90.0f, 90.0f);
        PlayerCamera.localEulerAngles = Vector3.right * CameraPitch;

        //Reading keyboard input to apply for character movement
        float HorizontalMove = Input.GetAxis("Horizontal");
        float VerticalMove = Input.GetAxis("Vertical");
        Vector3 Direction = new Vector3(HorizontalMove, 0, VerticalMove);
        Direction.Normalize();
        //Smoothing the direction vector
        CurrentDirection = Vector3.SmoothDamp(CurrentDirection, Direction, ref CurrentDirVelocity, MoveSmooth);

        Vector3 Velocity = transform.forward * CurrentDirection.z + transform.right * CurrentDirection.x;
        PlayerController.Move(Velocity * Time.deltaTime * PlayerSpeed);

        // Adding jump velocity to the CharacterController
        if (isGrounded) {
            if (Input.GetKey(KeyCode.Space)) {
                UpVelovity.y = Mathf.Sqrt(JumpHeight * -2 * gravity);
            }
        }

        UpVelovity.y += gravity * Time.deltaTime;
        PlayerController.Move(UpVelovity * Time.deltaTime);//Applying player y movement
    }
}

