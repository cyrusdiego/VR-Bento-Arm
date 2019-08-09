/* 
    BLINC LAB VIPER Project 

 */
using UnityEngine;
using System;
public class Motor : RotationBase
{
    // Inspector values
    public Global global = null;
    public string rotationAxis = "";
    public int arrayIndex = 255;
    public float mass;

    private float direction;
    private float velocity;
    private SoftJointLimit min;
    private SoftJointLimit max;

    void Start()
    {
        Tuple<int, int> limits; 

        axis = ((Axis)Enum.Parse(typeof(Axis),rotationAxis));
        min = new SoftJointLimit();
        max = new SoftJointLimit();

        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();
        go = gameObject;

        configureCJ();
        configureRB();
        configureJointLimits();
    }

    void FixedUpdate()
    {
        if(global.controlToggle)
        {
            direction = global.brachIOplexusControl[arrayIndex].Item1;
            velocity = global.brachIOplexusControl[arrayIndex].Item2;

        }
        else
        {
            float value = global.SteamVRControl[arrayIndex];

            if(value != 0)
            {
                direction = value / Math.Abs(value) < 0 ? 1 : 2; 
            }
            else
            {
                direction = 0;
            }
            velocity = Math.Abs(value) * maxSpeedLimit;
        }
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

        cj.angularXMotion = ConfigurableJointMotion.Limited;
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

    private void configureJointLimits()
    {
        byte lowPMin;
        byte lowPMax;
        byte hiPMin;
        byte hiPMax;
        ushort PMin;
        ushort PMax;

        lowPMin = global.jointLimits[(4 * arrayIndex) + 0];
        hiPMin = global.jointLimits[(4 * arrayIndex) + 1];
        lowPMax = global.jointLimits[(4 * arrayIndex) + 2];
        hiPMax = global.jointLimits[(4 * arrayIndex) + 3];

        PMin = (ushort)((lowPMin) | (hiPMin << 8));
        PMax = (ushort)((lowPMax) | (hiPMax << 8));

        int actualPMin;
        int actualPMax;

        actualPMin = (180 - PMin) - 90;
        actualPMax = 90 - (PMax - 180);

        min.limit = actualPMin;
        max.limit = actualPMax;

        cj.lowAngularXLimit = min;
        cj.highAngularXLimit = max;
    }
}

