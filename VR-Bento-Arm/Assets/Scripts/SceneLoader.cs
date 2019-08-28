/* 
    BLINC LAB VIPER Project 
    Initialization.cs
    Created by: Cyrus Diego July 24, 2019

    Loads the correct task with the specified details: arm shells, arm control,
    and VR Headset connection. 
 */
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class SceneLoader : MonoBehaviour
{
    public Global global = null;

    // Defaults values for the scene
    private int scene = 255;
    private bool armShell = true;
    private bool armControl = true;
    private bool VREnabled = true;

    /*
        @brief: function runs at a fixed rate of 1 / fixed time step
    */
    void FixedUpdate()
    {
        if(global.loaderPacket[0] != 255)
        {
            // Retrieve input from brachIOplexus with regards to loading the scene 
            scene = global.loaderPacket[0]; 
            armShell = Convert.ToBoolean(global.loaderPacket[1]);
            armControl = Convert.ToBoolean(global.loaderPacket[2]);
            VREnabled = Convert.ToBoolean(global.loaderPacket[3]);  // currently not in use, not until i make two versions

            loadScene();
            resetInitPacket();
        }
    }

    /*
        @brief: loads the specified scene using the scene index and sets parameters for the scene 
    */
    private void loadScene()
    {
        // Loads the scene using the index provided 
        // Note: scene index in Unity is + 1 from brachIOplexus because VIPER_INIT is the first scene in the build 
        SceneManager.LoadScene(scene + 1);

        // The tracker scene cannot have too high have a refresh rate
        if((scene + 1) == 3)
        {
            SteamVR.settings.lockPhysicsUpdateRateToRenderFrequency = true;
        }
        else
        {
            SteamVR.settings.lockPhysicsUpdateRateToRenderFrequency = false;
        }
        // Sets global states
        global.armShell = armShell;
        global.controlToggle = armControl;
        global.VREnabled = VREnabled;
    }

    /*
        @brief: clears the loader packet so that FixedUpdate doesn't keep reloading the scene 
    */
    private void resetInitPacket()
    {
        for(int i = 0; i < 4; i++)
        {
            global.loaderPacket[i] = 255;
        }
    }
}
