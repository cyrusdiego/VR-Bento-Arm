using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class NewBehaviourScript : MonoBehaviour
{

    public SteamVR_Input_Sources Left;
    public SteamVR_Action_Single trigger;


    // Update is called once per frame
    void Update()
    {
        print(trigger.GetAxis(Left));
    }
}
