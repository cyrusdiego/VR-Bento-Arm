﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrist: MonoBehaviour
{
    public Transform WristArmShellTransform = null;
    public GameObject Rotations = null;
    private Tuple<string,bool> msg;

    // Update is called once per frame
    void FixedUpdate() {
        gameObject.transform.position = WristArmShellTransform.position;
        gameObject.transform.eulerAngles = WristArmShellTransform.eulerAngles;
    }
    
    void OnTriggerEnter(Collider other) {
        msg = new Tuple<string,bool>("Forearm Rotation", true);
        Rotations.SendMessage("collisionDetection", msg);
    }

    void OnTriggerExit(Collider other) {
        msg = new Tuple<string,bool>("Forearm Rotation", false);
        Rotations.SendMessage("collisionDetection", msg);
    }
}
