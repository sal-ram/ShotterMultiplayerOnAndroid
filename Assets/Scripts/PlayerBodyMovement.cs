using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyMovement : MonoBehaviour
{
    private float speedMovement = 15f;
    public CharacterController controller;
    float jumpHeight = 1f;

    public void MoveBody(float vertical, float horizontal)
    {
       // Debug.Log("Here");
        Vector3 moveCoordinate = transform.right * vertical + transform.forward * horizontal;
        controller.Move(moveCoordinate * Time.deltaTime * speedMovement);
    }

    public void Jump()
    {
        if (PlayerGravityController.isGrounded)
        {
           Debug.Log(2);
            PlayerGravityController.velocity.y = Mathf.Sqrt(jumpHeight * -2f * PlayerGravityController.gravity);
        }
    }
}
