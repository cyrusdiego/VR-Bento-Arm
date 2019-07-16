﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public Global global = null;
    private int sphereTrigger; 

    void Start()
    {
        sphereTrigger = 0;        
    }

    void OnTriggerEnter(Collider other)
    {
        // cleanup by using interactable tag? 
        if((other.name == "Cube" || other.name == "Sphere") && sphereTrigger == 0)
        {
            global.timerTrigger = 1;
            sphereTrigger++;
        }
    }
}
