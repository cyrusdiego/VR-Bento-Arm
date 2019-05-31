using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chopsticks : MonoBehaviour
{
    public GameObject interactable = null;
    private bool leftBool, rightBool;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Interactable")
        {
            interactable = other.gameObject;
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
            print("attatching");
            interactable.transform.parent = gameObject.transform;
            interactable.GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            //print("left bool : " + leftBool + " right bool : " + rightBool);
            // if(interactable)
            // {
            //     interactable.transform.parent = interactable.transform;
            //     interactable.GetComponent<Rigidbody>().isKinematic = false;
            // }
        }
    }

}
