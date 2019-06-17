/* 
    BLINC LAB VIPER Project 
    HandRotation.cs 
    Created by: Cyrus Diego May 14, 2019 

    Inherits from RotationBase class and controls the arm's chopstick 
    rotation. Attatched to the chopstick game object. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRotation : RotationBase
{
    private float axisValue = 0;

    void Start()
    {
        // Chopsticks rotate about its local y axis
        setRotationAxis(2);

        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();
        go = gameObject;

        cj.rotationDriveMode = RotationDriveMode.XYAndZ;

        // Servo motor specs
        motorTorque = 978000;
        maxSpeedLimit = 1.03f;

        // Prevents the right end effector from being disassembled
        cj.projectionMode = JointProjectionMode.PositionAndRotation;
        cj.projectionAngle = 0.1f;
    }

    void FixedUpdate()
    {
        if(Input.GetAxis("SELECT_TRIGGER_SQUEEZE_LEFT") >= 0.5)
        {
            axisValue = Input.GetAxis("SELECT_TRIGGER_SQUEEZE_LEFT");
            getAxis(axisValue);
            return;
        }
        if(Input.GetAxis("SELECT_TRIGGER_SQUEEZE_RIGHT") >= 0.5)
        {
            axisValue = -1 * Input.GetAxis("SELECT_TRIGGER_SQUEEZE_RIGHT");
            getAxis(axisValue);
            return;
        }

        getAxis(0);
    }
}