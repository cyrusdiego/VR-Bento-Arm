using UnityEngine;
using System;
public class StartPosition : MonoBehaviour
{

    public Global global = null;
    public GameObject shoulder = null;
    public GameObject elbow = null;
    private bool shoulderReady;
    private bool elbowReady;
    private float shoulderAngle;
    private float elbowAngle;
    void Awake()
    {
        global.startup = true;
        shoulderReady = false;
        elbowReady = false;

        getLimit();

        global.brachIOplexusControl[0] = new Tuple<float, float>(2,0.7f);
        global.brachIOplexusControl[1] = new Tuple<float, float>(1,0.7f);
        global.brachIOplexusControl[2] = new Tuple<float, float>(0,0);
        global.brachIOplexusControl[3] = new Tuple<float, float>(0,0);
        global.brachIOplexusControl[4] = new Tuple<float, float>(0,0);
    }

    private void getLimit()
    {
        byte lowPMin;
        byte lowPMax;
        byte hiPMin;
        byte hiPMax;
        ushort PMin;
        ushort PMax;

        lowPMin = global.jointLimits[(4 * arrayIndex) + 0];
        hiPMin = global.jointLimits[(4 * arrayIndex) + 1];
        lowPMax = global.jointLimits[(4 * arrayIndex) + 2];
        hiPMax = global.jointLimits[(4 * arrayIndex) + 3];

        PMin = (ushort)((lowPMin) | (hiPMin << 8));
        PMax = (ushort)((lowPMax) | (hiPMax << 8));
    }

    void FixedUpdate()
    {
        shoulderAngle = shoulder.GetComponent<Transform>().rotation.eulerAngles.y;
        elbowAngle = elbow.GetComponent<Transform>().rotation.eulerAngles.x;
        if(shoulderAngle <= 325 && shoulderAngle >= 324) 
        {
            global.brachIOplexusControl[0] = new Tuple<float, float>(0,0);
            shoulderReady = true;
        }
        if(elbowAngle <= 30 && elbowAngle >= 29) 
        {
            global.brachIOplexusControl[1] = new Tuple<float, float>(0,0);
            elbowReady = true;
        }

        if(elbowReady && shoulderReady) 
        {
            global.startup = false;
        }
    }

}
