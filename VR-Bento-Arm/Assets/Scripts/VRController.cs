/* 
    BLINC LAB VIPER Project 

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

    void Start()
    {
        joystick = SteamVR_Action.FindExistingActionForPartialPath("/actions/default/in/Trackpad");
        trigger = SteamVR_Action.FindExistingActionForPartialPath("/actions/default/in/Squeeze");
        push = SteamVR_Action.FindExistingActionForPartialPath("/actions/default/in/Button");

        
        Left = SteamVR_Input_Sources.LeftHand;
        Right = SteamVR_Input_Sources.RightHand;

        for(int i = 0; i < 6; i++)
        {
            global.SteamVRControl[i] = 0;
        }
    }

    void Update()
    {
        if(!global.controlToggle)
        {
            clearRotationArray();
            single = (SteamVR_Action_Single)trigger;
            boolean = (SteamVR_Action_Boolean)push;
            vector2 = (SteamVR_Action_Vector2)joystick;

            if(single.GetAxis(Left) != 0 && single.GetAxis(Right) != 0 || (single.GetAxis(Left) == 0 && single.GetAxis(Right) == 0))
            {
                global.SteamVRControl[5] = 0;
            }
            else if(single.GetAxis(Left) != 0 && single.GetAxis(Right) == 0)
            {   
                global.SteamVRControl[5] = single.GetAxis(Left);
            }
            else if(single.GetAxis(Left) == 0 && single.GetAxis(Right) != 0)
            {
                global.SteamVRControl[5] = -1 * single.GetAxis(Right);
            }

            
            if(SteamVR.instance.hmd_ModelNumber == "VIVE_Pro MV")
            {
                if(boolean.GetState(Left))
                {
                    leftJoy = vector2.GetAxis(Left);
                    global.SteamVRControl[1] = Math.Abs(leftJoy.x) >= 0.15 ? -1 * leftJoy.x : 0;
                    global.SteamVRControl[2] =  Math.Abs(leftJoy.y) >= 0.15 ? -1 * leftJoy.y : 0;
                }
                if(boolean.GetState(Right))
                {
                    rightJoy = vector2.GetAxis(Right);
                    global.SteamVRControl[3] = Math.Abs(rightJoy.x) >= 0.15 ? -1 * rightJoy.x : 0;
                    global.SteamVRControl[4] = Math.Abs(rightJoy.y) >= 0.15 ? -1 * rightJoy.y : 0;
                }

            }
            else
            {
                leftJoy = vector2.GetAxis(Left);
                global.SteamVRControl[1] = Math.Abs(leftJoy.x) >= 0.15 ? -1 * leftJoy.x : 0;
                global.SteamVRControl[2] = Math.Abs(leftJoy.y) >= 0.15 ? -1 * leftJoy.y : 0;

                rightJoy = vector2.GetAxis(Right);
                global.SteamVRControl[3] = Math.Abs(rightJoy.x) >= 0.15 ? -1 * rightJoy.x : 0;
                global.SteamVRControl[4] = Math.Abs(rightJoy.y) >= 0.15 ? -1 * rightJoy.y : 0;
            
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

