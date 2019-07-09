// /* 
//     BLINC LAB VIPER Project 
//     HandRotation.cs 
//     Created by: Cyrus Diego May 14, 2019 

//     Inherits from RotationBase class and controls the arm's chopstick 
//     rotation. Attatched to the chopstick game object. 
//  */
// using UnityEngine;

// public class HandRotation : RotationBase
// {
//     public BentoControl bentoControl;

//     void Start()
//     {
//         // Chopsticks rotate about its local y axis
//         setRotationAxis(2);

//         cj = gameObject.GetComponent<ConfigurableJoint>();
//         rb = gameObject.GetComponent<Rigidbody>();
//         go = gameObject;

//         cj.rotationDriveMode = RotationDriveMode.XYAndZ;

//         // Servo motor specs
//         motorTorque = 978000;
//         maxSpeedLimit = 1.03f;

//         // Prevents the right end effector from being disassembled
//         cj.projectionMode = JointProjectionMode.PositionAndRotation;
//         cj.projectionAngle = 0.1f;
//     }

//     // void FixedUpdate()
//     // {
//     //     // if(Input.GetAxis("SELECT_TRIGGER_SQUEEZE_LEFT") >= 0.5)
//     //     // {
//     //     //     axisValue = Input.GetAxis("SELECT_TRIGGER_SQUEEZE_LEFT");
//     //     //     getAxis(axisValue);
//     //     //     return;
//     //     // }
//     //     // if(Input.GetAxis("SELECT_TRIGGER_SQUEEZE_RIGHT") >= 0.5)
//     //     // {
//     //     //     axisValue = -1 * Input.GetAxis("SELECT_TRIGGER_SQUEEZE_RIGHT");
//     //     //     getAxis(axisValue);
//     //     //     return;
//     //     // }

//     //     getAxis(0,1);
//     // }

//     void FixedUpdate()
//     {
//         // getAxis(Input.GetAxis("THUMBSTICK_VERTICAL_RIGHT"));
//         float direction = bentoControl.rotationArray[5].Item1;
//         float velocity = bentoControl.rotationArray[5].Item2;
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

