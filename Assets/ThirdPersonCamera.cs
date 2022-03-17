using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField]
    Transform PlayerTransform;
    Transform CameraTransform;

    float MouseMin = 14.0f;
    float MouseMax = 50.0f;
    Vector3 CameraOffset;
    Vector3 CameraLookat;
    float mouseX;
    float mouseY;
    void Start()
    {
        CameraTransform = transform;
        CameraOffset = new Vector3(0, 1.75f, -3.35f);
        CameraLookat = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y + 1.5f, PlayerTransform.position.z);
    }

    void Update()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, MouseMin, MouseMax);
    }

    void LateUpdate() {
        Quaternion camRotation = Quaternion.Euler(mouseY, mouseX, 0);
        CameraTransform.position = PlayerTransform.position + camRotation * CameraOffset;
        CameraTransform.LookAt(CameraLookat);
        //Debug.Log(camRotation);
    }
}
