/* 
    BLINC LAB VIPER Project 
    ShoulderRotation.cs 
    Created by: Cyrus Diego May 14, 2019 

    Inherits from RotationBase class and controls the arm's shoulder 
    rotation. Attatched to the bicep game object. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderRotation : RotationBase
{
    void Start()
    {
        // Shoulder rotates about local y axis
        setRotationAxis(2);

        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();
        go = gameObject;

        // Servo motor specs
        motorTorque = 1319000f;
        maxSpeedLimit = 0.61f;

        cj.rotationDriveMode = RotationDriveMode.XYAndZ;

    }

    void FixedUpdate()
    {
        // -1 to flip the direction 
        getAxis(-1*Input.GetAxis("THUMBSTICK_HORIZONTAL_LEFT"));
    }
    public void recieveInput(int i)
    {
        getAxis((float)i);
    }
}