using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElbowRotation : MonoBehaviour
{
    private ConfigurableJoint cj = null;
    private Rigidbody rb = null;
    private JointDrive motor;

    private float damp = 1000000;
    private float maxSpeedLimit = 0.537f;
    private float motorTorque = 2463000;
    private bool target = true;
    private Quaternion targetRotation;

    void Start()
    {
        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();
        cj.angularXDrive = motor;
        cj.rotationDriveMode = RotationDriveMode.XYAndZ;
        cj.projectionMode = JointProjectionMode.PositionAndRotation;
        cj.projectionAngle = 0.1f;
    }

    void FixedUpdate()
    {      
        if(Input.GetAxis("THUMBSTICK_VERTICAL_RIGHT") >= 0.5)
        {
            cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(maxSpeedLimit,0,0);
            motor.maximumForce = motorTorque;
            motor.positionDamper = motorTorque / (maxSpeedLimit - rb.angularVelocity.x);
            motor.positionSpring = 0;
            cj.angularXDrive = motor;
            target = true;
        }
        else if(Input.GetAxis("THUMBSTICK_VERTICAL_RIGHT") <= -0.5)
        {
            cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(-maxSpeedLimit,0,0);
            motor.maximumForce = motorTorque;
            motor.positionDamper = motorTorque / (maxSpeedLimit - rb.angularVelocity.x);
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
            cj.targetRotation = targetRotation;
            motor.maximumForce = motorTorque;
            motor.positionSpring = 10000000000000;
            motor.positionDamper = 0;
            cj.angularXDrive = motor;
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
        // Grabs rotation about x from the inspector 
        float xInspectorRotation = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x;
        targetRotation = Quaternion.Euler(xInspectorRotation,0,0);
        target = false;
    }
}