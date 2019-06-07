using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopstick : MonoBehaviour
{
    private ConfigurableJoint cj = null;
    private Rigidbody rb = null;
    private JointDrive motor;
    private float maxSpeedLimit = 1.03f;
    private float motorTorque = 978000;
    private bool target = true;
    private Quaternion targetRotation;

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
        if(Input.GetAxis("THUMBSTICK_HORIZONTAL_RIGHT") >= 0.5)
        {
            cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(-maxSpeedLimit,0,0);
            motor.maximumForce = motorTorque;
            motor.positionSpring = 0;
            cj.angularXDrive = motor;
            target = true;
        }
        else if(Input.GetAxis("THUMBSTICK_HORIZONTAL_RIGHT") <= -0.5)
        {
            cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(maxSpeedLimit,0,0);
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
            cj.targetRotation = targetRotation;
            motor.maximumForce = motorTorque;
            motor.positionSpring = 1000000000;
            cj.angularXDrive = motor;
            target = false;

        }
    }
    private void setTargetRotation()
    {
        targetRotation = Quaternion.Euler(-gameObject.transform.localEulerAngles.y,0,0);
        target = false;
    }
}
