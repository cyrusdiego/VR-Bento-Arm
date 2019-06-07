using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bicep : MonoBehaviour
{
    private ConfigurableJoint cj = null;
    private Rigidbody rb = null;
    private JointDrive motor;

    private float damp = 1;
    private float motorTorque = 1319000f;
    private float maxSpeedLimit = 0.61f;
    private Quaternion targetRotation;
    private bool target = true;

    void Start()
    {
        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();

        cj.angularXDrive = motor;
        motor.positionDamper = motorTorque / maxSpeedLimit;
        cj.rotationDriveMode = RotationDriveMode.XYAndZ;
    }
    void FixedUpdate()
    {
        if(Input.GetAxis("THUMBSTICK_HORIZONTAL_RIGHT") >= 0.5)
        {
            cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(maxSpeedLimit,0,0);
            motor.maximumForce = motorTorque;
            motor.positionSpring = 0;
            cj.angularXDrive = motor;
        }
        else if(Input.GetAxis("THUMBSTICK_HORIZONTAL_RIGHT") <= -0.5)
        {
            cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(-maxSpeedLimit,0,0);
            motor.maximumForce = motorTorque;
            motor.positionSpring = 0;
            cj.angularXDrive = motor;

        }
        else
        {
            rb.angularVelocity = Vector3.zero;
            cj.targetAngularVelocity = Vector3.zero;
            motor.maximumForce = motorTorque;
            motor.positionSpring = 0;
            cj.angularXDrive = motor;
            // cj.xMotion = ConfigurableJointMotion.Locked;
            // cj.yMotion = ConfigurableJointMotion.Locked;
            // cj.zMotion = ConfigurableJointMotion.Locked;
            // cj.angularXMotion = ConfigurableJointMotion.Locked;
            // cj.angularYMotion = ConfigurableJointMotion.Locked;
            // cj.angularZMotion = ConfigurableJointMotion.Locked;
        }
    }
}
