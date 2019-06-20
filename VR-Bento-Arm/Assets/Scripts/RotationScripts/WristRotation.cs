/* 
    BLINC LAB VIPER Project 
    WristRotation.cs 
    Created by: Cyrus Diego May 14, 2019 

    Inherits from RotationBase class and controls the arm's wrist 
    rotation. Attatched to the wrist game object. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristRotation: RotationBase
{
    void Start()
    {
        // Wrist rotates about its local x axis
        setRotationAxis(1);

        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();
        go = gameObject;

        // Servo motor specs
        motorTorque = 611000f;
        maxSpeedLimit = 1.03f;

        cj.rotationDriveMode = RotationDriveMode.XYAndZ;

    }

    void FixedUpdate()
    {
        // -1 * to flip the rotation 
        // getAxis(-1*Input.GetAxis("TOUCHPAD_HORIZONTAL_LEFT"));
        getAxis(0,1);
    }
}