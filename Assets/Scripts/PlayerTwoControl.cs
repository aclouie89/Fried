using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoControl : MonoBehaviour
{
    CharacterController p2controller;
    public float movementSpeed = 5.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 32.0F;
    //public float rotateSpeed = 3.0f;
    private Vector3 moveDirection = Vector3.zero;
    private bool movementEnabled = true;

    void Start()
    {
        p2controller = GetComponent<CharacterController>();
    }

    void allowMovement(bool flag)
    {
        movementEnabled = flag;
    }

    // non oriented movement
    void Update()
    {
        // only change movement when grounded
        if (p2controller.isGrounded)
        {
            moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
            // move forward
            if (Input.GetButton("P2Vertical"))
            {
                moveDirection.z += Input.GetAxis("P2Vertical");
            }
            // move sideways
            if (Input.GetButton("P2Horizontal"))
            {
                moveDirection.x += Input.GetAxis("P2Horizontal");
            }
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= movementSpeed;
            /*if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
                */
        }

        // move character
        moveDirection.y -= gravity * Time.deltaTime;
        p2controller.Move(moveDirection * Time.deltaTime);

    }

}
