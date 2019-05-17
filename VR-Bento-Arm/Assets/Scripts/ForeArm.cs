using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeArm : MonoBehaviour
{
    public Transform ForeArmShellTransform = null;
    public GameObject Rotations = null;
    private Tuple<string,bool> msg;

    // Update is called once per frame
    void FixedUpdate() {
        gameObject.transform.position = ForeArmShellTransform.position;
        gameObject.transform.eulerAngles = ForeArmShellTransform.eulerAngles;
    }

    void OnTriggerEnter(Collider other) {
        msg = new Tuple<string, bool>("Elbow", true);
        Rotations.SendMessage("collisionDetection",msg);
    }

    void OnTriggerExit(Collider other) {
        msg = new Tuple<string, bool>("Elbow", false);
        Rotations.SendMessage("collisionDetection", msg);
    }
}
