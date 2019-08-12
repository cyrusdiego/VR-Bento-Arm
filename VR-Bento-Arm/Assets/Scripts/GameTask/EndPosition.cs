using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPosition : MonoBehaviour
{
    public GameTask_Global _gtLogic = null;
    public Global global = null;
    public GameObject shoulder = null;
    public GameObject elbow = null;
    private Transform shoulderTransform = null;
    private Transform elbowTransform = null;

    void Awake()
    {
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
            print("done");
        }
    }
}
