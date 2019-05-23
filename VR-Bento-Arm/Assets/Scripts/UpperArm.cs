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

    void OnCollisionEnter(Collision collision) 
    {
        Debug.Log("collided");
        msg = new Tuple<string,bool>("Shoulder", true);
        Rotations.SendMessage("CollisionDetection",msg);
    }

    void OnCollisionExit(Collision collision) 
    {
        msg = new Tuple<string,bool>("Shoulder", false);
        Rotations.SendMessage("CollisionDetection", msg);
    }
}
