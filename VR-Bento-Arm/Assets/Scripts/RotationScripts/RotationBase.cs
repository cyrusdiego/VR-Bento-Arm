using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBase : MonoBehaviour
{
    protected ConfigurableJoint cj = null;
    protected Rigidbody rb = null;
    protected JointDrive motor;

    protected float motorTorque;
    protected float maxSpeedLimit;
    protected Quaternion targetRotation; 
    protected bool target = true;

    protected void getAxis(float axisValue)
    {
        cj.angularXMotion = ConfigurableJointMotion.Free;
        if(axisValue >= 0.5)
        {

        }
        else if(axisValue <= -0.5)
        {

        }
        else
        {
            
        }
    }
}
