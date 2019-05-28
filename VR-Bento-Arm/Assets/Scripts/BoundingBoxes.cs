/* 
    BLINC LAB VR-BENTO-ARM Project
    BoundingBoxes.cs
    Created by: Cyrus Diego May 27, 2019 

    - Toggles the correct box colliders for the Bento Arm 
    - Toggles arm shells, stand, and table 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBoxes : MonoBehaviour
{
    public GameObject[] armBoxes = new GameObject[3];
    public GameObject[] armShells = new GameObject[5];
    public GameObject rotations = null;
    private int mode = 0, counter = 0;

    /*
        @brief: Called before first frame. Turns of box colliders for the 
        Bento Arm so that only the Bento Arm Shells box colliders are used.
    */
    void Start()
    {
        foreach (GameObject armbox in armBoxes)
        {
            Component[] components = armbox.GetComponents(typeof(BoxCollider));
            foreach (BoxCollider box in components)
            {
                box.enabled = false;
            }
            
        }
    }

    /*
        @brief: Called every frame.
    */
    void Update()
    {
        CheckKeyPress();
    }

    /*
        @brief: checks if a controller button is pressed. 

        @param: the button being pressed down. 
    */
    private bool CheckPress(string button) 
    {
        if(Input.GetButtonDown(button))
        {
            return true;
        } 
        return false;
    }


    /*
        @brief: if the grip button is pressed, the "extras" will be hidden and 
        the correct box colliders will be set.
    */
    private void CheckKeyPress()
    {
        if(CheckPress("GRIP_BUTTON_PRESS_RIGHT"))
        {
            if(mode % 2 == 0)
            {
                foreach (GameObject armshell in armShells)
                {
                    armshell.SetActive(!armshell.activeSelf);
                }
                foreach (GameObject armbox in armBoxes)
                {
                    Component[] components = 
                            armbox.GetComponents(typeof(BoxCollider));
                    foreach (BoxCollider box in components)
                    {
                        box.enabled = true;
                    }
                }
            }
            else 
            {
                foreach (GameObject armshell in armShells)
                {
                    armshell.SetActive(!armshell.activeSelf);
                }
                foreach (GameObject armbox in armBoxes)
                {
                    Component[] components = 
                            armbox.GetComponents(typeof(BoxCollider));
                    foreach (BoxCollider box in components)
                    {
                        box.enabled = false;
                    }
                }
            }
            mode++;
        }
    }       
}
