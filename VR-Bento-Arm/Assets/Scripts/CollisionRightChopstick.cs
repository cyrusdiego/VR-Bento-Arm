using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRightChopstick : MonoBehaviour
{
    public GameObject Rotations = null;
    private Tuple<string,bool> msg;

    void OnTriggerEnter(Collider other) {
        msg = new Tuple<string, bool>("Right Hand", true);
        Rotations.SendMessage("collisionDetection",msg);
    }

    void OnTriggerExit(Collider other) {
        msg = new Tuple<string, bool>("Right Hand", false);
        Rotations.SendMessage("collisionDetection", msg);
    }
}
