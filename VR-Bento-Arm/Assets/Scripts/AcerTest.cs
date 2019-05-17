using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AcerTest : MonoBehaviour
{
    enum buttons{SELECT_TRIGGER_PRESS_LEFT, SELECT_TRIGGER_PRESS_RIGHT, 
    THUMBSTICK_HORIZONTAL_LEFT, THUMBSTICK_HORIZONTAL_RIGHT, 
    THUMBSTICK_VERTICAL_LEFT, THUMBSTICK_VERTICAL_RIGHT,
    TOUCHPAD_HORIZONTAL_LEFT, TOUCHPAD_HORIZONTAL_RIGHT, 
    TOUCHPAD_VERTICAL_LEFT, TOUCHPAD_VERTICAL_RIGHT, GRIP_BUTTON_PRESS_LEFT, 
    GRIP_BUTTON_PRESS_RIGHT, GRIP_BUTTON_SQUEEZE_LEFT, GRIP_BUTTON_SQUEEZE_RIGHT,
    MENU_BUTTON_LEFT, MENU_BUTTON_RIGHT, TOUCHPAD_TOUCH_LEFT, 
    TOUCHPAD_TOUCH_RIGHT, TOUCHPAD_PRESS_LEFT, TOUCHPAD_PRESS_RIGHT, 
    SELECT_TRIGGER_SQUEEZE_LEFT, SELECT_TRIGGER_SQUEEZE_RIGHT, 
    THUMBSTICK_PRESS_LEFT, THUMBSTICK_PRESS_RIGHT};

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey){
            Debug.Log("got a press");
        }
        
    }
}
