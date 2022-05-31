using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityController : MonoBehaviour
{
    [HideInInspector] public static float gravity { get; } = -9.81f;

    public Transform groundCheck;
    private float radiusDistance = 0.4f;
    public LayerMask groundMask;
    [SerializeField] private CharacterController controller;
    [HideInInspector] public static Vector3 velocity;
    [HideInInspector] public static bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, radiusDistance, groundMask);
        //Debug.Log(isGrounded);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime; 
        controller.Move(velocity * 4 * Time.deltaTime);
    }
}
