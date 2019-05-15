using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristArmShell : MonoBehaviour
{
    public Transform WristArmShellTransform = null;
    public GameObject Rotations = null;

    // Update is called once per frame
    void FixedUpdate() {
        gameObject.transform.position = WristArmShellTransform.position;
        gameObject.transform.eulerAngles = WristArmShellTransform.eulerAngles;
    }
    
    void OnTriggerEnter(Collider other)
    {
        Rotations.SendMessage("Stop",true);
                Debug.Log(other.gameObject);

    }

    void OnTriggerExit(Collider other)
    {
        Rotations.SendMessage("Stop", false);
    }
}
