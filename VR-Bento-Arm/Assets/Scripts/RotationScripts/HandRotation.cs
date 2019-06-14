﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRotation : MonoBehaviour
{
    private ConfigurableJoint cj = null;
    private Rigidbody rb = null;
    private JointDrive motor;

    // Servo specs
    private float motorTorque = 978000;
    private float maxSpeedLimit = 1.03f;
    private bool target = true;
    private Quaternion targetRotation;

    void Start()
    {
        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();

        cj.angularXDrive = motor;
        cj.rotationDriveMode = RotationDriveMode.XYAndZ;

        // Prevents the right end effector from being disassembled
        cj.projectionMode = JointProjectionMode.PositionAndRotation;
        cj.projectionAngle = 0.1f;
    }

    void FixedUpdate()
    {
        if(Input.GetButton("SELECT_TRIGGER_PRESS_LEFT"))
        {
            cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(-maxSpeedLimit,0,0);

            motor.maximumForce = motorTorque;
            motor.positionSpring = 0;
            motor.positionDamper = motorTorque / (maxSpeedLimit - rb.angularVelocity.y);

            cj.angularXDrive = motor;
            target = true;
            return;
        }
        if(Input.GetButton("SELECT_TRIGGER_PRESS_RIGHT"))
        {
            cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(maxSpeedLimit,0,0);
            
            motor.maximumForce = motorTorque;
            motor.positionSpring = 0;
            motor.positionDamper = motorTorque / (maxSpeedLimit - rb.angularVelocity.y);

            cj.angularXDrive = motor;
            target = true;
            return;
        }
            
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

        // Ensures it doesn't move in other axes
        cj.xMotion = ConfigurableJointMotion.Locked;
        cj.yMotion = ConfigurableJointMotion.Locked;
        cj.zMotion = ConfigurableJointMotion.Locked;
        cj.angularYMotion = ConfigurableJointMotion.Locked;
        cj.angularZMotion = ConfigurableJointMotion.Locked;
    }
    
    private void setTargetRotation()
    {
        targetRotation = Quaternion.Euler(-gameObject.transform.localEulerAngles.y,0,0);
        target = false;
    }
}