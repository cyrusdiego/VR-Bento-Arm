using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private Rigidbody rb = null;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddRelativeTorque(0,500,0);
    }
    
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision");
    }
    
}
