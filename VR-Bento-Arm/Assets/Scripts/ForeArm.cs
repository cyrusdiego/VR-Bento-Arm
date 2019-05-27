using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeArm : MonoBehaviour
{
    public GameObject Rotations = null;
    private Tuple<string,bool> msg;

    void OnTriggerEnter(Collider other)
    {
        msg = new Tuple<string, bool>("Elbow", true);
        Rotations.SendMessage("CollisionDetection",msg);
    }

    void OnTriggerExit(Collider other)
    {
        msg = new Tuple<string, bool>("Elbow", false);
        Rotations.SendMessage("CollisionDetection", msg);
    }

    void OnCollisionEnter(Collision collision) 
    {
        msg = new Tuple<string, bool>("Elbow", true);
        Rotations.SendMessage("CollisionDetection",msg);
    }

    void OnCollisionExit(Collision collision) 
    {
        msg = new Tuple<string, bool>("Elbow", false);
        Rotations.SendMessage("CollisionDetection", msg);
    }
}
