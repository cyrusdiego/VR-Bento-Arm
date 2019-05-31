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
    void Attach(Tuple<GameObject,bool> msg)
    {
        trigger = msg.Item1;
        contact = msg.Item2;
    }
    
    void FixedUpdate()
    {
        if(contact)
        {
            print("contact true");
        }
        if(contact && n >= 2)
        {
            gameObject.transform.parent = trigger.transform;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(!collisionObjs.Contains(other))
        {
            collisionObjs.Add(other);
        }
        print(other.gameObject.ToString());
        if(other.gameObject.tag == "Gripper")
        {
            n++;
        }

    }

    void OnCollisionExit(Collision other)
    {
        collisionObjs.Remove(other);
 
    }
}
