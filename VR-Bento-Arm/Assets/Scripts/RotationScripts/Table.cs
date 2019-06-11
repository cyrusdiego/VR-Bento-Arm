using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().ResetInertiaTensor();
        gameObject.GetComponent<Rigidbody>().ResetCenterOfMass();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
