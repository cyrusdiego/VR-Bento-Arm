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
            if(other.tag != "Interactable")
            {
                print("triggered");
                msg = new Tuple<VRrotations.modes, bool>(VRrotations.modes.Elbow, true);
                Rotations.SendMessage("CollisionDetection",msg);
            }
            
            collidedObjs.Add(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        collidedObjs.Remove(other);
        if(other.tag != "Interactable")
        {
            msg = new Tuple<VRrotations.modes, bool>(VRrotations.modes.Elbow, false);
            Rotations.SendMessage("CollisionDetection",msg);
        }
    }
}
