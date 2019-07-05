/* 
    BLINC LAB VIPER Project 
    ShoulderRotation.cs 
    Created by: Cyrus Diego May 14, 2019 

    Inherits from RotationBase class and controls the arm's shoulder 
    rotation. Attatched to the bicep game object. 
 */
using System;
using UnityEngine;

namespace WMR
{
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
            // getAxis(Input.GetAxis("THUMBSTICK_VERTICAL_RIGHT"));
            float direction = UDPConnection.udp.rotationArray[1].Item1;
            float velocity = UDPConnection.udp.rotationArray[1].Item2;
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
}
