/* 
    BLINC LAB VIPER Project 
    Motor.cs
    Created by Cyrus Diego 

    Attached to each joint of the Bento Arm to simulate the servo motor applying a 
    torque to each joint.
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

    // Temp variables to store the direction and velocity 
    private float direction;
    private float velocity;

    // Angular limits for the joint
    private SoftJointLimit min;
    private SoftJointLimit max;

    /*
        @brief: function called when script instance is being loaded
    */
    void Awake()
    {
        // Gets specified rotational axis from inspector 
        axis = ((Axis)Enum.Parse(typeof(Axis),rotationAxis));

        // Init new joint limit classes 
        min = new SoftJointLimit();
        max = new SoftJointLimit();

        cj = gameObject.GetComponent<ConfigurableJoint>();
        rb = gameObject.GetComponent<Rigidbody>();
        go = gameObject;

        configureCJ();
        configureRB();
        configureJointLimits();
        configureSpeedLimits();
    }

    /*
        @brief: function runs at a fixed rate of 1 / fixed time step
    */
    void FixedUpdate()
    {
        // Checks which input method to read from
        if(global.controlToggle)
        {
            // Read input from brachIOplexus
            direction = global.brachIOplexusControl[arrayIndex].Item1;
            velocity = global.brachIOplexusControl[arrayIndex].Item2;
        }
        else
        {
            //read input from vr controllers
            float value = global.SteamVRControl[arrayIndex];

            // determine direction of the joint
            if(value != 0)
            {
                direction = value / Math.Abs(value) < 0 ? 1 : 2; 
            }
            else
            {
                direction = 0;
            }
            // determine velocity using percentage of the max speed limit 
            velocity = Math.Abs(value) * maxSpeedLimit;
        }
        
        // stops applying torque if max has been reached 
        if(global.maxTorque)
        {
            if((arrayIndex == 1 || arrayIndex == 3) && direction != 1)
            {
                return;
            }
            else
            {
                // persists the state
                global.maxTorque = true; 
            }
        }

        // Moves the joint with specified direction and velocity 
        switch(direction)
        {
            case 0:
                getAxis(0,velocity);
                break;

            case 1:
                global.armActive = true;
                getAxis(-1,velocity);
                break;

            case 2:
                global.armActive = true;
                getAxis(1,velocity);
                break;
        }
        
    }

    /*
        @brief: configures the configurable joint of the module 
    */
    private void configureCJ()
    {
        // Restricts all degrees of freedom except for the joint's x axis 
        cj.xMotion = ConfigurableJointMotion.Locked;
        cj.yMotion = ConfigurableJointMotion.Locked;
        cj.zMotion = ConfigurableJointMotion.Locked;

        cj.angularXMotion = ConfigurableJointMotion.Limited;
        cj.angularYMotion = ConfigurableJointMotion.Locked;
        cj.angularZMotion = ConfigurableJointMotion.Locked;

        // auto determine the anchor point for the joint 
        cj.autoConfigureConnectedAnchor = true;

        // Configure the primary and secondary axis depending on rotational axis
        // Uses right hand rule 
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
        cj.projectionMode = JointProjectionMode.PositionAndRotation;
        cj.projectionAngle = 0.1f;
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

        if(PMax == 270 && PMin == 90)
        {
            cj.angularXMotion = ConfigurableJointMotion.Free;
            return;
        }

        if(actualPMax < actualPMin)
        {
            int temp;
            temp = actualPMin;
            actualPMin = actualPMax;
            actualPMax = temp;
        }

        if(arrayIndex == 3)
        {
            actualPMin = -90;
            actualPMax = 90;
        }

        min.limit = actualPMin;
        max.limit = actualPMax;

        cj.lowAngularXLimit = min;
        cj.highAngularXLimit = max;
    }

    private void configureSpeedLimits()
    {
        byte lowVMin;
        byte lowVMax;
        byte hiVMin;
        byte hiVMax;
        ushort VMin;
        ushort VMax;

        lowVMin = global.jointLimits[(4 * arrayIndex) + 20];
        hiVMin = global.jointLimits[(4 * arrayIndex) + 21];
        lowVMax = global.jointLimits[(4 * arrayIndex) + 22];
        hiVMax = global.jointLimits[(4 * arrayIndex) + 23];

        VMin = (ushort)((lowVMin) | (hiVMin << 8));
        VMax = (ushort)((lowVMax) | (hiVMax << 8));
        
        // int actualVMin;
        float actualVMax;

        // actualVMin = (180 - VMin) - 90;
        actualVMax = VMax * 0.114f * 0.1047f;

        maxSpeedLimit = actualVMax;
    }
}

