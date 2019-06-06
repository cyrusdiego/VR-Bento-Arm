using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forearm : MonoBehaviour
{
    private ConfigurableJoint cj = null;
    private Rigidbody rb = null;
    private bool shoulder = false;
    private JointDrive motor;

    void Start()
    {
        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {      
        if(Input.GetAxis("THUMBSTICK_VERTICAL_RIGHT") >= 0.5)
        {
            cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(-0.537f,0,0);
        }
        else if(Input.GetAxis("THUMBSTICK_VERTICAL_RIGHT") <= -0.5)
        {
            cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(0.537f,0,0);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
            cj.targetAngularVelocity = Vector3.zero;

            // cj.xMotion = ConfigurableJointMotion.Locked;
            // cj.yMotion = ConfigurableJointMotion.Locked;
            // cj.zMotion = ConfigurableJointMotion.Locked;
            // cj.angularXMotion = ConfigurableJointMotion.Locked;
            // cj.angularYMotion = ConfigurableJointMotion.Locked;
            // cj.angularZMotion = ConfigurableJointMotion.Locked;
        }
        
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if(shoulder)
        {
            Vector3 angularVelocity = new Vector3(rb.angularVelocity.x,0,rb.angularVelocity.z);
            rb.angularVelocity = Vector3.zero;
        }
    }

    void HaltShoulderRotation(bool msg)
    {
        shoulder = msg;
    }
}
