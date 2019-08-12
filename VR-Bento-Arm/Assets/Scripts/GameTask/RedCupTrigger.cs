using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCupTrigger : MonoBehaviour
{
    public GameTask_Global _gtLogic = null;

    void OnTriggerStay(Collider other)
    {
        if(other.name == "Cube")
        {
            _gtLogic.step2 = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.name == "Cube")
        {
            _gtLogic.step2 = false;
        }
    }
}
