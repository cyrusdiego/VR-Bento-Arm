﻿/* 
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

    void OnTriggerEnter(Collider other)
    {   
        print("left chopstick triggered");
        msg = new Tuple<string,bool>("Wrist Flexion", true);
        Rotations.SendMessage("CollisionDetection", msg);
    }

    void OnTriggerExit(Collider other)
    {
        msg = new Tuple<string,bool>("Wrist Flexion", false);
        Rotations.SendMessage("CollisionDetection", msg);
    }


    void OnCollisionEnter(Collision other)
    {
        print("leftchopstick collided  ");
        msg = new Tuple<string,bool>("Wrist Flexion", true);
        Rotations.SendMessage("CollisionDetection", msg);
    }

    void OnCollisionExit(Collision other)
    {
        msg = new Tuple<string,bool>("Wrist Flexion", false);
        Rotations.SendMessage("CollisionDetection", msg);
    }
}
