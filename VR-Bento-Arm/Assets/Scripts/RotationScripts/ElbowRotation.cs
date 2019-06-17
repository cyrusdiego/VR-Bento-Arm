/* 
    BLINC LAB VIPER Project 
    ElbowRotation.cs 
    Created by: Cyrus Diego May 14, 2019 

    Inherits from RotationBase class and controls the arm's elbow 
    rotation. Attatched to the forearm game object. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElbowRotation : RotationBase
{
    void Start()
    {
        // Elbow rotates about its local x axis
        setRotationAxis(1);

        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();
        go = gameObject;

        // Servo motor specs
        motorTorque = 2463000f;
        maxSpeedLimit = 0.537f;

        cj.rotationDriveMode = RotationDriveMode.XYAndZ;

    }

    void FixedUpdate()
    {
        getAxis(Input.GetAxis("THUMBSTICK_VERTICAL_RIGHT"));
    }
}