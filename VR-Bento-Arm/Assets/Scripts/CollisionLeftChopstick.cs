using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionLeftChopstick : MonoBehaviour
{
    private Tuple<string,bool> msg;
    public GameObject Rotations = null;

    void OnCollisionEnter(Collision collision) 
    {
        msg = new Tuple<string,bool>("Wrist Flexion", true);
        Rotations.SendMessage("CollisionDetection", msg);
    }

    void OnCollisionExit(Collision collision) 
    {
        msg = new Tuple<string,bool>("Wrist Flexion", false);
        Rotations.SendMessage("CollisionDetection", msg);
    }
}
