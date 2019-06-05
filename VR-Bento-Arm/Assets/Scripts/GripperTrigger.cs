/* 
    BLINC LAB VIPER PROJECT 
    GripperTrigger.cs
    Created by: Cyrus Diego May 31, 2019 

    Attatched to the empty gameObject that has the sphere collider trigger
    that detects if an interactable is inside the end - effectors 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GripperTrigger : MonoBehaviour
{
    public GameObject interactable = null;
    private bool leftBool, rightBool;
    private List<Collider> colliderObjs = new List<Collider>();
    private int interactableObjs = 0, boxCollider = 0;
    public VRrotations rotations = null;

    void OnTriggerEnter(Collider other)
    {
        // De - activates the trigger when the arm is rotating the Elbow.
        if(rotations.GetComponent<VRrotations>().mode == "Elbow")
        {
            return;
        }

        if(!colliderObjs.Contains(other))
        {
            colliderObjs.Add(other);
        }

        if(other.gameObject.tag == "Interactable")
        {
            interactable = other.gameObject;
            interactableObjs++;
        }

        if(other.gameObject.tag == "boxColliderChild")
        {
            interactable = other.transform.parent.gameObject;
            boxCollider++;
        }

        // If the the interactable is comprised of composite box colliders,
        // finds the root gameObject and assigns that as the interactable.
        if(interactableObjs > 0 && boxCollider > 0)
        {
            foreach(Collider obj in colliderObjs)
            {
                if(obj.gameObject.tag == "boxColliderChild")
                {
                    interactable = other.transform.parent.gameObject;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        interactable = null;

        if(other.gameObject.tag == "Interactable")
        {
            interactableObjs--;
        }

        if(other.gameObject.tag == "boxColliderChild")
        {
            boxCollider--;
        }
    }

    void RightBool(bool msg)
    {
        rightBool = msg;
    }

    void LeftBool(bool msg)
    {
        leftBool = msg;
    }

    void Update()
    {
        if(leftBool && rightBool && interactable)
        {
            interactable.transform.parent = gameObject.transform;
            interactable.GetComponent<Rigidbody>().isKinematic = true;
        }
        else if(!leftBool || !rightBool)
        {
            if(interactable)
            {
                interactable.transform.parent = null;
                interactable.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

}
