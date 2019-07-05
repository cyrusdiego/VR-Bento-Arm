using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class test : MonoBehaviour
{
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        List<string> thing = new List<string>(XRSettings.supportedDevices);
        foreach(string i in thing)
        {
            print(i);
        }

    }
}
