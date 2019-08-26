using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPosition_Box_and_Blocks : MonoBehaviour
{
    public Global global = null;
    public Box_And_Blocks_Global _BaBLogic = null;
    public GameObject elbow = null;
    private Transform elbowTransform = null;
    private int timerCounter;

    void Awake()
    {
        timerCounter = 0;
        elbowTransform = elbow.GetComponent<Transform>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float elbowAngle = elbow.GetComponent<Transform>().rotation.eulerAngles.x;

        if(elbowAngle >= 43 && elbowAngle <= 44 && _BaBLogic.ballCounter == 5) 
        {
            triggerTimer();
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
