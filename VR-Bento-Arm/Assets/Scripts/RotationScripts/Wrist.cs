using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrist : MonoBehaviour
{
    private ConfigurableJoint cj = null;
    private Rigidbody rb = null;
    private JointDrive motor;
    private float maxSpeedLimit = 1.03f;
    private float motorTorque = 611000;
    private Quaternion targetRotation;
    private bool target = true;

    void Start()
    {
        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();
        motor.positionDamper = motorTorque / maxSpeedLimit;
        cj.angularXDrive = motor;
        cj.rotationDriveMode = RotationDriveMode.XYAndZ;
    }

    void Update()
    {
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
            rb.angularVelocity = Vector3.zero;
            cj.targetAngularVelocity = Vector3.zero;
            motor.maximumForce = motorTorque;
            motor.positionSpring = 1000000000;
            target = false;

            // cj.xMotion = ConfigurableJointMotion.Locked;
            // cj.yMotion = ConfigurableJointMotion.Locked;
            // cj.zMotion = ConfigurableJointMotion.Locked;
            // cj.angularXMotion = ConfigurableJointMotion.Locked;
            // cj.angularYMotion = ConfigurableJointMotion.Locked;
            // cj.angularZMotion = ConfigurableJointMotion.Locked;
        }
    }
    private void setTargetRotation()
    {
        targetRotation = Quaternion.Euler(-gameObject.transform.localEulerAngles.z,0,0);
        target = false;
    }
}
