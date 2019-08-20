/* 
    BLINC LAB VIPER Project 
    RotationBase.cs 
    Created by: Cyrus Diego June 4, 2019 

    Base class to control motor and arm rotations for each arm segment
 */
using UnityEngine;

public class TrackerRotationBase : MonoBehaviour
{
    // Required components to rotate a joint
    protected ConfigurableJoint cj = null;
    protected Rigidbody rb = null;
    protected GameObject go = null;
    protected JointDrive motor;

    // Motor properties (need to change with brachIOplexus)
    public float motorTorque;
    public float maxSpeedLimit;
    protected Quaternion targetRotation; 
    protected bool target = true;

    // Depening on inherited class, axis of rotation in the joint space is 
    // different.
    protected enum Axis {x,y,z};
    protected Axis axis;
    
    /*
        @brief: rotates the arm segment using the configurable joint
        @param: axis value from specifies joystick / button
    */
    protected void getAxis(float axisValue, float speed)
    {
        // determines the damp value based on which axis
        switch(axis)
        {
            case Axis.x:
                motor.positionDamper = motorTorque /(maxSpeedLimit - rb.angularVelocity.x);
            break;
            case Axis.y:
                motor.positionDamper = motorTorque /(maxSpeedLimit - rb.angularVelocity.y);
            break;
            case Axis.z:
                motor.positionDamper = motorTorque /(maxSpeedLimit - rb.angularVelocity.z);
            break;
        }

        motor.maximumForce = motorTorque;
        motor.positionSpring = 0;
        // Rotates CW or CCW based on axis values from joystick / button
        if(axisValue >= 0.5)
        {
            // cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(speed,0,0);
            
            cj.angularXDrive = motor;
            target = true;
        }
        else if(axisValue <= -0.5)
        {
            // cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(-speed,0,0);

            cj.angularXDrive = motor;
            target = true;
        }
        else
        {
            if(target)
            {
                setTargetRotation();
            }
            cj.targetAngularVelocity = Vector3.zero;
            cj.targetRotation = targetRotation;

            motor.maximumForce = motorTorque;
            motor.positionSpring = 1000000000;
            motor.positionDamper = 0;
            
            cj.angularXDrive = motor;
            target = false;

            cj.xMotion = ConfigurableJointMotion.Locked;
            cj.yMotion = ConfigurableJointMotion.Locked;
            cj.zMotion = ConfigurableJointMotion.Locked;
            cj.angularYMotion = ConfigurableJointMotion.Locked;
            cj.angularZMotion = ConfigurableJointMotion.Locked;
        }
    }

    /*
        @brief: gets the current rotation from the inspector 
    */
    protected void setTargetRotation()
    {
        float inspectorRotation = 0;
        // Grabs rotation about x from the inspector 
        switch(axis)
        {
            case Axis.x:
                inspectorRotation = UnityEditor.TransformUtils.GetInspectorRotation(go.transform).x;
            break;
            case Axis.y:
                inspectorRotation = UnityEditor.TransformUtils.GetInspectorRotation(go.transform).y;
            break;
            case Axis.z:
                inspectorRotation = UnityEditor.TransformUtils.GetInspectorRotation(go.transform).z;
            break;
        }

        // Convert the rotation angle to quaternions
        targetRotation = Quaternion.Euler(inspectorRotation,0,0);
        target = false;
    }
}
