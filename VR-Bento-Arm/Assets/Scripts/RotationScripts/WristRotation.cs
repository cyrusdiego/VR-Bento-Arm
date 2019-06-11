using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristRotation: MonoBehaviour
{
    private ConfigurableJoint cj = null;
    private Rigidbody rb = null;
    private JointDrive motor;
    private float maxSpeedLimit = 1.03f;
    private float motorTorque = 611000;
    private float stallTorque = 250000;
    private Quaternion targetRotation;
    private bool target = true;
    private SoftJointLimit lowLimit;
    private SoftJointLimit upperLimit;
    void Start()
    {
        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();
        motor.positionDamper = motorTorque / maxSpeedLimit;
        cj.angularXDrive = motor;
        cj.rotationDriveMode = RotationDriveMode.XYAndZ;
        cj.angularXMotion = ConfigurableJointMotion.Free;
    }

    void Update()
    {
                    rb.constraints = RigidbodyConstraints.None;

        if(Input.GetAxis("TOUCHPAD_HORIZONTAL_RIGHT") >= 0.5)
        {
            cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(maxSpeedLimit,0,0);
            motor.maximumForce = motorTorque;
            motor.positionSpring = 0;
            cj.angularXDrive = motor;
            target = true;

        }
        else if(Input.GetAxis("TOUCHPAD_HORIZONTAL_RIGHT") <= -0.5)
        {
            cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(-maxSpeedLimit,0,0);
            motor.maximumForce = motorTorque;
            motor.positionSpring = 0;
            cj.angularXDrive = motor;
            target = true;
        }
        else
        {
            if(target)
            {
                setTargetRotation();
            }
            // rb.angularVelocity = Vector3.zero;
            cj.targetAngularVelocity = Vector3.zero;
            motor.maximumForce = motorTorque;
            motor.positionSpring = 100000000000000;
            target = false;
            rb.constraints = RigidbodyConstraints.FreezeRotationX;

            cj.xMotion = ConfigurableJointMotion.Locked;
            cj.yMotion = ConfigurableJointMotion.Locked;
            cj.zMotion = ConfigurableJointMotion.Locked;
            cj.angularYMotion = ConfigurableJointMotion.Locked;
            cj.angularZMotion = ConfigurableJointMotion.Locked;
        }
    }
    private void setTargetRotation()
    {
        targetRotation = Quaternion.Euler(-gameObject.transform.localEulerAngles.z,0,0);
        target = false;
    }
}