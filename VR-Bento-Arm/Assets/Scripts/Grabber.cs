using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Grabber : MonoBehaviour
{
    protected bool rightBool = false;
    public GripperTrigger grabber = null;
    public GameObject rotations = null;
    public GameObject RightChopStickParent = null;
    private Vector3 currentAngle;
    private bool objectDetected = false;

    private Tuple<VRrotations.modes,bool> msg;
    private List<Collision> collisionRightObjs = new List<Collision>();
    private List<Collider> colliderObjs = new List<Collider>();

    void OnCollisionEnter(Collision other)
    {
        print("collidef with something");
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
        else 
        {
            if(collisionRightObjs.Contains(other))
            {
                return;
            }   
            print("sending msg to arm (right hand)");
            msg = new Tuple<VRrotations.modes, bool>(VRrotations.modes.Hand, true);
            rotations.SendMessage("CollisionDetection",msg);
            collisionRightObjs.Add(other);
        }
    }
    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Interactable")
        {
            return;
        }
        collisionRightObjs.Remove(other);
        if(other.gameObject.tag != "Interactable")
        {
            msg = new Tuple<VRrotations.modes, bool>(VRrotations.modes.Hand, false);
            rotations.SendMessage("CollisionDetection", msg);
        }
    }
    void FixedUpdate()
    {
        if(RightChopStickParent.transform.localEulerAngles.y != currentAngle.y && objectDetected)
        {
            rightBool = false;
            grabber.gameObject.SendMessage("RightBool",rightBool);
            rotations.SendMessage("ObjectDetatched",false);
            objectDetected = false;
        }
    }
}
