using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTPSScript : MonoBehaviour
{
    Animator ShooterAnimator;
    float VelocityY = 0.0f;
    float VelocityX = 0.0f;
    float accelerator = 2.0f;
    float deccelerator = 2.0f;
    void Start()
    {
        ShooterAnimator = GetComponentInChildren<Animator>();    
    }

    void Update()
    {
        bool ForwardPressed = Input.GetKey(KeyCode.W);
        bool LeftPressed = Input.GetKey(KeyCode.A);
        bool RightPressed = Input.GetKey(KeyCode.D);
        bool RunPressed = Input.GetKey(KeyCode.LeftShift);

        if (ForwardPressed && VelocityY < 0.8f) {
            VelocityY += Time.deltaTime * accelerator;
        }

        if (LeftPressed && VelocityX > -0.8f) {
            VelocityX -= Time.deltaTime * accelerator;
        }

        if (RightPressed && VelocityX < 0.8f) {
            VelocityX += Time.deltaTime * accelerator;
        }

        ShooterAnimator.SetFloat("VelocityY", VelocityY);
        ShooterAnimator.SetFloat("VelocityX", VelocityX);
    }
}