/* 
    BLINC LAB VIPER PROJECT 
    CollisionRightChopsticks.cs
    Created by: Cyrus Diego May 21, 2019 

    Sends message to controller script if it has collided with something
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// seems to not do anything with the grabber.cs being a child script under it 

public class RightChopstick : MonoBehaviour
{
    public GameObject Rotations = null;
    private Tuple<VRrotations.modes,bool> msg;
    private List<Collision> collisionRightObjs = new List<Collision>();
    private List<Collider> colliderObjs = new List<Collider>();

    void OnCollisionEnter(Collision other)
    {
        if(collisionRightObjs.Contains(other))
        {
            return;
        }   
        else 
        {
            if(other.gameObject.tag != "Interactable")
            {
                msg = new Tuple<VRrotations.modes, bool>(VRrotations.modes.Hand, true);
                Rotations.SendMessage("CollisionDetection",msg);
            }
            collisionRightObjs.Add(other);
        }
    }

    void OnCollisionExit(Collision other)
    {
        collisionRightObjs.Remove(other);
        if(other.gameObject.tag != "Interactable")
        {
            msg = new Tuple<VRrotations.modes, bool>(VRrotations.modes.Hand, false);
            Rotations.SendMessage("CollisionDetection", msg);
        }
    }
}
