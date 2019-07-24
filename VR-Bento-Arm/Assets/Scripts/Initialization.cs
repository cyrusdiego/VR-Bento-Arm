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

        global.sent = false;
    }


}
