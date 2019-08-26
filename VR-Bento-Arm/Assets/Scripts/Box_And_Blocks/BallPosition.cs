using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPosition : MonoBehaviour
{
    public Box_And_Blocks_Global _BaBLogic = null;

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Ball")
        {
            _BaBLogic.ballCounter++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.name == "Ball")
        {
            _BaBLogic.ballCounter--;
        }
    }
}
