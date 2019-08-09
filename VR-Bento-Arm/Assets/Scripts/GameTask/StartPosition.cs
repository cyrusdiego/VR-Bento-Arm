using UnityEngine;
using System;
public class StartPosition : MonoBehaviour
{

    public Global global = null;
    public GameObject shoulder = null;
    public GameObject elbow = null;
    private bool shoulderReady;
    private bool elbowReady;
    void Awake()
    {
        global.startup = true;
        shoulderReady = false;
        elbowReady = false;
        global.brachIOplexusControl[0] = new Tuple<float, float>(2,0.7f);
        global.brachIOplexusControl[1] = new Tuple<float, float>(1,0.7f);
        global.brachIOplexusControl[2] = new Tuple<float, float>(0,0);
        global.brachIOplexusControl[3] = new Tuple<float, float>(0,0);
        global.brachIOplexusControl[4] = new Tuple<float, float>(0,0);
    }

    void FixedUpdate()
    {
        if(shoulder.GetComponent<Transform>().rotation.eulerAngles.y <= 325 && shoulder.GetComponent<Transform>().rotation.eulerAngles.y >= 324) 
        {
            global.brachIOplexusControl[0] = new Tuple<float, float>(0,0);
            shoulderReady = true;
        }
        if(elbow.GetComponent<Transform>().rotation.eulerAngles.x <= 30 && elbow.GetComponent<Transform>().rotation.eulerAngles.x >= 29) 
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
