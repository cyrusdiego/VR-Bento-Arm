using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        print("got a trigger with " + other.gameObject);
    }    
}
