using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeArmShell : MonoBehaviour
{
    public Transform ForeArmShellTransform = null;
    public GameObject Rotations = null;
    private Collider thing = null;
    private Vector3 point;

    void Start() {
        thing = gameObject.GetComponent<Collider>();
    }
    // Update is called once per frame
    void FixedUpdate() {
        gameObject.transform.position = ForeArmShellTransform.position;
        gameObject.transform.eulerAngles = ForeArmShellTransform.eulerAngles;
        
    }

    void OnTriggerEnter(Collider other) {
        Rotations.SendMessage("collisionDetection",true);
    }

    void OnTriggerExit(Collider other) {
        Rotations.SendMessage("collisionDetection", false);
    }
}
