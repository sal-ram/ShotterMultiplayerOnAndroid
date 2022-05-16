using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookMovement : MonoBehaviour
{
    private float mouseSensivity = 200f;
    [SerializeField] private Transform bodyObj;
   // [SerializeField] private Transform gunObj;

    float Xrotation = 0f;
    private void Update()
    {
        
    }
    public void MoveLookByMouse(float vertical, float horizontal)
    {
        Xrotation -= vertical;
        Xrotation = Mathf.Clamp(Xrotation, -90f, 90f);

        Quaternion PlayerLookEulerRotation = Quaternion.Euler(Xrotation, 0f, 0f);

        transform.localRotation = PlayerLookEulerRotation;
        //gunObj.localRotation = PlayerLookEulerRotation;

        bodyObj.Rotate(Vector3.up * horizontal * mouseSensivity * Time.deltaTime);
    }
}
