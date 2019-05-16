using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrist: MonoBehaviour
{
    public Transform WristArmShellTransform = null;
    public GameObject Rotations = null;

    // Update is called once per frame
    void FixedUpdate() {
        gameObject.transform.position = WristArmShellTransform.position;
        gameObject.transform.eulerAngles = WristArmShellTransform.eulerAngles;
    }
    
    void OnTriggerEnter(Collider other) {
        Rotations.SendMessage("collisionDetection",true);
    }

    void OnTriggerExit(Collider other) {
        Rotations.SendMessage("collisionDetection", false);
    }
}
