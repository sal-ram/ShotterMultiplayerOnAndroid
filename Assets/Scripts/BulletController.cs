using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private int shotSpeed = 200;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * shotSpeed, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * shotSpeed * Time.deltaTime);
        //GetComponent<Rigidbody>().AddForceAtPosition(transform.forward, transform.position);
        GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * shotSpeed, transform.position);
    }

    public void Shot()
    {
    }
}
