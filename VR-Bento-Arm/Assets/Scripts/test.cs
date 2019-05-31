using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject parent = null;
    void Start()
    {
        gameObject.transform.parent = parent.transform;
    }

    void FixedUpdate()
    {
        
    }
    
    void OnCollisionEnter(Collision other)
    {

    }
    
}
