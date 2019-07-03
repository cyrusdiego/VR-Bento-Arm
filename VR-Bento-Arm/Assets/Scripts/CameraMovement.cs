using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{            
    public Transform cameraObject = null;
    
    void FixedUpdate()
    {
        if(Input.GetAxis("THUMBSTICK_HORIZONTAL_RIGHT") < 1 && Input.GetAxis("THUMBSTICK_HORIZONTAL_RIGHT") > -1){}
    }

    private int camZMovement = 0, camXMovement = 0, camYMovement = 0;
    // Update is called once per frame
    void Update()
    {
        checkJoystick();
    }

    /*
        @brief: checks if a button is being held / squeezed

        @param: the button being squeezed 
    */
    private int checkHold(string button) {
        if(Input.GetAxis(button) > 0.9){
            return -1;
        } else if(Input.GetAxis(button) < -0.9){
            return 1;
        } else {
            return 0;
        }
    }

    private void checkJoystick(){
        camZMovement = checkHold("THUMBSTICK_VERTICAL_RIGHT");
        switch(camZMovement){
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
        camXMovement = checkHold("THUMBSTICK_HORIZONTAL_RIGHT");
        switch(camXMovement){
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
        camYMovement = checkHold("THUMBSTICK_VERTICAL_LEFT");
        switch(camYMovement){
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
