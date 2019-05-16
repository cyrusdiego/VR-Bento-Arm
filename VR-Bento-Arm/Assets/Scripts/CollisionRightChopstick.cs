using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRightChopstick : MonoBehaviour
{
    public GameObject Rotations = null;

    void OnTriggerEnter(Collider other) {
        Rotations.SendMessage("collisionDetection",true);
    }

    void OnTriggerExit(Collider other) {
        Rotations.SendMessage("collisionDetection", false);
    }
}
