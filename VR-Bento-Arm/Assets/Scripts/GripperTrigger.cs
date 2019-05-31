using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GripperTrigger : MonoBehaviour
{
    private Tuple<GameObject,bool> msg;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "test")
        {
            msg = new Tuple<GameObject, bool>(gameObject,true);
            other.gameObject.SendMessage("Attach",msg);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "test")
        {
            msg = new Tuple<GameObject, bool>(gameObject,false);
            other.gameObject.SendMessage("Attach",msg);
        }
    }
}
