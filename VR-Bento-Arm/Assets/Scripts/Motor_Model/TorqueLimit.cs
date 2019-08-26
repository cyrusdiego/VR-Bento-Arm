using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorqueLimit : MonoBehaviour
{
    public Global global = null;
    private float maximum = 10e3f;

    void OnCollisionEnter(Collision col)
    {
        string name; 
        name = col.gameObject.name;
        if(name == "LeftChopstick" || name == "RightChopstick")
        {
            Vector3 impulse; 
            impulse = col.impulse;

            Vector3 force;
            force = impulse / Time.fixedDeltaTime;
            if(force.y >= maximum)
            {
                global.maxTorque = true;
            }
        }       
    }
    
    void OnCollisionExit(Collision col)
    {
        string name; 
        name = col.gameObject.name;
        if(name == "LeftChopstick" || name == "RightChopstick")
        {
            global.maxTorque = false;
        }
    }
}
