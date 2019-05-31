using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightChopStick : MonoBehaviour
{
    protected bool rightBool = false;
    public GameObject grabber = null;
    public GameObject rotations = null;
    public GameObject RightChopStickParent = null;
    private Vector3 currentAngle;
    private bool objectDetected = false;

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Interactable")
        {
            rightBool = true;
            currentAngle = RightChopStickParent.transform.localEulerAngles;
            grabber.SendMessage("RightBool",rightBool);
            rotations.SendMessage("GrabbedObject",true);
            objectDetected = true;
        }
    }

    void FixedUpdate()
    {
        if(RightChopStickParent.transform.localEulerAngles.y != currentAngle.y && objectDetected)
        {
            rightBool = false;
            grabber.SendMessage("RightBool",rightBool);
            rotations.SendMessage("GrabbedObject", false);
        }
    }
}
