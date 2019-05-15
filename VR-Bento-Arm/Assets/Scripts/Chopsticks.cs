using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopsticks : MonoBehaviour
{
    public Transform ChopStick1Transform = null;
    public Transform ChopStick2Transform = null;
    public GameObject Rotations = null;

    // Update is called once per frame
    void FixedUpdate() {
        gameObject.transform.GetChild(0).gameObject.transform.position = ChopStick1Transform.position;
        gameObject.transform.GetChild(1).gameObject.transform.position = ChopStick2Transform.position;

        gameObject.transform.GetChild(0).gameObject.transform.eulerAngles = ChopStick1Transform.eulerAngles;
        gameObject.transform.GetChild(1).gameObject.transform.eulerAngles = ChopStick2Transform.eulerAngles;
        
    }
}
