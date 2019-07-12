/* 
    BLINC LAB VIPER Project 

 */
using UnityEngine;
using Valve.VR;
using System;

public class VRController : MonoBehaviour
{
    public BentoControl bentoControl = null;
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
            bentoControl.SteamVRControl[i] = 0;
        }
    }

    void Update()
    {
        if(!bentoControl.controlToggle)
        {
            clearRotationArray();
            single = (SteamVR_Action_Single)trigger;
            boolean = (SteamVR_Action_Boolean)push;
            vector2 = (SteamVR_Action_Vector2)joystick;

            if(single.GetAxis(Left) != 0 && single.GetAxis(Right) != 0 || (single.GetAxis(Left) == 0 && single.GetAxis(Right) == 0))
            {
                bentoControl.SteamVRControl[5] = 0;
            }
            else if(single.GetAxis(Left) != 0 && single.GetAxis(Right) == 0)
            {   
                bentoControl.SteamVRControl[5] = single.GetAxis(Left);
            }
            else if(single.GetAxis(Left) == 0 && single.GetAxis(Right) != 0)
            {
                bentoControl.SteamVRControl[5] = -1 * single.GetAxis(Right);
            }

            
            if(SteamVR.instance.hmd_ModelNumber == "VIVE_Pro MV")
            {
                if(boolean.GetState(Left))
                {
                    leftJoy = vector2.GetAxis(Left);
                    bentoControl.SteamVRControl[1] = -1 * leftJoy.x;
                    bentoControl.SteamVRControl[2] = -1 * leftJoy.y;
                }
                if(boolean.GetState(Right))
                {
                    rightJoy = vector2.GetAxis(Right);
                    bentoControl.SteamVRControl[3] = -1 * rightJoy.x;
                    bentoControl.SteamVRControl[4] = -1 * rightJoy.y;
                }

            }
            else
            {
                leftJoy = vector2.GetAxis(Left);
                bentoControl.SteamVRControl[1] = leftJoy.x;
                bentoControl.SteamVRControl[2] = leftJoy.y;

                rightJoy = vector2.GetAxis(Right);
                bentoControl.SteamVRControl[3] = rightJoy.x;
                bentoControl.SteamVRControl[4] = rightJoy.y;
            
            }
        }
    }

    void clearRotationArray()
    {
        for(int i = 0; i < bentoControl.SteamVRControl.Length; i++)
        {
            bentoControl.SteamVRControl[i] = 0;
        }
    }

}

