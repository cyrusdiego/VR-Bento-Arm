using System.Collections;
using System;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private float motorTorque;
    private float maxSpeedLimit;
    private Quaternion targetRotation;

    public Rotation(float motorTorque, float maxSpeedLimit)
    {
        this.motorTorque = motorTorque;
        this.maxSpeedLimit = maxSpeedLimit;
    }
    public void positiveJoystick(ref ConfigurableJoint cj, ref JointDrive motor, ref Rigidbody rb)
    {
        cj.angularXMotion = ConfigurableJointMotion.Free;
        cj.targetAngularVelocity = new Vector3(maxSpeedLimit,0,0);
        motor.maximumForce = motorTorque;
        motor.positionSpring = 0;
        motor.positionDamper = motorTorque / maxSpeedLimit;
        cj.angularXDrive = motor;
    }
    public void negativeJoystick(ref ConfigurableJoint cj, ref JointDrive motor, ref Rigidbody rb)
    {
        cj.angularXMotion = ConfigurableJointMotion.Free;
        cj.targetAngularVelocity = new Vector3(-maxSpeedLimit,0,0);
        motor.maximumForce = motorTorque;
        motor.positionDamper = motorTorque / maxSpeedLimit;
        motor.positionSpring = 0;
        cj.angularXDrive = motor;
    }
    public void zeroJoystick(ref ConfigurableJoint cj, ref JointDrive motor, ref Rigidbody rb)
    {
        cj.targetAngularVelocity = Vector3.zero;
        cj.targetRotation = targetRotation;
        motor.maximumForce = motorTorque;
        motor.positionSpring = 1000000000;
        motor.positionDamper = 0;
        cj.angularXDrive = motor;
        
        cj.xMotion = ConfigurableJointMotion.Locked;
        cj.yMotion = ConfigurableJointMotion.Locked;
        cj.zMotion = ConfigurableJointMotion.Locked;
        cj.angularYMotion = ConfigurableJointMotion.Locked;
        cj.angularZMotion = ConfigurableJointMotion.Locked;
    }
    public void setTarget(Quaternion targetRotation)
    {
        this.targetRotation = targetRotation;
    }

}
