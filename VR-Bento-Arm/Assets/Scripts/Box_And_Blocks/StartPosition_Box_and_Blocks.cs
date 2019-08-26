using System;
using UnityEngine;

public class StartPosition_Box_and_Blocks : MonoBehaviour
{
    public Global global = null;
    public Box_And_Blocks_Global _BaBLogic = null;
    public GameObject elbow = null;
    private bool elbowReady;
    private float elbowAngle;
    private float elbowLimit;
    private int timerCounter;

    void Awake()
    {   
        _BaBLogic.ballCounter = 0;
        global.startup = true;
        elbowReady = false;

        elbowLimit = getLimit(1) + 360;

        timerCounter = 0; 

        global.brachIOplexusControl[0] = new Tuple<float, float>(0,0);
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
        elbowAngle = elbow.GetComponent<Transform>().rotation.eulerAngles.x;
        if(elbowAngle >= 43 && elbowAngle <= 44) 
        {
            global.brachIOplexusControl[1] = new Tuple<float, float>(0,0);
            elbowReady = true;
        }

        if(elbowReady && global.armActive) 
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
