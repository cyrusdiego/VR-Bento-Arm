﻿   /* 
    BLINC LAB VIPER Project 
    armShellController.cs
    Created by: Cyrus Diego July 22, 2019

    This class toggles the arm shells for the bento arm upon task launch
    BrachIOplexus will send a signal that will toggle the arm shells on or off
 */
using UnityEngine;

public class armShellController : MonoBehaviour
{
    // Array that holds the arm shells
    public GameObject[] armShells = new GameObject[3];
    // Array that hold the metal structure under the arm shell
    public GameObject[] armSkeleton = new GameObject[3];
    public Global global = null;

    void FixedUpdate()
    {
        // If the armshell are enabled
        if(global.armShell)
        {
            // enables arm shells 
            for(int i = 0; i < armShells.Length; i++)
            {
                armShells[i].SetActive(true);
            }
            // disabled box colliders under arm shells 
            for(int i = 0; i < armSkeleton.Length; i++)
            {
                // Array to hold box collider components 
                BoxCollider[] boxes;
                
                // Assigns the array to the array of box colliders for each game object
                boxes = armSkeleton[i].GetComponents<BoxCollider>();
                for(int j = 0; j < boxes.Length; j++)
                {
                    boxes[j].enabled = false;
                }
            }
        }
        else
        {
            // disables armshells 
            for(int i = 0; i < armShells.Length; i++)
            {
                armShells[i].SetActive(false);
            }
            // enables box colliders under arm shells
            for(int i = 0; i < armSkeleton.Length; i++)
            {
                // Array to hold box collider components 
                BoxCollider[] boxes;

                // Assigns the array to the array of box colliders for each game object
                boxes = armSkeleton[i].GetComponents<BoxCollider>();
                for(int j = 0; j < boxes.Length; j++)
                {
                    boxes[j].enabled = true;
                }
            }
        }
    }
}
