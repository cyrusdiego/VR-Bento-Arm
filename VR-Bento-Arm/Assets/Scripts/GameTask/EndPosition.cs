using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPosition : MonoBehaviour
{
    public Global global = null;
    public GameTask_Global _gtLogic = null;
    public GameObject shoulder = null;
    public GameObject elbow = null;
    private Transform shoulderTransform = null;
    private Transform elbowTransform = null;
    private int timerCounter;

    void Awake()
    {
        timerCounter = 0;
        shoulderTransform = shoulder.GetComponent<Transform>();
        elbowTransform = elbow.GetComponent<Transform>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        int shoulderAngle;
        int elbowAngle;

        shoulderAngle = (int)shoulderTransform.rotation.eulerAngles.y;
        elbowAngle = (int)elbowTransform.rotation.eulerAngles.x;

        if(shoulderAngle == 304 && elbowAngle == 44 && _gtLogic.step1 && _gtLogic.step2)
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
