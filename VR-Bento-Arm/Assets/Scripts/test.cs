using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class test : MonoBehaviour
{
    public Transform cameraObject = null;
    private SteamVR_Input_Sources Left;
    private SteamVR_Input_Sources Right;
    private SteamVR_Action action;
    private SteamVR_Action_Vector2 trackpad = null;
    private int camZMovement = 0, camXMovement = 0, camYMovement = 0;
    
    void Start()
    {
        if(Initialization.Instance.platformSelection == Initialization.PlatformSelection.VIVE)
        {
            Left = SteamVR_Input_Sources.LeftHand;
            Right = SteamVR_Input_Sources.RightHand;
            action = SteamVR_Action.FindExistingActionForPartialPath("\\action\\default\\in\\Trackpad");
            trackpad = (SteamVR_Action_Vector2)action;
                        print(action);

        }

    }


}
