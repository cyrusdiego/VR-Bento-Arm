using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class Initialization : Singleton<Initialization>
{

    public GameObject screenCamera = null;

    public enum PlatformSelection 
    {
        NULL,
        ACER,
        VIVE
    }

    public PlatformSelection platformSelection
    {
        get { return _platformSelection;}
    }

    private PlatformSelection _platformSelection = PlatformSelection.NULL;

    void Awake()
    {
        List<string> supportedDeviceList = new List<string>(XRSettings.supportedDevices);

        #if (UNITY_STANDALONE_WIN) && !UNITY_WSA_10_0 //  && !UNITY_EDITOR
            if(supportedDeviceList.Contains("OpenVR"))
            {
                LoadPlatform(PlatformSelection.VIVE);
                return;
            }
        #elif UNITY_WSA_10_0 // !UNITY_EDITOR && 
            if(supportedDeviceList.Contains("WindowsMR"))
            {
                LoadPlatform(PlatformSelection.ACER);
                return;
            }
        #endif

        if(_platformSelection == PlatformSelection.NULL) 
        {
            LoadPlatform(PlatformSelection.NULL);
        }
    }

    public void LoadPlatform(PlatformSelection platform)
    {
        _platformSelection = platform;
        switch(platform)
        {
            case PlatformSelection.NULL:
                print("ERROR: HEADSET NOT FOUND");
                break;
            case PlatformSelection.ACER:
                print("DETECTED DEVICE: Acer Windows Mixed Reality Headset");
                SceneManager.LoadScene("VIPER_WMR");
                break;
            case PlatformSelection.VIVE:
                print("DETECTED DEVICE: HTC Vive Headset");
                SceneManager.LoadScene("VIPER_VIVE");
                break;
        }
    }
}
