﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionLeftChopstick : MonoBehaviour
{
    private Tuple<string,bool> msg;
    public GameObject Rotations = null;

    void OnTriggerEnter(Collider other) {
        msg = new Tuple<string,bool>("Left Hand", true);
        Rotations.SendMessage("collisionDetection", msg);
    }

    void OnTriggerExit(Collider other) {
        msg = new Tuple<string,bool>("Left Hand", false);
        Rotations.SendMessage("collisionDetection", msg);
    }
}
