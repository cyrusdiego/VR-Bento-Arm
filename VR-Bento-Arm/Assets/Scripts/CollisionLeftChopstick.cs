/* 
    BLINC LAB VIPER PROJECT 
    CollisionLeftChopsticks.cs
    Created by: Cyrus Diego May 21, 2019 

    Sends message to controller script if it has collided with something
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionLeftChopstick : MonoBehaviour
{
    private Tuple<string,bool> msg;
    public GameObject Rotations = null;
    private List<Collision> collisionObjs = new List<Collision>();
    private List<Collider> colliderObjs = new List<Collider>();

    void OnTriggerEnter(Collider other)
    {
        if(colliderObjs.Contains(other))
        {
            return;
        }   
        else 
        {
            if(other.gameObject.tag != "test")
            {
                msg = new Tuple<string,bool>("Wrist Flexion", true);
                Rotations.SendMessage("CollisionDetection", msg);
            }
            colliderObjs.Add(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        colliderObjs.Remove(other);
        msg = new Tuple<string,bool>("Wrist Flexion", false);
        Rotations.SendMessage("CollisionDetection", msg);
    }

    void OnCollisionEnter(Collision other)
    {     

        if(collisionObjs.Contains(other))
        {
            return;
        }   
        else 
        {
            if(other.gameObject.tag != "test")
            {
                msg = new Tuple<string,bool>("Wrist Flexion", true);
                Rotations.SendMessage("CollisionDetection", msg);
            }
            collisionObjs.Add(other);
        }
    }

    void OnCollisionExit(Collision other)
    {
        collisionObjs.Remove(other);
        msg = new Tuple<string,bool>("Wrist Flexion", false);
        Rotations.SendMessage("CollisionDetection", msg);
    }
}
