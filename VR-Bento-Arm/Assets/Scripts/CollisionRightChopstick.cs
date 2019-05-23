﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRightChopstick : MonoBehaviour
{
    public GameObject Rotations = null;
    private Tuple<string,bool> msg;

    void OnCollisionEnter(Collision collision) 
    {
        Debug.Log("chopstick collied");
        msg = new Tuple<string, bool>("Open Hand", true);
        Rotations.SendMessage("CollisionDetection",msg);
    }

    void OnCollisionExit(Collision collision) 
    {
        msg = new Tuple<string, bool>("Open Hand", false);
        Rotations.SendMessage("CollisionDetection", msg);
    }
}
