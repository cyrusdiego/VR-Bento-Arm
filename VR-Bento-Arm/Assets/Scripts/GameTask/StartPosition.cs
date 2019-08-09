using UnityEngine;
using System;
public class StartPosition : MonoBehaviour
{

    public Global global = null;
    public GameObject shoulder = null;
    private bool shoulderReady;
    private float shoulderAngle;
    private float shoulderLimit;
    private int timerCounter;
    void Awake()
    {
        global.startup = true;
        shoulderReady = false;

        shoulderLimit = getLimit(0) + 360;

        timerCounter = 0; 

        global.brachIOplexusControl[0] = new Tuple<float, float>(2,0.7f);
        global.brachIOplexusControl[1] = new Tuple<float, float>(1,0.7f);
        global.brachIOplexusControl[2] = new Tuple<float, float>(0,0);
        global.brachIOplexusControl[3] = new Tuple<float, float>(0,0);
        global.brachIOplexusControl[4] = new Tuple<float, float>(0,0);
    }

    private int getLimit(int arrayIndex)
    {
        byte lowPMin;
        byte hiPMin;
        ushort PMin;
        int actualPMin;

        lowPMin = global.jointLimits[(4 * arrayIndex) + 0];
        hiPMin = global.jointLimits[(4 * arrayIndex) + 1];

        PMin = (ushort)((lowPMin) | (hiPMin << 8));

        actualPMin = (180 - PMin) - 90;
        return actualPMin;
    }

    void FixedUpdate()
    {
        shoulderAngle = shoulder.GetComponent<Transform>().rotation.eulerAngles.y;
        if(shoulderAngle <= shoulderLimit && shoulderAngle >= (shoulderLimit - 1)) 
        {
            global.brachIOplexusControl[0] = new Tuple<float, float>(0,0);
            shoulderReady = true;
        }

        if(shoulderReady) 
        {
            triggerTimer();
            global.startup = false;
        }
    }

    private void triggerTimer()
    {
        if(timerCounter == 0)
        {
            global.timer = true;
            timerCounter++;
        }
        else
        {
            global.timer = false;
        }
    }

}
