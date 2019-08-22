using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Rigidbody rb = null;
    ConfigurableJoint cj = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        cj = gameObject.GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        print(cj.currentTorque);
    }

    // void OnCollisionEnter(Collision other)
    // {
    //     Vector3 impulse; 
    //     impulse = other.impulse;

    //     Vector3 force;
    //     force = impulse / Time.fixedDeltaTime;

    //     print(force);
    // }
}
