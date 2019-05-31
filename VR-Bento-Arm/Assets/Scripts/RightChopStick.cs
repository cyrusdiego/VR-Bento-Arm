using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightChopStick : MonoBehaviour
{
    protected bool rightBool = false;
    public GameObject grabber = null;

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Interactable")
        {
            rightBool = true;
            grabber.SendMessage("RightBool",rightBool);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Interactable")
        {
            rightBool = false;
            grabber.SendMessage("RightBool",rightBool);
        }
    }
}
