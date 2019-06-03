using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GripperTrigger : MonoBehaviour
{
    public GameObject interactable = null;
    private Transform interactableOriginal = null;
    private bool leftBool, rightBool;
    private List<Collider> colliderObjs = new List<Collider>();
    private int n = 0;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Interactable")
        {
            interactable = other.gameObject;
        }
        if(other.gameObject.tag == "boxColliderChild")
        {
            print("thing had right tag");
            interactable = other.transform.parent.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        interactable = null;
    }

    void RightBool(bool msg)
    {
        rightBool = msg;
    }

    void LeftBool(bool msg)
    {
        leftBool = msg;
    }

    void FixedUpdate()
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
