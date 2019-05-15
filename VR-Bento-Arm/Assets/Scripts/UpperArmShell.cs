using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperArmShell : MonoBehaviour
{
    public Transform UpperArmShellTransform = null;
    public GameObject Rotations = null;

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.position = UpperArmShellTransform.position;
        gameObject.transform.eulerAngles = UpperArmShellTransform.eulerAngles;
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
