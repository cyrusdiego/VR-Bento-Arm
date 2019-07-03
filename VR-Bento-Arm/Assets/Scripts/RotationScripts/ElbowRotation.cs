/* 
    BLINC LAB VIPER Project 
    ElbowRotation.cs 
    Created by: Cyrus Diego May 14, 2019 

    Inherits from RotationBase class and controls the arm's elbow 
    rotation. Attatched to the forearm game object. 
 */
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
        // getAxis(Input.GetAxis("THUMBSTICK_VERTICAL_RIGHT"));

        float direction = UDPConnection.udp.rotationArray[2].Item1;
        float velocity = UDPConnection.udp.rotationArray[2].Item2;
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