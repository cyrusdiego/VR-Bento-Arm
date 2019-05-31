using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chopsticks : MonoBehaviour
{
    public GameObject interactable = null;
    private Transform interactableOriginal = null;
    private bool leftBool, rightBool;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Interactable")
        {
            interactable = other.gameObject;
            interactableOriginal = interactable.transform;
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
                interactable.transform.parent = interactableOriginal;
                interactable.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

}
