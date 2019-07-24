using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using Valve.VR;

public class Initialization : MonoBehaviour
{
    public Global global = null;

    void Awake()
    {
        for(int i = 0; i < 4; i++)
        {
            global.cameraArray[i] = 255;
            global.loaderPacket[i] = 255;
        }
    }
}
