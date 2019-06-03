using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    protected bool rightBool = false;
    public GripperTrigger grabber = null;
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
            grabber.gameObject.SendMessage("RightBool",rightBool);
            if(grabber.GetComponent<GripperTrigger>().interactable)
            {
                rotations.SendMessage("ObjectAttatched",true);
            }
            objectDetected = true;
        }
    }

    void FixedUpdate()
    {
        if(RightChopStickParent.transform.localEulerAngles.y != currentAngle.y && objectDetected)
        {
            print("detatching");
            rightBool = false;
            grabber.gameObject.SendMessage("RightBool",rightBool);
            rotations.SendMessage("ObjectDetatched",false);
        }
    }
}
