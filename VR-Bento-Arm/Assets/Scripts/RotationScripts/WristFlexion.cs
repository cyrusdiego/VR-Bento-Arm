using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristFlexion : RotationBase
{
    void Start()
    {
        // Shoulder rotates about local y axis
        setRotationAxis(1);

        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();
        go = gameObject;

        cj.rotationDriveMode = RotationDriveMode.XYAndZ;
        cj.projectionMode = JointProjectionMode.PositionAndRotation;
        cj.projectionAngle = 0.1f;

        motorTorque = 733000f;
        maxSpeedLimit = 0.771f;

    }

    void FixedUpdate()
    {
        getAxis(Input.GetAxis("TOUCHPAD_VERTICAL_RIGHT"));
    }
}