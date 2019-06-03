/* 
    BLINC LAB VIPER PROJECT 
    CollisionLeftChopsticks.cs
    Created by: Cyrus Diego May 21, 2019 

    Sends message to controller script if it has collided with something
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftChopstick : MonoBehaviour
{
    private Tuple<VRrotations.modes,bool> msg;
    public GameObject Rotations = null;
    private List<Collision> collisionLeftObjs = new List<Collision>();
    private List<Collider> colliderObjs = new List<Collider>();
    protected bool leftBool = false;
    public GameObject grabber = null;

    void OnTriggerEnter(Collider other)
    {
        // print("got a trigger in wrist flexion object");
        if(colliderObjs.Contains(other))
        {
            return;
        }   
        else 
        {
            if(other.gameObject.tag != "Interactable" && other.gameObject.tag != "Gripper")
            {
                msg = new Tuple<VRrotations.modes,bool>(VRrotations.modes.Wrist, true);
                Rotations.SendMessage("CollisionDetection", msg);
            }
            colliderObjs.Add(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        colliderObjs.Remove(other);
        if(other.gameObject.tag != "Interactable")
        {
            msg = new Tuple<VRrotations.modes,bool>(VRrotations.modes.Wrist, false);
            Rotations.SendMessage("CollisionDetection", msg);
        }
    }

    void OnCollisionEnter(Collision other)
    {     
        // print("collision detected: " + other);
        // print("got a collision in wrist flexion object");
        if(collisionLeftObjs.Contains(other))
        {
            return;
        }   
        else 
        {
            if(other.gameObject.tag != "Interactable")
            {
                msg = new Tuple<VRrotations.modes,bool>(VRrotations.modes.Wrist, true);
                Rotations.SendMessage("CollisionDetection", msg);
            }
            else
            {
                leftBool = true;
                grabber.SendMessage("LeftBool",leftBool);
            }
            collisionLeftObjs.Add(other);
        }
    }

    void OnCollisionExit(Collision other)
    {
        collisionLeftObjs.Remove(other);
        if(other.gameObject.tag != "Interactable")
        {
            msg = new Tuple<VRrotations.modes,bool>(VRrotations.modes.Wrist, false);
            Rotations.SendMessage("CollisionDetection", msg);
        }
        else
        {
            leftBool = false;
            grabber.SendMessage("LeftBool",leftBool);
        }
    }
}
