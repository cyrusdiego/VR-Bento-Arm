/* 
    BLINC LAB VR-BENTO-ARM Project
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
    private bool collidedOnce = false;

    void OnTriggerEnter(Collider other)
    {
        if(!collidedOnce)
        {
            msg = new Tuple<string, bool>("Elbow", true);
            Rotations.SendMessage("CollisionDetection",msg);
        }
    }

    void OnTriggerExit(Collider other)
    {
        msg = new Tuple<string, bool>("Elbow", false);
        Rotations.SendMessage("CollisionDetection", msg);
        collidedOnce = true;
    }
}
