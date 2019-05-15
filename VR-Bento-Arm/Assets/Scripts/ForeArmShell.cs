using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeArmShell : MonoBehaviour
{
    public Transform ForeArmShellTransform = null;
    public GameObject Rotations = null;

    // Update is called once per frame
    void FixedUpdate() {
        gameObject.transform.position = ForeArmShellTransform.position;
        gameObject.transform.eulerAngles = ForeArmShellTransform.eulerAngles;
    }
    
    void OnTriggerEnter(Collider other)
    {
        Rotations.SendMessage("Stop",true);
    }

    void OnTriggerExit(Collider other)
    {
        Rotations.SendMessage("Stop", false);
    }
}
