using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCupTrigger : MonoBehaviour
{
    public GameTask_Global _gtLogic = null;

    void OnTriggerStay(Collider other)
    {
        if(other.name == "Ball")
        {
            _gtLogic.step1 = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.name == "Ball")
        {
            _gtLogic.step1 = false;
        }
    }
}
