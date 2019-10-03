using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneControl : MonoBehaviour
{
    public float movementSpeed;

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
    }


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
