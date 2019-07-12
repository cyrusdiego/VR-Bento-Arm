using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CameraMovement : MonoBehaviour
{            
    public Transform cameraObject = null;
    private SteamVR_Input_Sources Left;
    private SteamVR_Input_Sources Right;
    private SteamVR_Action action;
    private SteamVR_Action_Vector2 trackpad = null;
    private SteamVR_Action_Boolean boolean;
    private SteamVR_Action push;
    private int camZMovement = 0, camXMovement = 0, camYMovement = 0;
    public BentoControl bentoControl = null;
    
    void Start()
    {
        Left = SteamVR_Input_Sources.LeftHand;
        Right = SteamVR_Input_Sources.RightHand;
        
        action = SteamVR_Action.FindExistingActionForPartialPath("/actions/default/in/Trackpad");
        push = SteamVR_Action.FindExistingActionForPartialPath("/actions/default/in/Button");

    }

    // Update is called once per frame
    void Update()
    {
        trackpad = (SteamVR_Action_Vector2)action;
        boolean = (SteamVR_Action_Boolean)push;

        if(bentoControl.controlToggle)
        {
            print("in here bitch");
            checkJoystick();
        }
        else
        {
            checkKeyboard();
        }
        updateX();
        updateY();
        updateZ();
    }

    private int checkHold(float val)
    {
       if(val > 0.8)
       {
            return 1;
        } 
        else if(val < -0.8)
        {
            return -1;
        }
        else 
        {
            return 0;
        }
    }

    private int checkHold(KeyCode left, KeyCode right)
    {
        if((Input.GetKey(left) && Input.GetKey(right)) || (!Input.GetKey(left) && !Input.GetKey(right)))
        {
            return 0;
        }
        else if(Input.GetKey(left) && !Input.GetKey(right))
        {
            return -1;
        }
        else 
        {
            return 1;
        }
    }

    private void checkJoystick()
    {
        if(trackpad != null)
        {
                            print("checking");

            if(SteamVR.instance.hmd_ModelNumber == "VIVE_Pro MV")
            {
                if(boolean.GetState(Left))
                {
                    camYMovement = checkHold(trackpad.GetAxis(Left).y);
                }
                if(boolean.GetState(Right))
                {
                    camXMovement = checkHold(trackpad.GetAxis(Right).x);
                    camZMovement = checkHold(trackpad.GetAxis(Right).y);
                }
            }
            else
            {
                camYMovement = checkHold(trackpad.GetAxis(Left).y);
                camXMovement = -1 * checkHold(trackpad.GetAxis(Right).x);
                camZMovement = checkHold(trackpad.GetAxis(Right).y);
            }

        }
    }

    private void checkKeyboard()
    {
        camXMovement = checkHold(KeyCode.D, KeyCode.A);
        camYMovement = checkHold(KeyCode.S, KeyCode.W);
        camZMovement = checkHold(KeyCode.G, KeyCode.T);
    }

    private void updateX()
    {
        switch(camXMovement)
        {
            case 0:
                cameraObject.Translate(new Vector3(0, 0, 0), Space.Self);
                break;
            case 1:
                cameraObject.Translate(new Vector3(-500f * Time.deltaTime, 0, 0), Space.Self);
                break;
            case -1:
                cameraObject.Translate(new Vector3(500f * Time.deltaTime, 0, 0), Space.Self);
                break;
        }
    }

    private void updateZ()
    {
        switch(camZMovement)
        {
            case 0:
                cameraObject.Translate(new Vector3(0, 0, 0), Space.Self);
                break;
            case 1:
                cameraObject.Translate(new Vector3(0, 0, 500f * Time.deltaTime), Space.Self);
                break;
            case -1:
                cameraObject.Translate(new Vector3(0, 0, -500f * Time.deltaTime), Space.Self);
                break;
        }
    }

    private void updateY()
    {
        switch(camYMovement)
        {
            case 0:
                cameraObject.Translate(new Vector3(0, 0, 0), Space.Self);
                break;
            case 1:
                cameraObject.Translate(new Vector3(0, 500f * Time.deltaTime, 0), Space.Self);
                break;
            case -1:
                cameraObject.Translate(new Vector3(0, -500f * Time.deltaTime, 0), Space.Self);
                break;
        }
    }
}