using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonScript : MonoBehaviour {
    [SerializeField]
    Transform PlayerCamera;

    CharacterController ThirdPersonController;
    float CharacterSpeed = 4;
    float HorizontalMove;
    float VerticalMove;
    float TurnSmoothVelocity;
    float TurnSmoothTime = 0.25f;

    //variables for jump
    float JumpSpeed;
    bool isGrounded;
    float GroundCheckDistance;
    LayerMask GroundMask;
    float gravity;
    float JumpHeight;
    Vector3 JumpVelocity;

    void Start() {
        ThirdPersonController = GetComponent<CharacterController>();
    }

    void Update() {
        //Getting move button inputs
        HorizontalMove = Input.GetAxis("Horizontal");
        VerticalMove = Input.GetAxis("Vertical");
        //Creating a vector from the inputs
        Vector3 direction = new Vector3(HorizontalMove, 0, VerticalMove).normalized;

        //Checking if any input values are coming in. (Magnitude is square root of the total of the squares of of the individual vector components)
        if (direction.magnitude > 0) {
            //Get angle from x and z in Radians, convert to Degree, add camera rotation on y axis
            float TargetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + PlayerCamera.eulerAngles.y;

            //Smooth the angle with the SmoothDampAngle method
            float SmoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngle, ref TurnSmoothVelocity, TurnSmoothTime);

            //Rotate player to the smoothed angle
            transform.rotation = Quaternion.Euler(0, SmoothAngle, 0);

            //Apply smoothed angle to forward vector and create a direction vector
            Vector3 MoveDir = Quaternion.Euler(0f, SmoothAngle, 0f) * Vector3.forward;

            //Apply move to the Character Controller
            ThirdPersonController.Move(MoveDir * Time.deltaTime * CharacterSpeed);
        }
        Jump();
    }

    void Jump() {
        //TODO next week.

        isGrounded = Physics.CheckSphere(transform.position, GroundCheckDistance, GroundMask);

        if (!isGrounded) {
            JumpVelocity.y = -10.0f;
        }

        ThirdPersonController.Move(JumpVelocity * Time.deltaTime);

        //if (Input.GetKeyDown("Jump")) {
        //    Vector2 JumpVelocityToAdd = new Vector2(0f, JumpSpeed);

        //}
    }
}
