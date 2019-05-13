using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderRotation : MonoBehaviour
{
    public ConfigurableJoint joint = null;
    public JointDrive motor;
    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
