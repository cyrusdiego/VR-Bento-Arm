﻿// /* 
//     BLINC LAB VIPER Project 
//     WristRotation.cs 
//     Created by: Cyrus Diego May 14, 2019 

//     Inherits from RotationBase class and controls the arm's wrist 
//     rotation. Attatched to the wrist game object. 
//  */
// using UnityEngine;

// public class WristRotation: RotationBase
// {
//     public BentoControl bentoControl;
//     void Start()
//     {
//         // Wrist rotates about its local x axis
//         setRotationAxis(1);

//         cj = gameObject.GetComponent<ConfigurableJoint>();
//         rb = gameObject.GetComponent<Rigidbody>();
//         go = gameObject;

//         // Servo motor specs
//         motorTorque = 611000f;
//         maxSpeedLimit = 1.03f;

//         cj.rotationDriveMode = RotationDriveMode.XYAndZ;
//     }

//     // void FixedUpdate()
//     // {
//     //     // -1 * to flip the rotation 
//     //     // getAxis(-1*Input.GetAxis("TOUCHPAD_HORIZONTAL_LEFT"));
//     //     getAxis(0,1);
//     // }

//     void FixedUpdate()
//     {
//         // getAxis(Input.GetAxis("THUMBSTICK_VERTICAL_RIGHT"));
//         float direction = bentoControl.rotationArray[3].Item1;
//         float velocity = bentoControl.rotationArray[3].Item2;
//         switch(direction)
//         {
//             case 0:
//                 getAxis(0,velocity);
//                 break;

//             case 1:
//                 getAxis(-1,velocity);
//                 break;

//             case 2:
//                 getAxis(1,velocity);
//                 break;
//         }
//     }
// }
