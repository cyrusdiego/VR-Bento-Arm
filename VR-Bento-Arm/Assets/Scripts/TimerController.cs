using System;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public Global global = null;
    private int triggerCounter = 0;

    void FixedUpdate()
    {
        if(!global.timer && triggerCounter == 0)
        {
            foreach(Tuple<float,float> el in global.brachIOplexusControl)
            {
                if(el.Item1 != 0)
                {
                    global.timer = true;
                    triggerCounter++;
                }
            }
        }
    }
}
