using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Rigidbody rb = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        Vector3 impulse; 
        impulse = other.impulse;

        Vector3 force;
        force = impulse / Time.fixedDeltaTime;

        print(force);
    }
}
