/* 
    BLINC LAB VIPER Project 
    WristFlexion.cs 
    Created by: Cyrus Diego May 14, 2019 

    Inherits from RotationBase class and controls the arm's wrist flexion.
    Attatched to the hand game object. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristFlexion : RotationBase
{
    void Start()
    {
        // Wrist flexes about its local x axis
        setRotationAxis(1);

        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();
        go = gameObject;

        // Servo motor specs
        motorTorque = 733000f;
        maxSpeedLimit = 0.771f;

        cj.rotationDriveMode = RotationDriveMode.XYAndZ;
        cj.projectionMode = JointProjectionMode.PositionAndRotation;
        cj.projectionAngle = 0.1f;

    }

    void FixedUpdate()
    {
        getAxis(Input.GetAxis("TOUCHPAD_VERTICAL_RIGHT"));
    }
}