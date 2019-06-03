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
    private int interactableObjs = 0, boxCollider = 0;

    void OnTriggerEnter(Collider other)
    {
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
