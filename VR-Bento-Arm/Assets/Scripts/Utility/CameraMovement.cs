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
    private int camZMovement = 0, camXMovement = 0, camYMovement = 0;
    
    void Start()
    {
        string path = "/actions/default/in/Trackpad";

        Left = SteamVR_Input_Sources.LeftHand;
        Right = SteamVR_Input_Sources.RightHand;
        
        action = SteamVR_Action.FindExistingActionForPartialPath(path);
        trackpad = (SteamVR_Action_Vector2)action;
    }

    // Update is called once per frame
    void Update()
    {
        checkJoystick();
        updateX();
        updateY();
        updateZ();
    }

    /*
        @brief: checks if a button is being held / squeezed

        @param: the button being squeezed 
    */
    private int checkHold(string button) 
    {
        if(Input.GetAxis(button) > 0.8)
        {
            return 1;
        } 
        else if(Input.GetAxis(button) < -0.8)
        {
            return -1;
        } 
        else
        {
            return 0;
        }
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

    private void checkJoystick()
    {
        if(trackpad != null)
        {
            camZMovement = checkHold(trackpad.GetAxis(Right).x);
            camXMovement = checkHold(trackpad.GetAxis(Right).y);
            camYMovement = checkHold(trackpad.GetAxis(Left).y);
        }
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