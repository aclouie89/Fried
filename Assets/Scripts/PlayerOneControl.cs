using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneControl : MonoBehaviour
{
    CharacterController controller;
    public float movementSpeed = 5.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 32.0F;
    //public float rotateSpeed = 3.0f;
    private Vector3 moveDirection = Vector3.zero;
    private bool movementEnabled = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void allowMovement(bool flag)
    {
        movementEnabled = flag;
    }

    // non oriented movement
    void Update()
    {
        // only change movement when grounded
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
            // move forward
            if (Input.GetKey("w"))
            {
                moveDirection.z += Input.GetAxis("Vertical");
            }
            if (Input.GetKey("s"))
            {
                moveDirection.z += Input.GetAxis("Vertical");
            }
            if (Input.GetKey("a"))
            {
                moveDirection.x += Input.GetAxis("Horizontal");
            }
            if (Input.GetKey("d"))
            {
                moveDirection.x += Input.GetAxis("Horizontal");
            }
            //if (input.GetKeyDown(KeyCode.A))
            /*
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            */
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= movementSpeed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }

        // move character
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

    }

}
