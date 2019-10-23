using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player ID enum
enum PlayerNum {None = 0, One, Two}
// Player status
enum PlayerStatus {Start = 0, Normal, iFrame, CC}

// Player Controller
/* - Handles Movement
 */
public class PlayerControl : MonoBehaviour
{
  // Character related
  public int player_id = (int)PlayerNum.None;
  private string p_vertical = "Vertical";
  private string p_horizontal = "Horizontal";
  private string p_iframe_key = "iFrame_Key";

  // Movement related
  CharacterController controller;
  private float movementSpeed = 4.0F;
  private float jumpSpeed = 8.0F;
  private float gravity = 32.0F;
  private float rotateSpeed = 1800.0f;
  private Vector3 moveDirection = Vector3.zero;
  private bool movementEnabled = true;

  private int status = (int)PlayerStatus.Start;
  private float iFrame_time = 0.5f;
  private float DoubleTapCD = 0.5f;
  private int DoubleTapCount = 0;

  // Starting function
  void Start()
  {
    controller = GetComponent<CharacterController>();

    // set player one
    if (player_id == (int)PlayerNum.One)
    {
      p_vertical = "P1Vertical";
      p_horizontal = "P1Horizontal";
      p_iframe_key = "P1_iFrame_Key";
    }
    else if (player_id == (int)PlayerNum.Two)
    {
      p_vertical = "P2Vertical";
      p_horizontal = "P2Horizontal";
      p_iframe_key = "P2_iFrame_Key";
    }

    status = (int)PlayerStatus.Normal;
  }

  // allow movement
  void allowMovement(bool flag)
  {
    movementEnabled = flag;
  }

  private void Movement()
  {
    // only change movement when grounded
    if (controller.isGrounded)
    {
        moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
        // move forward
        if (Input.GetButton(p_vertical))
        {
            moveDirection.z += Input.GetAxis(p_vertical);
        }
        // move sideways
        if (Input.GetButton(p_horizontal))
        {
            moveDirection.x += Input.GetAxis(p_horizontal);
        }
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= movementSpeed;
        /*if (Input.GetButton("Jump"))
            moveDirection.y = jumpSpeed;
            */

    }

    // move character
    moveDirection.y -= gravity * Time.deltaTime;
    controller.Move(moveDirection * Time.deltaTime);
  }

  private IEnumerator iFrameRotate()
  {
    float cur_time = iFrame_time;
    //Debug.Log("iFraming");
    while(cur_time > 0)
    {
      transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
      cur_time -= Time.deltaTime;
      yield return null;
    }
    //Debug.Log("Normal");
    status = (int)PlayerStatus.Normal;
    transform.rotation = Quaternion.identity;
  }

  private void iFrame()
  {
    if (controller.isGrounded)
    {
      if (Input.GetButtonDown(p_iframe_key) && status == (int)PlayerStatus.Normal)
      {
        status = (int)PlayerStatus.iFrame;
        StartCoroutine(iFrameRotate());
        //iFrameRotate();
        // Rotate back to normal
      }
    }
  }

  // non oriented movement
  void Update()
  {
    if (status == (int)PlayerStatus.Normal)
    {
      iFrame();
      Movement();
    }
  }

}
