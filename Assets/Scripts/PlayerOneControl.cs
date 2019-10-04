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
            if(Input.GetKey("s"))
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

    /*
    // rotational - oriented movement
    void Update()
    {
        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = movementSpeed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);
    }*/

    /*
    public float movementSpeed = 10.0f;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w"))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed * 2.5f;
        }
        else if (Input.GetKey("w") && !Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetKey("s"))
        {
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        }

        if (Input.GetKey("a") && !Input.GetKey("d"))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetKey("d") && !Input.GetKey("a"))
        {
            transform.position -= transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        }
    }*/


    //public float inputDelay = 0.1f;
    //public float forwardVel = 12;
    //public float rotateVel = 100;

    //Quaternion targetRotation;
    //Rigidbody rbody;

    //float forwardInput, turnInput;

    //public Quaternion TargetRotation
    //{
    //    get { return targetRotation; }
    //}
    //// Start is called before the first frame update
    //void Start()
    //{
    //    targetRotation = transform.rotation;
    //    if (GetComponent<Rigidbody>())
    //        rbody = GetComponent<Rigidbody>();
    //    else
    //        Debug.LogError("The character needs a rigidbody");

    //    forwardInput = turnInput = 0;
    //}

    //void GetInput()
    //{
    //    forwardInput = Input.GetAxis("Vertical");
    //    turnInput = Input.GetAxis("Horizontal");
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    GetInput();
    //    Turn();
    //}

    //void FixedUpdate()
    //{
    //    Run();
    //}

    //void Run()
    //{
    //    if (Mathf.Abs(forwardInput) > inputDelay)
    //    {
    //        rbody.velocity = transform.forward * forwardInput * forwardVel;
    //    }
    //    else   
    //        rbody.velocity = Vector3.zero;
    //}

    //void Turn()
    //{
    //    if (Mathf.Abs(turnInput) > inputDelay)
    //    {
    //        targetRotation *= Quaternion.AngleAxis(rotateVel * turnInput * Time.deltaTime, Vector3.up);
    //    }
    //    transform.rotation = targetRotation;
    //}
}
