using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperArm: MonoBehaviour
{
    public Transform UpperArmShellTransform = null;
    public GameObject Rotations = null;
    private Tuple<string,bool> msg;
    
    // Update is called once per frame
    void FixedUpdate() 
    {
        gameObject.transform.position = UpperArmShellTransform.position;
        gameObject.transform.eulerAngles = UpperArmShellTransform.eulerAngles;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit something");
        msg = new Tuple<string,bool>("Shoulder", true);
        Rotations.SendMessage("CollisionDetection",msg);
    }

    void OnTriggerExit(Collider other)
    {
        msg = new Tuple<string,bool>("Shoulder", false);
        Rotations.SendMessage("CollisionDetection", msg);
    }

    void OnCollisionEnter(Collision collision) 
    {
        msg = new Tuple<string,bool>("Shoulder", true);
        Rotations.SendMessage("CollisionDetection",msg);
    }

    void OnCollisionExit(Collision collision) 
    {
        msg = new Tuple<string,bool>("Shoulder", false);
        Rotations.SendMessage("CollisionDetection", msg);
    }
}
