/* 
    BLINC LAB VR-BENTO-ARM Project
    CollisionRightChopsticks.cs
    Created by: Cyrus Diego May 21, 2019 

    Sends message to controller script if it has collided with something
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRightChopstick : MonoBehaviour
{
    public GameObject Rotations = null;
    private Tuple<string,bool> msg;
    private bool collidedOnce = false;

    void OnTriggerEnter(Collider other)
    {
        if(!collidedOnce)
        {
            msg = new Tuple<string, bool>("Open Hand", true);
            Rotations.SendMessage("CollisionDetection",msg);
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        msg = new Tuple<string, bool>("Open Hand", false);
        Rotations.SendMessage("CollisionDetection", msg);
        collidedOnce = false;
    }
}
