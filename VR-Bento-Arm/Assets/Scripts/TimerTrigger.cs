﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public SceneFeedback feedback = null;
    private int sphereTrigger; 

    void Start()
    {
        sphereTrigger = 0;        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Sphere" && sphereTrigger == 0)
        {
            feedback.timerTrigger = 1;
            sphereTrigger++;
        }
    }
}