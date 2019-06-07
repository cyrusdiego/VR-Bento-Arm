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
    private float hKeyPressTime,gKeyPressTime;
    private float minTime = 0.01f;
    private bool keyPress = false;
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
        keyPress = false;
        if (Input.GetKeyDown(KeyCode.G)) {
            gKeyPressTime = Time.timeSinceLevelLoad;
        }
        if (Input.GetKey(KeyCode.G)) {
            if (Time.timeSinceLevelLoad - gKeyPressTime > minTime) {
                cj.angularXMotion = ConfigurableJointMotion.Free;
                cj.targetAngularVelocity = new Vector3(-maxSpeedLimit,0,0);
                motor.maximumForce = motorTorque;
                motor.positionSpring = 0;
                cj.angularXDrive = motor;
                target = true;
                keyPress = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.H)) {
            hKeyPressTime = Time.timeSinceLevelLoad;
        }
        if (Input.GetKey(KeyCode.H)) {
            if (Time.timeSinceLevelLoad - hKeyPressTime > minTime) {
                cj.angularXMotion = ConfigurableJointMotion.Free;
                cj.targetAngularVelocity = new Vector3(maxSpeedLimit,0,0);
                motor.maximumForce = motorTorque;
                motor.positionSpring = 0;
                cj.angularXDrive = motor;
                target = true;
                keyPress = true;
            }
        }
        if(!keyPress)
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
            keyPress = false;
        }
    }
    private void setTargetRotation()
    {
        targetRotation = Quaternion.Euler(-gameObject.transform.localEulerAngles.y,0,0);
        target = false;
    }
}
