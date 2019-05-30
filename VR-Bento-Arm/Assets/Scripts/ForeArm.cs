/* 
    BLINC LAB VIPER PROJECT 
    ForeArm.cs
    Created by: Cyrus Diego May 21, 2019 

    Sends message to controller script if it has collided with something
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeArm : MonoBehaviour
{
    public GameObject Rotations = null;
    private Tuple<string,bool> msg;
    private List<Collider> collidedObjs = new List<Collider>();

    void OnTriggerEnter(Collider other)
    {
        print("trigger in forearm");

        if(collidedObjs.Contains(other))
        {
            return;
        }
        else
        {
            if(other.tag == "Stand")
            {
                msg = new Tuple<string, bool>("Shoulder", true);
                Rotations.SendMessage("CollisionDetection",msg);
            }
            else
            {
                msg = new Tuple<string, bool>("Elbow", true);
                Rotations.SendMessage("CollisionDetection",msg);
            }
            collidedObjs.Add(other);
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if(collidedObjs.Contains(other))
        {
            collidedObjs.Remove(other);
        }
        if(other.tag == "Stand")
        {
            msg = new Tuple<string, bool>("Shoulder", false);
            Rotations.SendMessage("CollisionDetection",msg);
        }
        else
        {
            msg = new Tuple<string, bool>("Elbow", false);
            Rotations.SendMessage("CollisionDetection",msg);
        }
    }
    
    // void OnCollisionEnter(Collision other)
    // {
    //     print("forearm collided with" + other);
    //     msg = new Tuple<string, bool>("Elbow", true);
    //     Rotations.SendMessage("CollisionDetection",msg);
    // }

    // void OnCollisionExit(Collision other)
    // {
    //     msg = new Tuple<string, bool>("Elbow", false);
    //     Rotations.SendMessage("CollisionDetection",msg);
    // }
}
