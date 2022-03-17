using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonScript : MonoBehaviour
{
    CharacterController ThirdPersonController;

    float HorizontalMove;
    float VerticalMove;
    void Start()
    {
        ThirdPersonController = GetComponent<CharacterController>();
    }


    void Update()
    {
        HorizontalMove = Input.GetAxis("Horizontal");
        VerticalMove = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(HorizontalMove, 0, VerticalMove);
        ThirdPersonController.Move(direction);
    }
}
