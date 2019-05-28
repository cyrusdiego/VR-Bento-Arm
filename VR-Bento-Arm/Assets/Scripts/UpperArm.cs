/* 
    BLINC LAB VR-BENTO-ARM Project
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
    private bool collidedOnce = false;

    void OnTriggerEnter(Collider other)
    {
        if(!collidedOnce)
        {
            print("shoulder collided");
            print("/////////////////////////////////////////////");
            Debug.Break();
            msg = new Tuple<string,bool>("Shoulder", true);
            Rotations.SendMessage("CollisionDetection",msg);
        }
    }

    void OnTriggerExit(Collider other)
    {
        msg = new Tuple<string,bool>("Shoulder", false);
        Rotations.SendMessage("CollisionDetection", msg);
        collidedOnce = false;
    }
}
