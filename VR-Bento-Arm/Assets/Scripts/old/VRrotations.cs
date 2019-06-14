/* 
    BLINC LAB VIPER Project 
    RotationScript.cs 
    Created by: Cyrus Diego May 7, 2019 

    Controls rotation of the virtual bento arm using the Acer Headset Controllers
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VRrotations : MonoBehaviour
{
    #region Variables
    // Name : rigidbody storage.
    private Dictionary<string, GameObject> robotGameObject = 
            new Dictionary<string, GameObject>();
    // Name : torque and velocity storage.
    private Dictionary<string, Tuple<float, float>> torqueVelocityVals = 
            new Dictionary<string, Tuple<float, float>>();
    // Name : collision bool storage.
    private Dictionary<string, bool> jointCollision = 
            new Dictionary<string, bool>();
    private Dictionary<string,int> currentNumValues = 
            new Dictionary<string,int>();

    // Rotation modes and motor properties. 
    private string[] robotPartNames = { "Shoulder", "Elbow", "Forearm Rotation", 
            "Wrist Flexion", "Hand" };
    public enum modes {Shoulder, Elbow, Forearm, Wrist, Hand};
    public float[] torqueVals = { 1319, 2436, 733, 611, 977 };
    public float[] velocityVals = { 6.27f, 5.13f, 737f, 9.90f, 9.90f }; // rpm 

    // Drag and Drop in Inspector. 
    public GameObject[] gameObjects = new GameObject[5];

    // Specifies which joint will be rotating. 
    public string mode;
    private int modeItr = 0;

    // Sets properties for the configurable joints. 
    private ConfigurableJoint joint = null;
    private JointDrive motor;
    private SoftJointLimit tempAngle1,tempAngle2;

    // Used for Angular Limits. 
    private float deltaAngle;
    public float[] angleLimits = new float[5];

    // Canvas GUI for VR Headset 
    public Canvas cavasObject = null;
    public Text textObject = null;

    // Left end effector game object
    public GameObject leftEndEffector = null;
    private bool[] mux = new bool[] {false,false,false,false,false,false};
    private bool tempBool = false;
    #endregion
    
    #region MonoAPI

    /*
        @brief: Called before first frame. Initializes and fills
        data structures and properties 
    */
    void Start() 
    {
        // Sets the first mode.
        mode = robotPartNames[modeItr++];  

        // Convert to rad/s.
        for(int j = 0; j < velocityVals.Length; j++) 
        {
            velocityVals[j] *= (Mathf.PI / 30);
        }

        // Adds to the dictionaries for easy lookup.
        int i = 0;
        foreach (GameObject gameobject in gameObjects) 
        {
            // Maps name to rigid body. 
            robotGameObject.Add(robotPartNames[i], gameobject);  

            // Maps name to max torque / velocity. 
            torqueVelocityVals.Add(robotPartNames[i], 
                    new Tuple<float, float>(torqueVals[i], velocityVals[i]));

            // Maps name to current rotation direction. 
            currentNumValues.Add(robotPartNames[i],0);
            jointCollision.Add(robotPartNames[i], false);
            i++;
        }

        // Sets initial settings. 
        SetRigidBody();
        SetRotationAxis();

    }

    void Update()
    {
        CheckTrigger();
    }
    
    /*
        @brief: called once per frame.
    */
    void FixedUpdate()
    {
        textObject.text = mode;
        //Checks if the arm has collided with a box collider 
        if(refreshMux())
        {
            RestrictRotation();
        } 
        else 
        {
            RotateArm();
        }
    }

    #endregion
    
    #region Set

    /*
        @brief: sets the configurable joint settings for the current rotational mode. 

        @param: pass by ref of configurable joint of the current parent object.
    */
    private void SetConfigurableJoint(ref ConfigurableJoint cj)
    {
        cj.anchor = Vector3.zero;
        
        cj.xMotion = ConfigurableJointMotion.Locked;
        cj.yMotion = ConfigurableJointMotion.Locked;
        cj.zMotion = ConfigurableJointMotion.Locked;
        cj.angularYMotion = ConfigurableJointMotion.Locked;
        cj.angularZMotion = ConfigurableJointMotion.Locked;
        cj.autoConfigureConnectedAnchor = true;

        switch(modeItr % 5)
        {
            // Open hand 
            case 0: 
                cj.connectedAnchor = new Vector3(-409,125,0);
                break;
            // Shoulder 
            case 1: 
                cj.connectedAnchor = new Vector3(-101, 130,0);
                break;
            // Elbow
            case 2: 
                cj.connectedAnchor = new Vector3(-130, 125,0);
                break;
            // Forearm 
            case 3:
                cj.connectedAnchor = new Vector3(-325,122,0);
                break;
            // Wrist 
            case 4:
                cj.connectedAnchor = new Vector3(-325,122,-21);
                break;
        }
    }

    /*
        @brief: removes rigid bodies and configurable joints from all parent objects and 
        only adds the compoonents to current parent object. Also sets specific properties for 
        the components. 

        If isKinematic is enabled, Forces, collisions or joints 
        will not affect the rigidbody anymore (Unity API).
    */
    private void SetRigidBody() 
    {
        // Remove components. 
        foreach(String name in robotPartNames)
        {
            GameObject temp = robotGameObject[name];
            if(temp.GetComponent<ConfigurableJoint>())
            {
                Destroy(temp.GetComponent<ConfigurableJoint>());
            }
            if(temp.GetComponent<Rigidbody>())
            {
                Destroy(temp.GetComponent<Rigidbody>());
            }
        }

        // Add components.
        GameObject parent = robotGameObject[mode];
        GameObject child = robotGameObject[robotPartNames[modeItr % 5]];

        // Do not change the order of this 
        if(!child.GetComponent<Rigidbody>())
        {
            child.AddComponent<Rigidbody>();  
        }
        if(!parent.GetComponent<Rigidbody>())
        {
            parent.AddComponent<Rigidbody>(); 
        }
        if(!parent.GetComponent<ConfigurableJoint>())
        {
            parent.AddComponent<ConfigurableJoint>();
        }

        // Set properties. 
        Rigidbody rbParent = parent.GetComponent<Rigidbody>();
        Rigidbody rbChild = child.GetComponent<Rigidbody>();

        rbParent.useGravity = false; 
        rbParent.isKinematic = false; 
        if(rbParent.isKinematic)
        {
            rbParent.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        }
        else
        {
            rbParent.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
        rbParent.inertiaTensorRotation = Quaternion.identity;
        rbParent.constraints = RigidbodyConstraints.FreezePosition;

        rbChild.useGravity = false;
        rbChild.isKinematic = true;
        rbChild.constraints = RigidbodyConstraints.FreezePosition;
        rbChild.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;

        ConfigurableJoint cj = parent.GetComponent<ConfigurableJoint>();
        cj.targetAngularVelocity = Vector3.zero;
        SetConfigurableJoint(ref cj);
        // Sets joint to the configurable joint of current parent object. 
        joint = robotGameObject[mode].GetComponent<ConfigurableJoint>();
        if(mode == "Hand")
        {
            Destroy(child.GetComponent<Rigidbody>());
            Rigidbody leftRB = leftEndEffector.GetComponent<Rigidbody>();

            leftRB.constraints = RigidbodyConstraints.FreezeAll;
            leftRB.useGravity = false;
            leftRB.isKinematic = false;
            leftRB.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }
        else 
        {
            Rigidbody leftRB = leftEndEffector.GetComponent<Rigidbody>();
            leftRB.constraints = RigidbodyConstraints.None;
            leftRB.useGravity = false;
            leftRB.isKinematic = true;
            leftRB.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }
    }

     /*
        @brief: sets the rotational axis depending on the joint.
    */
    private void SetRotationAxis() 
    {
        switch (modeItr % 5) 
        {
            // Open hand 
            case 0: 
                joint.axis = Vector3.down;
                joint.connectedAnchor = new Vector3(-409.5f, 122.7f, 0);
                deltaAngle = Mathf.FloorToInt(joint.transform.localEulerAngles.y);
                break;
            // Shoulder 
            case 1: 
                joint.axis = Vector3.down;
                joint.secondaryAxis = Vector3.forward;
                joint.connectedAnchor = new Vector3(-101.2f,125.1f,0);
                deltaAngle = Mathf.FloorToInt(joint.transform.localEulerAngles.y);
                break;
            // Elbow
            case 2: 
                joint.axis = Vector3.forward;
                joint.secondaryAxis = Vector3.up;
                joint.connectedAnchor = new Vector3(-130.2f, 125.1f, 0f);
                deltaAngle = Mathf.FloorToInt(joint.transform.localEulerAngles.z);
                break;
            // Forearm 
            case 3:
                joint.axis = Vector3.left;
                joint.secondaryAxis = Vector3.up;
                joint.connectedAnchor = new Vector3(-325.2f, 121.1f, 0);
                deltaAngle = Mathf.FloorToInt(joint.transform.localEulerAngles.x);
                break;
            // Wrist 
            case 4:
                joint.axis = Vector3.forward;
                joint.secondaryAxis = Vector3.up;
                joint.connectedAnchor = new Vector3(-325.2f, 121.1f, 0);
                deltaAngle = Mathf.FloorToInt(joint.transform.localEulerAngles.z);
                break;
        }

        // Adjusts the angular limit with the current euler angle. 
        deltaAngle = (deltaAngle > 180) ? Mathf.FloorToInt(deltaAngle - 360) : Mathf.FloorToInt(deltaAngle);  // https://answers.unity.com/questions/554743/how-to-calculate-transformlocaleuleranglesx-as-neg.html
        tempAngle1.limit = -angleLimits[modeItr % 5] - deltaAngle;
        joint.lowAngularXLimit = tempAngle1;
        tempAngle2.limit = angleLimits[modeItr % 5] - deltaAngle;
        joint.highAngularXLimit = tempAngle2;
    }
#endregion

    #region Check

    /*
        @brief: checks if a button is being held / squeezed.

        @param: the button being squeezed. 
    */
    private bool CheckHold(string button)
    {
        if(Input.GetAxis(button) > 0.1)
        {
            return true;
        }
        return false;
    }

    /*
        @brief: checks if a controller button is pressed. 

        @param: the button being pressed down. 
    */
    private bool CheckPress(string button) 
    {
        if(Input.GetButtonDown(button))
        {
            return true;
        } 
        return false;
    }

    /*
        @brief: checks if the robot arm needs to be stopped and if the mode needs to cycle
    */
    private void CheckTrigger()
    {
        if(CheckPress("SELECT_TRIGGER_PRESS_RIGHT"))
        {
            KeyPress(0);
            mode = robotPartNames[modeItr++ % 5]; 
            SetRigidBody();  
            SetRotationAxis();
            CorrectMUX();
        }
    }

#endregion

    #region Rotations

    private void CorrectMUX()
    {
        if(mode == "Shoulder")
        {
            tempBool = mux[1];
            mux[0] = false;
            mux[1] = false;
        }
        if(mode == "Elbow")
        {
            mux[1] = tempBool;
            if(mux[3] && mux[4])
            {
                mux[3] = false;
                mux[4] = false; 
            }
        }
        if(mode == "Forearm")
        {
            mux[3] = false;
            mux[4] = false;
        }
    }

    public void ObjectDetatched(bool msg)
    {
        mux[5] = msg;
        mux[4] = msg;
        mux[3] = msg;
    }

    public void ObjectAttatched(bool msg)
    {
        mux[5] = msg;
        mux[4] = msg;
        currentNumValues[mode] = -1;
    }

    private bool refreshMux()
    {
        // print("mux[0] == " + mux[0]);
        // print("mux[1] == " + mux[1]);
        // print("mux[2] == " + mux[2]);
        // print("mux[3] == " + mux[3]);
        // print("mux[4] == " + mux[4]);
        // print("mux[5] == " + mux[5]);

        switch (modeItr % 5) 
        {
            // Open hand 
            case 0: 
                jointCollision[mode] =  mux[4];
                break;
            // Shoulder 
            case 1: 
                jointCollision[mode] = mux[1];
                break;
            // Elbow
            case 2: 
                jointCollision[mode] = mux[1] || ((mux[3] || mux[4]) && !mux[5]);
                break;
            // Forearm 
            case 3:
                jointCollision[mode] = mux[3] || mux[4];
                break;
            // Wrist 
            case 4:
                jointCollision[mode] = mux[3];
                break;
        }
        // print(jointCollision[mode]);
        return jointCollision[mode];
    }

    /*
        @brief: called by bounding box gameObjects and will set collided to TRUE
        when the box colliders collide with one another. 

        @param: Tuple holding the mode and if that segment of the arm is 
        colliding with another box collider.
    */
    public void CollisionDetection(Tuple<modes,bool> msg) 
    {
        // print("got a msg " + msg.Item1 + " " + msg.Item2 + " " + (int) msg.Item1);
        mux[(int) msg.Item1] = msg.Item2;
    }

    /*
        @brief: specifies joint rotation properties depending on key press 
        (or lack thereof). 

        @param: num specifies what direction the joint rotates.  
    */
    private void KeyPress(int num) 
    {
        joint = robotGameObject[mode].GetComponent<ConfigurableJoint>();
        if(num != 0)
        {
            joint.targetAngularVelocity = new Vector3(torqueVelocityVals[mode].Item2 * num , 0, 0);
            motor.maximumForce = torqueVelocityVals[mode].Item1 * Mathf.Abs(num);
            // These need to be modified, doesnt accurately depict servo motors. 
            motor.positionSpring = 1.0f;
            motor.positionDamper = 1000;
            joint.angularXDrive = motor;

            motor.maximumForce = 0;
            joint.angularYZDrive = motor;
        } 
        else 
        {
            joint.targetAngularVelocity = Vector3.zero;
            motor.maximumForce = torqueVelocityVals[mode].Item1 * Mathf.Abs(num);
            joint.xDrive = motor;

            Rigidbody rb = robotGameObject[mode].GetComponent<Rigidbody>();
            rb.angularVelocity = Vector3.zero;
    
        }
    }

    /*
        @brief: similar to RotateArm(), will only allow the arm to rotate in 
        one direction when it has collided with another box collider. 
    */
    private void RestrictRotation() 
    {
        if(Input.GetAxis("THUMBSTICK_VERTICAL_RIGHT") >= 0.9)
        {
            if(currentNumValues[mode] > 0) 
            {
                KeyPress(0);
                return;
            }
            else 
            {
                KeyPress(1);
                return;
            }
        } 
        else if (Input.GetAxis("THUMBSTICK_VERTICAL_RIGHT") <= -0.9)
        {
            if(currentNumValues[mode] < 0) 
            {
                KeyPress(0);
                return;
            }
            else 
            {
                KeyPress(-1);
                return;
            }
        }
        else
        {
            KeyPress(0);
        }
    }

    /*
        @brief: rotates the joint with specified torque and maximum velocity. 
    */
    private void RotateArm() 
    {
        if(Input.GetAxis("THUMBSTICK_VERTICAL_RIGHT") >= 0.9)
        {
            KeyPress(1);
            currentNumValues[mode] = 1;
            return;
        } 
        else if (Input.GetAxis("THUMBSTICK_VERTICAL_RIGHT") <= -0.9)
        {
            KeyPress(-1);
            currentNumValues[mode] = -1;
            return;
        }
        else 
        {
            KeyPress(0);
        }
    }

#endregion
 
}