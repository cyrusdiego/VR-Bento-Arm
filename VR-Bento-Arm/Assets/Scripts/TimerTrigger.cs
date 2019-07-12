using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public SceneFeedback feedback = null;

    private void onTriggerEnter(Collider obj)
    {   
        if(obj.name == "Sphere")
        {
            feedback.timerTrigger = 1;
        }
    }
}
