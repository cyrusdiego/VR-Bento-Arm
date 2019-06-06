using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bicep : MonoBehaviour
{
    private ConfigurableJoint cj = null;
    private Rigidbody rb = null;
    private JointDrive motor;

    private float positionSpring = 1.0f;
    private float positionDamper = 1000.0f;
    private float effectiveTorque = 13190f;
    private float stallTorque = 60000;
    private float maxSpeedLimit = 650f;

    public GameObject[] armSegments = new GameObject[4];

    void Start()
    {
        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();

        motor.positionSpring = positionSpring;
        motor.positionDamper = positionDamper;
        motor.maximumForce = stallTorque;
    }
    void Update()
    {
        foreach(GameObject obj in armSegments)
        {
            obj.SendMessage("HaltShoulderRotation", false);
        }

        if(Input.GetAxis("THUMBSTICK_HORIZONTAL_RIGHT") >= 0.5)
        {
            motor.maximumForce = effectiveTorque;
            cj.angularXDrive = motor;
            cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(maxSpeedLimit,0,0);
        }
        else if(Input.GetAxis("THUMBSTICK_HORIZONTAL_RIGHT") <= -0.5)
        {
            motor.maximumForce = effectiveTorque;
            cj.angularXDrive = motor;
            cj.angularXMotion = ConfigurableJointMotion.Free;
            cj.targetAngularVelocity = new Vector3(-maxSpeedLimit,0,0);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
            motor.maximumForce = stallTorque;
            cj.angularXDrive = motor;
            cj.targetAngularVelocity = Vector3.zero;
            foreach(GameObject obj in armSegments)
            {
                obj.SendMessage("HaltShoulderRotation", true);
            }
            // cj.xMotion = ConfigurableJointMotion.Locked;
            // cj.yMotion = ConfigurableJointMotion.Locked;
            // cj.zMotion = ConfigurableJointMotion.Locked;
            // cj.angularXMotion = ConfigurableJointMotion.Locked;
            // cj.angularYMotion = ConfigurableJointMotion.Locked;
            // cj.angularZMotion = ConfigurableJointMotion.Locked;
        }
    }
}
