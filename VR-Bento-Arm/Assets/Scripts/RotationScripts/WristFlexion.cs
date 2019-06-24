/* 
    BLINC LAB VIPER Project 
    WristFlexion.cs 
    Created by: Cyrus Diego May 14, 2019 

    Inherits from RotationBase class and controls the arm's wrist flexion.
    Attatched to the hand game object. 
 */
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

    // void FixedUpdate()
    // {
    //     // getAxis(Input.GetAxis("TOUCHPAD_VERTICAL_RIGHT"));
    //     getAxis(0,1);
    // }

    void FixedUpdate()
    {
        // getAxis(Input.GetAxis("THUMBSTICK_VERTICAL_RIGHT"));
        float direction = UDPConnection.udp.rotationArray[4].Item1;
        float velocity = UDPConnection.udp.rotationArray[4].Item2;
        switch(direction)
        {
            case 0:
                getAxis(0,velocity);
                break;

            case 1:
                getAxis(-1,velocity);
                break;

            case 2:
                getAxis(1,velocity);
                break;
        }
    }
}