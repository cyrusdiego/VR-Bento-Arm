using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristRotation: RotationBase
{
    void Start()
    {
        // Shoulder rotates about local y axis
        setRotationAxis(1);

        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();
        go = gameObject;

        cj.rotationDriveMode = RotationDriveMode.XYAndZ;

        motorTorque = 611000f;
        maxSpeedLimit = 1.03f;

    }

    void FixedUpdate()
    {
        getAxis(Input.GetAxis("TOUCHPAD_HORIZONTAL_LEFT"));
    }
}