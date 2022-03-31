using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonScript : MonoBehaviour {
    CharacterController ThirdPersonController;
    Animator ThirdPersonAnimator;

    [SerializeField] GameObject Bullet;
    [SerializeField] Transform BulletStartPoint;

    [SerializeField] Transform PlayerCamera;
    [SerializeField] float CharacterSpeed = 1;
    float HorizontalMove;
    float VerticalMove;
    bool Sprint;
    float TurnSmoothVelocity;
    float TurnSmoothTime = 0.25f;

    bool lockCursor = true;

    //variables for jump
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    float groundedCheckDistance = 0.1f;
    float gravity = -9.8f;
    float jumpHeight = 5.0f;
    Vector3 velocity;

    void Start() {
        ThirdPersonController = GetComponent<CharacterController>();
        ThirdPersonAnimator = GetComponentInChildren<Animator>();


    }

    void Update() {
        Fire();
        Movement();
        Jump();
        MouseState();
    }

    private void MouseState() {
        if (lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Fire() {
        if (Input.GetMouseButtonDown(0)) {
            ThirdPersonAnimator.SetBool("FireAnim", true);
            Instantiate(Bullet, BulletStartPoint.position, BulletStartPoint.rotation);
        }
        else {
            ThirdPersonAnimator.SetBool("FireAnim", false);
        }
    }

    void Movement() {
        //Getting move button inputs
        HorizontalMove = Input.GetAxis("Horizontal");
        VerticalMove = Input.GetAxis("Vertical");

        //Shift button to run faster
        Sprint = Input.GetKey(KeyCode.LeftShift);
        if (Sprint) {
            ThirdPersonAnimator.SetBool("CanRun", true);
            CharacterSpeed = 4;
        }
        else {
            ThirdPersonAnimator.SetBool("CanRun", false);
            CharacterSpeed = 1;
        }

        //Creating a vector from the inputs
        Vector3 direction = new Vector3(HorizontalMove, 0, VerticalMove).normalized;

        //Checking if any input values are coming in. (Magnitude is square root of the total of the squares of of the individual vector components)
        if (direction.magnitude > 0) {
            ThirdPersonAnimator.SetBool("CanWalk", true);

            //Get angle from x and z in Radians, convert to Degree, add camera rotation on y axis
            float TargetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + PlayerCamera.eulerAngles.y;

            //Smooth the angle with the SmoothDampAngle method
            float SmoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngle, ref TurnSmoothVelocity, TurnSmoothTime);

            //Rotate player to the smoothed angle
            transform.rotation = Quaternion.Euler(0, SmoothAngle, 0);

            //Apply smoothed angle to forward vector and create a direction vector
            Vector3 MoveDir = Quaternion.Euler(0f, SmoothAngle, 0f) * Vector3.forward;

            //Apply move to the Character Controller
            ThirdPersonController.Move(MoveDir  * Time.deltaTime * CharacterSpeed);
        }
        else {
            // Changing transition bool to false to set animation to idle 
            ThirdPersonAnimator.SetBool("CanWalk", false);
        }
    }

    void Jump() {
        // Check if the player is on ground
        isGrounded = Physics.CheckSphere(transform.position, groundedCheckDistance, groundMask);
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2.0f; // Always applying a negative y value so it falls off from the edge of platforms (not snap to floor)
        }
        velocity.y += gravity * Time.deltaTime; // adding gravity
        ThirdPersonController.Move(velocity * Time.deltaTime); // applying the velocity to character controller

        if (Input.GetKey(KeyCode.Space)) {
            if (isGrounded) { // can jump only if grounded
                velocity.y = jumpHeight;
            }
        }
    }
}