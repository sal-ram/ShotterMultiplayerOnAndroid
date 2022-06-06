using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookMovement : MonoBehaviour
{
    private float mouseSensivity = 50f;
    [SerializeField] private Transform bodyObj;
   // [SerializeField] private Transform gunObj;

    float Xrotation = 0f;
    private void Update()
    {
        
    }
    public void MoveLookByMouse(float vertical, float horizontal)
    {
        //Debug.Log(vertical + " " + horizontal);
        Xrotation -= vertical * (float)0.3;
        Xrotation = Mathf.Clamp(Xrotation, -90f, 90f);

        /*if (vertical > 0)
            horizontal = 1;
        else if (horizontal < 0)
            horizontal = -1;
        */

        Quaternion PlayerLookEulerRotation = Quaternion.Euler(Xrotation, 0f, 0f);

        transform.localRotation = PlayerLookEulerRotation;
        //gunObj.localRotation = PlayerLookEulerRotation;

        bodyObj.Rotate(Vector3.up * horizontal * mouseSensivity * Time.deltaTime);
    }
}
