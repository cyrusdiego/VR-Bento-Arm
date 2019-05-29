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

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "BentoArm")
        {
            msg = new Tuple<string,bool>("Shoulder", true);
            Rotations.SendMessage("CollisionDetection",msg);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag != "BentoArm")
        {
        msg = new Tuple<string,bool>("Shoulder", false);
        Rotations.SendMessage("CollisionDetection", msg);
        }
    }
}
