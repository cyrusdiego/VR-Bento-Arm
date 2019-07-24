using UnityEngine;

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

        global.outgoing = null;
        global.sent = false;
        global.controlToggle = true;
        global.armShell = true;
        global.VREnabled = true;
        global.pause = false;
        global.end = false;
        global.reset = false;
    }


}
