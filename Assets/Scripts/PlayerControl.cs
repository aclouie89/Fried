using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player ID enum
enum PlayerNum {None = 0, One, Two}

// Player Controller
/* - Handles Movement
 */
public class PlayerControl : MonoBehaviour
{
  // Character related
  public int player_id = (int)PlayerNum.None;
  private string p_vertical = "Vertical";
  private string p_horizontal = "Horizontal";

  // Movement related
  CharacterController controller;
  public float movementSpeed = 5.0F;
  public float jumpSpeed = 8.0F;
  public float gravity = 32.0F;
  //public float rotateSpeed = 3.0f;
  private Vector3 moveDirection = Vector3.zero;
  private bool movementEnabled = true;

  // Starting function
  void Start()
  {
    controller = GetComponent<CharacterController>();

    // set player one
    if (player_id == (int)PlayerNum.One)
    {
      p_vertical = "P1Vertical";
      p_horizontal = "P1Horizontal";
    }
    else if (player_id == (int)PlayerNum.Two)
    {
      p_vertical = "P2Vertical";
      p_horizontal = "P2Horizontal";
    }
  }

  // allow movement
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

}
