/* 
    BLINC LAB VIPER Project 

 */
using UnityEngine;
using System;
public class Motor : RotationBase
{
    // Inspector values
    public BentoControl bentoControl = null;
    public string rotationAxis = "";
    public int arrayIndex = 255;
    public float mass;

    private float direction;
    private float velocity;

    void Start()
    {
        axis = ((Axis)Enum.Parse(typeof(Axis),rotationAxis));

        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();
        go = gameObject;

        configureCJ();
        configureRB();
    }

    void FixedUpdate()
    {
        float direction = bentoControl.rotationArray[arrayIndex].Item1;
        float velocity = bentoControl.rotationArray[arrayIndex].Item2;
        
        switch(direction)
        {
            case 0:
                getAxis(0,velocity);
                break;

            case 1:
                getAxis(-1,velocity);
                break;

            case 2:
                getAxis(1,velocity);
                break;
        }
    }

    private void configureCJ()
    {
        cj.xMotion = ConfigurableJointMotion.Locked;
        cj.yMotion = ConfigurableJointMotion.Locked;
        cj.zMotion = ConfigurableJointMotion.Locked;

        cj.angularXMotion = ConfigurableJointMotion.Free;
        cj.angularYMotion = ConfigurableJointMotion.Locked;
        cj.angularZMotion = ConfigurableJointMotion.Locked;

        cj.autoConfigureConnectedAnchor = true;
        switch(axis)
        {
            case Axis.x:
                cj.axis = new Vector3(-1,0,0);
                cj.secondaryAxis = new Vector3(0,-1,0);
            break;

            case Axis.y:
                cj.axis = new Vector3(0,-1,0);
                cj.secondaryAxis = new Vector3(0, 0, -1);
            break;

            case Axis.z:
                cj.axis = new Vector3(0,0,-1);
                cj.secondaryAxis = new Vector3(-1, 0, 0);
            break;
        }

        cj.anchor = Vector3.zero;
        cj.enableCollision = true;
        // cj.projectionMode = JointProjectionMode.PositionAndRotation;
        // cj.projectionAngle = 0.1f;
        cj.rotationDriveMode = RotationDriveMode.XYAndZ;
    }

    private void configureRB()
    {
        rb.drag = 0;
        rb.mass = mass;
        rb.angularDrag = 0;
        rb.useGravity = false;
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
}

