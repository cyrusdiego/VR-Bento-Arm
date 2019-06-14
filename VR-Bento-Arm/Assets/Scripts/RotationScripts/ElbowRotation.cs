using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElbowRotation : RotationBase
{
    void Start()
    {
        // Shoulder rotates about local y axis
        setRotationAxis(1);

        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();
        go = gameObject;

        cj.rotationDriveMode = RotationDriveMode.XYAndZ;

        motorTorque = 2463000f;
        maxSpeedLimit = 0.537f;

    }

    void FixedUpdate()
    {
        getAxis(Input.GetAxis("THUMBSTICK_VERTICAL_RIGHT"));
    }
}