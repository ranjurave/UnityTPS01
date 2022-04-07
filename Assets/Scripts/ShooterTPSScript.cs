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

    [SerializeField]float MaxWalkSpeed = 2.0f;
    [SerializeField]float MaxRunSpeed = 8.0f;
    

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

        float currentMaxVelocity = RunPressed ? MaxRunSpeed : MaxWalkSpeed;

        if (ForwardPressed && VelocityY < currentMaxVelocity) {
            VelocityY += Time.deltaTime * accelerator;
        }

        if (LeftPressed && VelocityX > -currentMaxVelocity) {
            VelocityX -= Time.deltaTime * accelerator;
        }

        if (RightPressed && VelocityX < currentMaxVelocity) {
            VelocityX += Time.deltaTime * accelerator;
        }

        if (!ForwardPressed && VelocityY > 0.0) {
            VelocityY -= Time.deltaTime * deccelerator;
        }

        if (!LeftPressed && VelocityX < 0.0) {
            VelocityX += Time.deltaTime * deccelerator;
        }

        if (!RightPressed && VelocityX > 0.0) {
            VelocityX -= Time.deltaTime * deccelerator;
        }

        if (ForwardPressed && !RunPressed && VelocityY > currentMaxVelocity) {
            VelocityY -= Time.deltaTime * deccelerator;
        }

        if (LeftPressed && !RunPressed && VelocityX < -currentMaxVelocity) {
            VelocityX += Time.deltaTime * deccelerator;
        }

        if (RightPressed && !RunPressed && VelocityX > currentMaxVelocity) {
            VelocityX -= Time.deltaTime * deccelerator;
        }

        ShooterAnimator.SetFloat("VelocityY", VelocityY);
        ShooterAnimator.SetFloat("VelocityX", VelocityX);
    }
}