/* 
    BLINC LAB VIPER Project 
    Initialization.cs
    Created by: Cyrus Diego July 24, 2019

    Loads the correct task with the specified details: arm shells, arm control,
    and VR Headset connection. Also deals with clean up of VRHeadset when reseting 
    scenes
 */
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Global global = null;

    private int scene = 255;
    private bool armShell = true;
    private bool armControl = true;
    private bool VREnabled = true;

    // have stuff to trigger a destroy in udp connection 
    // something like this:
        // exitRX = true;
        // clientRX.Close();
        // clientTX.Close();
        // Destroy(VRHeadset);
        // if(scene == 1)
        // {
        //     SceneManager.LoadScene("VIPER_SHELLS");
        // }
        // else if(scene == 2)
        // {
        //     SceneManager.LoadScene("VIPER_NoShells");
        // }
        // scene = 0;


    void FixedUpdate()
    {
        if(global.loaderPacket[0] != 255)
        {
            scene = global.loaderPacket[0]; 
            armShell = Convert.ToBoolean(global.loaderPacket[1]);
            armControl = Convert.ToBoolean(global.loaderPacket[2]);
            VREnabled = Convert.ToBoolean(global.loaderPacket[3]);  // currently not in use, not until i make two versions

            loadScene();
            resetInitPacket();
        }
    }

    private void loadScene()
    {
        SceneManager.LoadScene(scene + 1);
        global.armShell = armShell;
        global.controlToggle = armControl;
        global.VREnabled = VREnabled;
    }

    private void resetInitPacket()
    {
        for(int i = 0; i < 4; i++)
        {
            global.loaderPacket[i] = 255;
        }
    }
}
