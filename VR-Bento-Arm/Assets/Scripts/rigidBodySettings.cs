using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigidBodySettings : MonoBehaviour
{
    private Rigidbody rb = null;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0,0,0);
        rb.inertiaTensor = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
