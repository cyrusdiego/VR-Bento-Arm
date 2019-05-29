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

public class CollisionRightChopstick : MonoBehaviour
{
    public GameObject Rotations = null;
    private Tuple<string,bool> msg;

    void OnTriggerEnter(Collider other)
    {
        print("Hello");
        msg = new Tuple<string, bool>("Open Hand", true);
        Rotations.SendMessage("CollisionDetection",msg);
    }

    void OnTriggerExit(Collider other)
    {
        
        msg = new Tuple<string, bool>("Open Hand", false);
        Rotations.SendMessage("CollisionDetection", msg);
    }

    void OnCollisionEnter(Collision other)
    {
        print("right chopstick collided with" + other);
    }
}
