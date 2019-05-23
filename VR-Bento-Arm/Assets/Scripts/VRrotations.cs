/* 
    BLINC LAB VR-BENTO-ARM Project
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

public class VRrotations : MonoBehaviour
{
    #region Variables
    // Name : rigidbody storage.
    private Dictionary<string, Rigidbody> robotRigidBody = 
            new Dictionary<string, Rigidbody>();
    // Name : torque and velocity storage.
    private Dictionary<string, Tuple<float, float>> torqueVelocityVals = 
            new Dictionary<string, Tuple<float, float>>();
    // Name : collision bool storage.
    private Dictionary<string, bool> jointCollision = 
            new Dictionary<string, bool>();
    private Dictionary<string,int> currentNumValues = 
            new Dictionary<string,int>();

    // Rotation modes and motor properties. 
    private string[] rigidBodyNames = { "Shoulder", "Elbow", "Forearm Rotation", 
            "Wrist Flexion", "Open Hand" };
    public float[] torqueVals = { 1.319f, 2.436f, 0.733f, 0.611f, 0.977f };
    public float[] velocityVals = { 6.27f, 5.13f, 737f, 9.90f, 9.90f }; // rpm 

    // Drag and Drop in Inspector. 
    public GameObject[] shells = new GameObject[10];
    public GameObject[] armBoxes = new GameObject[4];
    public Rigidbody[] rigidBodies = new Rigidbody[5];

    // Specifies which joint will be rotating. 
    private string mode;
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
    #endregion
    
    /*
        @brief: Called before first frame. Initializes and fills
        data structures and properties 
    */
    void Start() 
    {
        // Sets the first mode.
        mode = rigidBodyNames[modeItr++];  

        // Convert to rad/s.
        for(int j = 0; j < velocityVals.Length; j++) 
        {
            velocityVals[j] *= (Mathf.PI / 30);
        }

        // Adds to the dictionaries for easy lookup.
        int i = 0;
        foreach (Rigidbody rigidbody in rigidBodies) 
        {
            // Maps name to rigid body. 
            robotRigidBody.Add(rigidBodyNames[i], rigidbody);  

            // Maps name to max torque / velocity. 
            torqueVelocityVals.Add(rigidBodyNames[i], 
                    new Tuple<float, float>(torqueVals[i], velocityVals[i]));

            // Maps name to current rotation direction. 
            currentNumValues.Add(rigidBodyNames[i],0);
            jointCollision.Add(rigidBodyNames[i], false);

            i++;
        } 

        // Sets initial settings. 
        SetKinematic();
        SetJointMotor();
        SetRotationAxis();
        SetBoxColliders();
    }

    
    /*
        @brief: called once per frame.
    */
    void FixedUpdate()
    {
        CheckKeyPress();
        textObject.text = mode; 
        //Checks if the arm has collided with a box collider 
        if(CheckCollision())
        {
            // Debug.Log("INSIDE RESTRICT ROTATION");
            // foreach(var thing in jointCollision){
            //     Debug.Log(thing);
            // }
            RestrictRotation();
        } 
        else 
        {
            RotateArm();
        }
    }

    /*
        @brief: draws the buttons and slider to change modes, hides extras, 
        and rotates joints.
    */
    void OnGUI() 
    {
        // Hides arm-shells, 8020 stand, Table, and Desktop-Workspace.
        if (GUI.Button(new Rect(10, 25, 100, 20), "Hide")) 
        {
            foreach (GameObject shell in shells) 
            {
                shell.SetActive(!shell.activeSelf);
            }
            SetBoxColliders();
        }

        // Button to alternate between joints / freedom of movements. 
        if (GUI.Button(new Rect(10, 50, 150, 20), mode)) 
        {
            mode = rigidBodyNames[modeItr++ % 5];  
            SetJointMotor();
            SetKinematic();  
            SetRotationAxis();
        }
    }
   
    
    IEnumerator ClearConsole()
    {
        // wait until console visible
        while(!Debug.developerConsoleVisible)
        {
            yield return null;
        }
        yield return null; // this is required to wait for an additional frame, without this clearing doesn't work (at least for me)
        Debug.ClearDeveloperConsole();
    }
    #region SetX
    private void SetBoxColliders() 
    {
        if(shells[0].activeSelf)
        {
            foreach(GameObject boxes in armBoxes)
            {
                boxes.SetActive(false);
            }
        } else if(!shells[0].activeSelf)
        {
            foreach(GameObject boxes in armBoxes)
            {
                boxes.SetActive(true);
            }
        }
    }

    /*
        @brief: changes the joint being rotated. 
    */
    private void SetJointMotor() 
    {
        joint = robotRigidBody[mode].gameObject.GetComponent<ConfigurableJoint>();
    }

    /*
        @brief: sets the "isKinematic" property to false for the current joint
        and sets it false for the other joints. 

        If isKinematic is enabled, Forces, collisions or joints 
        will not affect the rigidbody anymore (Unity API).
    */
    private void SetKinematic() 
    {
        for(int i = modeItr % 5; i < 5; i++) 
        {
            robotRigidBody[rigidBodyNames[i]].isKinematic = true;
        }
        robotRigidBody[mode].isKinematic = false;
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
                joint.axis = Vector3.up;
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
                joint.axis = Vector3.back;
                joint.secondaryAxis = Vector3.up;
                joint.connectedAnchor = new Vector3(-130.2f, 125.1f, 0f);
                deltaAngle = Mathf.FloorToInt(joint.transform.localEulerAngles.z);
                break;
            // Forearm 
            case 3:
                joint.axis = Vector3.right;
                joint.secondaryAxis = Vector3.up;
                joint.connectedAnchor = new Vector3(-325.2f, 121.1f, 0);
                deltaAngle = Mathf.FloorToInt(joint.transform.localEulerAngles.x);
                break;
            // Wrist 
            case 4:
                joint.axis = Vector3.back;
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

    #region CheckX
    private bool CheckCollision()
    {
        if(jointCollision[mode])
        {
            return true;
        } 
        else 
        {
            return false;
        }
    }
    
    /*
        @brief:  
    */
    private void CheckKeyPress() 
    {
        // if(Input.GetButtonDown("TOUCHPAD_PRESS_RIGHT"))
        // {
        //     mode = rigidBodyNames[modeItr++ % 5]; 
        //     SetJointMotor();
        //     SetKinematic();  
        //     SetRotationAxis();
        // }

        if(Input.GetButtonDown("GRIP_BUTTON_PRESS_LEFT"))
        {
            foreach (GameObject shell in shells)
            {
                shell.SetActive(!shell.activeSelf);
            }
            SetBoxColliders();
        }
    }

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
#endregion

    #region Rotations

    /*
        @brief: called by bounding box gameObjects and will set collided to TRUE
        when the box colliders collide with one another. 

        @param: Tuple holding the mode and if that segment of the arm is 
        colliding with another box collider.
    */
    public void CollisionDetection(Tuple<string,bool> msg) 
    {
        ClearConsole();
        Debug.Log("msg: " + msg);
        if(Array.IndexOf(rigidBodyNames, mode) > Array.IndexOf(rigidBodyNames, msg.Item1))
        {
            Debug.Log("index of mode is greater than index of msg");
            jointCollision[mode] = msg.Item2;
        } 
        else
        {
            jointCollision[msg.Item1] = msg.Item2;
        } 

        // jointCollision[msg.Item1] = msg.Item2;
        // jointCollision[mode] = msg.Item2;
        ClearConsole();

        // Debug.Log("collision detected: " + msg.Item1 + " " + msg.Item2);
        // foreach(var thing in jointCollision){
        //     Debug.Log(thing);
        // }
        // Debug.Break();
    }

    /*
        @brief: specifies joint rotation properties depending on key press 
        (or lack thereof). 

        @param: num specifies what direction the joint rotates.  
    */
    private void KeyPress(int num) 
    {
        // joint.targetAngularVelocity = 
        //             new Vector3(torqueVelocityVals[mode].Item2 * num, 0, 0);
        // motor.maximumForce = torqueVelocityVals[mode].Item1 * Mathf.Abs(num);

        if(num != 0)
        {
            // These need to be modified, doesnt accurately depict servo motors. 
            motor.positionSpring = 1.0f;
            motor.positionDamper = 1000;
            joint.angularXDrive = motor;
            joint.targetAngularVelocity = 
                    new Vector3(torqueVelocityVals[mode].Item2 * num * 0.5f, 0, 0);
            motor.maximumForce = torqueVelocityVals[mode].Item1 * Mathf.Abs(num);

        } 
        else 
        {
            joint.targetAngularVelocity = 
                    new Vector3(0, 0, 0);
            motor.maximumForce = torqueVelocityVals[mode].Item1 * Mathf.Abs(num);
            joint.xDrive = motor;
        }
        Debug.Log("target angular velocity: " + joint.targetAngularVelocity);
        Debug.Log("maximum force: " + motor.maximumForce);
        // if(CheckCollision()){
        // Debug.Break();

        // }
        // if(mode == "Elbow")
        // {
        //     Debug.Log(joint + " " + joint.targetAngularVelocity + " " + joint.xDrive + " " + motor.maximumForce);
        // }
    }

    /*
        @brief: similar to RotateArm(), will only allow the arm to rotate in 
        one direction when it has collided with another box collider. 
    */
    private void RestrictRotation() 
    {
        Debug.Log("current num val: " + currentNumValues[mode]);
        Debug.Log("controller angle" + InputTracking.GetLocalRotation(XRNode.RightHand).eulerAngles.z);
        if(CheckHold("SELECT_TRIGGER_SQUEEZE_RIGHT"))
        {
            KeyPress(0);
            currentNumValues[mode] = 0;
            if(Input.GetButtonDown("TOUCHPAD_PRESS_RIGHT"))
            {
                mode = rigidBodyNames[modeItr++ % 5]; 
                SetJointMotor();
                SetKinematic();  
                SetRotationAxis();
            }
            return;
        }
        if(InputTracking.GetLocalRotation(XRNode.RightHand).eulerAngles.z > 0 && InputTracking.GetLocalRotation(XRNode.RightHand).eulerAngles.z < 90)
        {
            if(currentNumValues[mode] <= 0) 
            {
                KeyPress(1);
                return;
            } 
            else 
            {
                KeyPress(0);
                return;
            }
        } 
        else if (InputTracking.GetLocalRotation(XRNode.RightHand).eulerAngles.z > 180 && InputTracking.GetLocalRotation(XRNode.RightHand).eulerAngles.z < 360)
        {
            if(currentNumValues[mode] >= 0)
            {
                KeyPress(-1);
                return;
            } 
            else 
            {
                KeyPress(0);
                return;
            }
        }
        
        // if(CheckHold("SELECT_TRIGGER_SQUEEZE_LEFT"))
        // {
        //     if(currentNumValues[mode] < 0) 
        //     {
        //         KeyPress(1);
        //         return;
        //     } 
        //     else 
        //     {
        //         KeyPress(0);
        //         return;
        //     }
        // }
        // else if(CheckHold("SELECT_TRIGGER_SQUEEZE_RIGHT"))
        // {
        //     if(currentNumValues[mode] > 0)
        //     {
        //         KeyPress(-1);
        //         return;
        //     } 
        //     else 
        //     {
        //         KeyPress(0);
        //         return;
        //     }
        // } 
        // KeyPress(0);
    }

    /*
        @brief: rotates the joint with specified torque and maximum velocity. 
    */
    private void RotateArm() 
    {
        if(CheckHold("SELECT_TRIGGER_SQUEEZE_RIGHT"))
        {
            KeyPress(0);
            currentNumValues[mode] = 0;
            if(Input.GetButtonDown("TOUCHPAD_PRESS_RIGHT"))
            {
                mode = rigidBodyNames[modeItr++ % 5]; 
                SetJointMotor();
                SetKinematic();  
                SetRotationAxis();
            }
            return;
        }
        if(InputTracking.GetLocalRotation(XRNode.RightHand).eulerAngles.z > 0 && InputTracking.GetLocalRotation(XRNode.RightHand).eulerAngles.z < 90)
        {
            KeyPress(1);
            currentNumValues[mode] = 1;
            return;
        } 
        else if (InputTracking.GetLocalRotation(XRNode.RightHand).eulerAngles.z > 180 && InputTracking.GetLocalRotation(XRNode.RightHand).eulerAngles.z < 360)
        {
            KeyPress(-1);
            currentNumValues[mode] = -1;
            return;
        }

        // if(CheckHold("SELECT_TRIGGER_SQUEEZE_LEFT")){
        //     KeyPress(1);
        //     currentNumValues[mode] = 1;
        //     return;
        // } else if(CheckHold("SELECT_TRIGGER_SQUEEZE_RIGHT")){
        //     KeyPress(-1);
        //     currentNumValues[mode] = -1;
        //     return;
        // }
        // KeyPress(0);
        // currentNumValues[mode] = 0;
        
    }
#endregion
 
}