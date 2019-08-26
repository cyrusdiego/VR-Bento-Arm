/* 
    BLINC LAB VIPER Project 
    VRController.cs
    Created by: Cyrus Diego July 12, 2019

    This class is used to control the bento arm using the VR Controllers
 */
using UnityEngine;
using Valve.VR;
using System;

public class VRController : MonoBehaviour
{
    public Global global = null;
    private SteamVR_Input_Sources Left;
    private SteamVR_Input_Sources Right;
    private SteamVR_Action joystick;
    private SteamVR_Action trigger;
    private SteamVR_Action push;
    private SteamVR_Action_Single single;
    private SteamVR_Action_Boolean boolean;
    private SteamVR_Action_Vector2 vector2;

    private Vector2 rightJoy;
    private Vector2 leftJoy;

    void Awake()
    {
        // Attatch the correct action to each variable to track values from VR controllers 
        joystick = SteamVR_Action.FindExistingActionForPartialPath("/actions/default/in/Trackpad");
        trigger = SteamVR_Action.FindExistingActionForPartialPath("/actions/default/in/Squeeze");
        push = SteamVR_Action.FindExistingActionForPartialPath("/actions/default/in/Button");

        // Attatches variables to Left / Right controllers
        Left = SteamVR_Input_Sources.LeftHand;
        Right = SteamVR_Input_Sources.RightHand;

        // Clears the VR control array 
        for(int i = 0; i < global.SteamVRControl.Length; i++)
        {
            global.SteamVRControl[i] = 0;
        }
    }

    void FixedUpdate()
    {
        if(global.startup)
        {
            return;
        }
        // Will only use VR Controller input if the control toggle from brachIOplexus is disabled
        if(!global.controlToggle)
        {
            // clears buffer
            clearRotationArray();

            // Gets current values from the actions 
            single = (SteamVR_Action_Single)trigger;  // Gets a float 0 to 1
            boolean = (SteamVR_Action_Boolean)push;  // Gets a bool (if button is pressed or not)
            vector2 = (SteamVR_Action_Vector2)joystick;  // Get a 2-D vector (0 to 1)
            // Determines if the end effectors should open or not 
            // Will need to be expanded for future end effectors (only created for chopsticks)

            // if both triggers are pressed OR if neither triggers are pressed, don't move
            if(single.GetAxis(Left) != 0 && single.GetAxis(Right) != 0 || (single.GetAxis(Left) == 0 && single.GetAxis(Right) == 0))
            {
                global.SteamVRControl[4] = 0;
            }
            else if(single.GetAxis(Left) != 0 && single.GetAxis(Right) == 0)
            {   
                global.SteamVRControl[4] = single.GetAxis(Left);
            }
            else if(single.GetAxis(Left) == 0 && single.GetAxis(Right) != 0)
            {
                global.SteamVRControl[4] = -1 * single.GetAxis(Right);
            }

            
            // Checks if the controllers are from the VIVE
            if(SteamVR.instance.hmd_ModelNumber == "VIVE_Pro MV")
            {
                // Checks first if the trackpad is pressed down
                if(boolean.GetState(Left))
                {
                    // Gets values from the Left trackpad
                    leftJoy = vector2.GetAxis(Left);
                    // Only fills array if the value is above the threshold 
                    global.SteamVRControl[0] = Math.Abs(leftJoy.x) >= 0.15 ? -1 * leftJoy.x : 0;
                    global.SteamVRControl[1] =  Math.Abs(leftJoy.y) >= 0.15 ? -1 * leftJoy.y : 0;
                }
                // Checks first if the trackpad is pressed down
                if(boolean.GetState(Right))
                {
                    // Gets value from Right trackpad
                    rightJoy = vector2.GetAxis(Right);
                    // Only fills array if tyhe value is above the threshold
                    global.SteamVRControl[2] = Math.Abs(rightJoy.x) >= 0.15 ? -1 * rightJoy.x : 0;
                    global.SteamVRControl[3] = Math.Abs(rightJoy.y) >= 0.15 ? -1 * rightJoy.y : 0;
                }

            }
            else
            {
                // Gets values from the Left trackpad
                leftJoy = vector2.GetAxis(Left);
                // Only fills array if the value is above the threshold 
                global.SteamVRControl[0] = Math.Abs(leftJoy.x) >= 0.15 ? -1 * leftJoy.x : 0;
                global.SteamVRControl[1] = Math.Abs(leftJoy.y) >= 0.15 ? -1 * leftJoy.y : 0;

                // Gets value from Right trackpad
                rightJoy = vector2.GetAxis(Right);
                // Only fills array if tyhe value is above the threshold
                global.SteamVRControl[2] = Math.Abs(rightJoy.x) >= 0.15 ? -1 * rightJoy.x : 0;
                global.SteamVRControl[3] = Math.Abs(rightJoy.y) >= 0.15 ? -1 * rightJoy.y : 0;
            
            }
        }
    }

    void clearRotationArray()
    {
        for(int i = 0; i < global.SteamVRControl.Length; i++)
        {
            global.SteamVRControl[i] = 0;
        }
    }

}

