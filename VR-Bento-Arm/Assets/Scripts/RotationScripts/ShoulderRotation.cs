using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderRotation : RotationBase
{
    void Start()
    {
        // Shoulder rotates about local y axis
        setRotationAxis(2);

        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();
        go = gameObject;

        motorTorque = 1319000f;
        maxSpeedLimit = 0.61f;

    }

    void FixedUpdate()
    {
        getAxis(Input.GetAxis("THUMBSTICK_HORIZONTAL_LEFT"));
    }
}