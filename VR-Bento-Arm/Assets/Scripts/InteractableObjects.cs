using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractableObjects : MonoBehaviour
{
    public GameObject trigger = null;
    private List<Collision> collisionObjs = new List<Collision>();
    private bool contact = false;
    private int n = 0;

    void Detatch(bool msg)
    {
        contact = msg;
    }

    void Attach(Tuple<GameObject,bool> msg)
    {
        trigger = msg.Item1;
        contact = msg.Item2;
    }
    
    void FixedUpdate()
    {
        int countRight = 0, countLeft = 0;
        foreach(Collision obj in collisionObjs)
        {
            if(obj.gameObject.tag == "GripperRight")
            {
                countRight++;
            }
            if(obj.gameObject.tag == "GripperLeft")
            {
                countLeft++;
            }
        }
        print("right : " + countRight);
        print("left : " + countLeft);
        if(countLeft == 0 || countRight == 0 || !contact)
        {
            gameObject.transform.parent = gameObject.transform;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

        if(contact && countLeft > 0 && countRight > 0)
        {
            gameObject.transform.parent = trigger.transform;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        bool add = true;
        foreach(Collision obj in collisionObjs)
        {
            if(other.gameObject.tag == obj.gameObject.tag)
            {
                add = false;
            }
        }
        if(add)
        {
            collisionObjs.Add(other);
        }
    }

    void OnCollisionExit(Collision other)
    {
        print("collision exit");
        collisionObjs.Remove(other);
    }
}
