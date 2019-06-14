﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderRotation : MonoBehaviour
{
    private ConfigurableJoint cj = null;
    private Rigidbody rb = null;
    private JointDrive motor;

    // Servo specs
    private float motorTorque = 1319000f;
    private float maxSpeedLimit = 0.61f;
    private Quaternion targetRotation;
    private bool target = true;

    void Start()
    {
        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();

        cj.angularXDrive = motor;
        cj.rotationDriveMode = RotationDriveMode.XYAndZ;
    }

    void FixedUpdate()
    {
        if(Input.GetAxis("THUMBSTICK_HORIZONTAL_LEFT") >= 0.5)
        {
            cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(maxSpeedLimit,0,0);

            motor.maximumForce = motorTorque;
            motor.positionSpring = 0;
            motor.positionDamper = motorTorque /(maxSpeedLimit - rb.angularVelocity.y);
            
            cj.angularXDrive = motor;
            target = true;
        }
        else if(Input.GetAxis("THUMBSTICK_HORIZONTAL_LEFT") <= -0.5)
        {
            cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(-maxSpeedLimit,0,0);

            motor.maximumForce = motorTorque;
            motor.positionSpring = 0;
            motor.positionDamper = motorTorque /(maxSpeedLimit - rb.angularVelocity.y);
            
            cj.angularXDrive = motor;
            target = true;

        }
        else
        {
            if(target)
            {
                setTargetRotation();
            }
            cj.targetAngularVelocity = Vector3.zero;
            cj.targetRotation = targetRotation;

            motor.maximumForce = motorTorque;
            motor.positionSpring = 1000000000;
            motor.positionDamper = 0;
            
            cj.angularXDrive = motor;
            target = false;

            cj.xMotion = ConfigurableJointMotion.Locked;
            cj.yMotion = ConfigurableJointMotion.Locked;
            cj.zMotion = ConfigurableJointMotion.Locked;
            cj.angularYMotion = ConfigurableJointMotion.Locked;
            cj.angularZMotion = ConfigurableJointMotion.Locked;
        }
    }
    private void setTargetRotation()
    {
        targetRotation = Quaternion.Euler(-gameObject.transform.localEulerAngles.y,0,0);
        target = false;
    }
}