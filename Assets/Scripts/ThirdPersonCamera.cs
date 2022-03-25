using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField]
    Transform PlayerTransform;
    Transform CameraTransform;

    float MouseMin = -20.0f; 
    float MouseMax = 65.0f;

    Vector3 CameraOffset;
    Vector3 CameraLookat;

    float CameraLookHeight = 1.4f;
    float mouseX;
    float mouseY;
    void Start()
    {
        // getting the transform value of camera.
        CameraTransform = transform;
        //Set the camera from a distance to player
        CameraOffset = new Vector3(0, 1.0f, -2.4f);
    }

    void Update()
    {
        //Get mouse input 
        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");
        // limiting how far camera can go up and down
        mouseY = Mathf.Clamp(mouseY, MouseMin, MouseMax); 
    }

    void LateUpdate() {
        // Applying mouse position to a Quaternion rotation.
        Quaternion camRotation = Quaternion.Euler(mouseY, mouseX, 0);
        //Positioning camera around the player using Quaternion * operator
        //https://docs.unity3d.com/ScriptReference/Quaternion-operator_multiply.html
        CameraTransform.position = PlayerTransform.position + camRotation * CameraOffset;
        //Creating a look at position from player position and adding height.
        CameraLookat = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y + CameraLookHeight, PlayerTransform.position.z);
        //Rotating camera to look at a position
        CameraTransform.LookAt(CameraLookat);
        //Debug.Log(camRotation);
    }
}
