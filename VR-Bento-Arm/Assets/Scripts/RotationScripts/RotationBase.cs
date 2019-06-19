/* 
    BLINC LAB VIPER Project 
    RotationBase.cs 
    Created by: Cyrus Diego May 14, 2019 

    Base class to control motor and arm rotations for each arm segment
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBase : MonoBehaviour
{
    protected ConfigurableJoint cj = null;
    protected Rigidbody rb = null;
    protected GameObject go = null;
    protected JointDrive motor;

    protected float motorTorque;
    protected float maxSpeedLimit;
    protected Quaternion targetRotation; 
    protected bool target = true;

    protected enum Axis {x,y,z};
    protected Axis axis;

    /*
        @breif: stores the rotation axis for later use
        @param: integer mapping to an axis
    */
    protected void setRotationAxis(int x)
    {
        if(x == 1)
        {
            axis = Axis.x;
        }
        else if(x == 2)
        {
            axis = Axis.y;
        }
        else
        {
            axis = Axis.z;
        }
    }

    /*
        @brief: rotates the arm segment using the configurable joint
        @param: axis value from specifies joystick / button
    */
    protected void getAxis(float axisValue)
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
            cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(maxSpeedLimit / 2,0,0);
            
            cj.angularXDrive = motor;
            target = true;
        }
        else if(axisValue <= -0.5)
        {
            cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(-maxSpeedLimit,0,0);

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
