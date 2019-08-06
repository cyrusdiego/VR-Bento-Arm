using System;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public Global global = null;

    void Awake()
    {
        global.timer = false;
    }

    void FixedUpdate()
    {
        if(!global.timer)
        {
            foreach(Tuple<float,float> el in global.brachIOplexusControl)
            {
                if(el.Item1 != 0)
                {
                    global.timer = true;
                }
            }
        }
    }
}
