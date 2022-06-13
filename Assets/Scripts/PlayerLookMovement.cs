using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookMovement : MonoBehaviour
{
    [SerializeField] private float mouseSensivityHoriz = 125f;
    [SerializeField] private float mouseSensivityVertic = 2.5f;
    [SerializeField] private Transform bodyObj;
    [SerializeField] private Transform gunObj;
    // [SerializeField] private Transform gunObj;

    float Xrotation = 0f;
    private void Update()
    {
        
    }
    public void MoveLookByMouse(float vertical, float horizontal)
    {
        //Debug.Log(vertical + " " + horizontal);
        Xrotation -= vertical;
        Xrotation = Mathf.Clamp(Xrotation, -90f, 90f);

        /*if (vertical > 0)
            horizontal = 1;
        else if (horizontal < 0)
            horizontal = -1;
        */

        Quaternion PlayerLookEulerRotation = Quaternion.Euler(Xrotation * mouseSensivityVertic, 0f, 0f);

        transform.localRotation = PlayerLookEulerRotation;
        gunObj.localRotation = PlayerLookEulerRotation;

        bodyObj.Rotate(Vector3.up * horizontal * mouseSensivityHoriz * Time.deltaTime);
    }
}
