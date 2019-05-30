/* 
    BLINC LAB VIPER PROJECT 
    UpperArm.cs
    Created by: Cyrus Diego May 21, 2019 

    Sends message to controller script if it has collided with something
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperArm: MonoBehaviour
{
    // public Transform UpperArmShellTransform = null;
    public GameObject Rotations = null;
    private Tuple<string,bool> msg;

    void OnTriggerEnter(Collider other)
    {   
        print("upper arm triggered");
        print("the collider was " + other.gameObject.transform.parent.gameObject);
        print("tag was " + other.tag);
        if(other.tag == "test")
        {
            return;
        }
        if(other.tag != "BentoArm")
        {
            msg = new Tuple<string,bool>("Shoulder", true);
            Rotations.SendMessage("CollisionDetection",msg);
        }

    }

    void OnTriggerExit(Collider other)
    {        
        if(other.tag == "test")
        {
            return;
        }
        if(other.tag != "BentoArm")
        {
        msg = new Tuple<string,bool>("Shoulder", false);
        Rotations.SendMessage("CollisionDetection", msg);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        print("upper collided with" + other);

        if(other.gameObject.tag != "BentoArm")
        {
            msg = new Tuple<string,bool>("Shoulder", true);
            Rotations.SendMessage("CollisionDetection",msg);
        }
    }

    /// <summary>
    /// OnCollisionExit is called when this collider/rigidbody has
    /// stopped touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionExit(Collision other)
    {

        if(other.gameObject.tag != "BentoArm")
        {
            msg = new Tuple<string,bool>("Shoulder", true);
            Rotations.SendMessage("CollisionDetection",msg);
        }
    }
}
