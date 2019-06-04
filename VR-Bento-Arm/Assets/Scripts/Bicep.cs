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

public class Bicep: MonoBehaviour
{
    // public Transform UpperArmShellTransform = null;
    public GameObject Rotations = null;
    private Tuple<VRrotations.modes,bool> msg;
    private List<Collider> collidedObjs = new List<Collider>();

    void OnTriggerEnter(Collider other)
    {   
        if(collidedObjs.Contains(other))
        {
            return;
        }
        else
        {
            msg = new Tuple<VRrotations.modes,bool>(VRrotations.modes.Shoulder, true);
            Rotations.SendMessage("CollisionDetection",msg);
            collidedObjs.Add(other);
        }
    }

    void OnTriggerExit(Collider other)
    {        
        collidedObjs.Remove(other);
        msg = new Tuple<VRrotations.modes,bool>(VRrotations.modes.Shoulder, false);
        Rotations.SendMessage("CollisionDetection", msg);
    }
}
