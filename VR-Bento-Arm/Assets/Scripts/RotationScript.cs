/* BLINC LAB VR-BENTO-ARM Project
 * RotationScript.cs 
 * Created by: Cyrus Diego May 7, 2019 
 * 
 * Controls rotation of the virtual bento arm using the keyboard 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    // Name : rigidbody and Name : torque and velocity storage 
    private Dictionary<string, Rigidbody> robotRigidBody = 
            new Dictionary<string, Rigidbody>();

    private Dictionary<string, Tuple<float, float>> torque_velocityVals = 
            new Dictionary<string, Tuple<float, float>>();

    // Rotation modes and motor properties 
    private string[] rigidBodyNames = { "Shoulder", "Elbow", "Forearm Rotation", 
            "Wrist Flexion", "Open Hand" };

    public float[] torqueVals = { 1.319f, 2.436f, 0.733f, 0.611f, 0.977f };
    public float[] velocityVals = { 6.27f, 5.13f, 737f, 9.90f, 9.90f }; // rpm 

    // Drag and Drop in Inspector 
    public GameObject[] shells = new GameObject[6];
    public Rigidbody[] rigidBodies = new Rigidbody[5];

    private float sliderValue = 0.0f, currentNumValue;
    private string mode;
    private int modeitr = 0;

    // sets properties for the configurable joints 
    private ConfigurableJoint joint = null;
    private JointDrive motor;
    private SoftJointLimit tempAngle1,tempAngle2;

    // Used for Angular Limits 
    private float deltaAngle;
    public float[] angleLimits = new float[5];

    // Checks if arm has collided with itself or another collider / rigid body 
    private bool collided = false;

    // Keyboard button presses 
    private float pressTime, minTime = 0.01f;

    /*
        @brief: Called before first frame. Initializes and fills
        data structures and properties 
    */
    void Start() {
        // sets the first mode
        mode = rigidBodyNames[modeitr++];  

        // convert to rad / s
        for(int j = 0; j < velocityVals.Length; j++) {
            velocityVals[j] *= (Mathf.PI / 30);
        }

        // adds to the dictionaries for easy lookup 
        int i = 0;
        foreach (Rigidbody rigidbody in rigidBodies) {
            robotRigidBody.Add(rigidBodyNames[i], rigidbody);  
            torque_velocityVals.Add(rigidBodyNames[i], 
                    new Tuple<float, float>(torqueVals[i], velocityVals[i]));
            i++;
        }

        // Sets initial settings 
        setKinematic();
        setJointMotor();
        setRotationAxis();
    }

    /*
        @brief: called once per frame
    */
    void FixedUpdate() {
        // Checks if the arm has collided with a box collider 
        if(!collided){
            rotateArm();
        } else {
            restrictRotation();
        }
    }

    /*
        @brief: called by bounding box gameObjects and will set collided to TRUE
        when the box colliders collide with one another 

        @param: TRUE if box collider collides with another box collider 
    */
    public void collisionDetection(bool msg) {
        collided = msg;
    }

    /*
        @brief: changes the joint being rotated 
    */
    private void setJointMotor() {
        joint = robotRigidBody[mode].gameObject.GetComponent<ConfigurableJoint>();
    }

    /*
        @brief: sets the "isKinematic" property to false for the current joint
        and sets it false for the other joints 

        If isKinematic is enabled, Forces, collisions or joints 
        will not affect the rigidbody anymore (Unity API)
    */
    private void setKinematic() {
        for(int i = modeitr % 5; i < 5; i++) {
            robotRigidBody[rigidBodyNames[i]].isKinematic = true;
        }
        robotRigidBody[mode].isKinematic = false;
    }

    /*
        @brief: checks if a keyboard key is being held 

        @param: the letter being pressed down 
    */
    private bool checkHold(KeyCode letter) {
        if(Input.GetKeyDown(letter)){
            pressTime = Time.timeSinceLevelLoad;
        } 
        if(Input.GetKey(letter)){
            if(Time.timeSinceLevelLoad - pressTime > minTime){
                return true;
            }
        }
        return false;
    }

    /*
        @brief: specifies joint rotation properties depending on key press 
        (or lack thereof) 

        @param: num specifies what direction the joint rotates  
    */
    private void keyPress(int num) {
        joint.targetAngularVelocity = 
                    new Vector3(torque_velocityVals[mode].Item2 * num, 0, 0);
        motor.maximumForce = torque_velocityVals[mode].Item1 * Mathf.Abs(num);

        if(num != 0){
            // These need to be modified, doesnt accurately depict servo motors 
            motor.positionSpring = 1.0f;
            motor.positionDamper = 1000;
            joint.angularXDrive = motor;

        } else {
            joint.xDrive = motor;
            joint.targetRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }

    /*
        @brief: similar to rotateArm(), will only allow the arm to rotate in 
        one direction when it has collided with another box collider 
    */
    private void restrictRotation() {
        if(checkHold(KeyCode.L)){
            if(currentNumValue < 0) {
                keyPress(1);
            } else {
                keyPress(0);
            }
        } else if(checkHold(KeyCode.K)) {
            if(currentNumValue > 0){
                keyPress(-1);
            } else {
                keyPress(0);
            }
        } else if(Input.anyKey == false) {
            keyPress(0);
        } 
    }

    /*
        @brief: rotates the joint with specified torque and maximum velocity 
    */
    private void rotateArm() {
        if(checkHold(KeyCode.L)){
            keyPress(1);
            currentNumValue = 1;
        }
        if(checkHold(KeyCode.K)){
            keyPress(-1);
            currentNumValue = -1;
        }
        if(Input.anyKey == false){
            keyPress(0);
            currentNumValue = 0;
        }
    }

    /*
        @brief: sets the rotational axis depending on the joint
    */
    private void setRotationAxis() {
        switch (modeitr % 5) {
            case 0: // open hand 
                joint.axis = Vector3.up;
                joint.connectedAnchor = new Vector3(-409.5f, 122.7f, 0);
                deltaAngle = Mathf.FloorToInt(joint.transform.localEulerAngles.y);
                break;

            case 1: // shoulder 
                joint.axis = Vector3.down;
                joint.secondaryAxis = Vector3.forward;
                joint.connectedAnchor = new Vector3(-101.2f,125.1f,0);
                deltaAngle = Mathf.FloorToInt(joint.transform.localEulerAngles.y);
                break;

            case 2:// elbow 
                joint.axis = Vector3.back;
                joint.secondaryAxis = Vector3.up;
                joint.connectedAnchor = new Vector3(-130.2f, 125.1f, 0f);
                deltaAngle = Mathf.FloorToInt(joint.transform.localEulerAngles.z);
                break;

            case 3:// forearm 
                joint.axis = Vector3.right;
                joint.secondaryAxis = Vector3.up;
                joint.connectedAnchor = new Vector3(-325.2f, 121.1f, 0);
                deltaAngle = Mathf.FloorToInt(joint.transform.localEulerAngles.x);
                break;

            case 4:// wrist 
                joint.axis = Vector3.back;
                joint.secondaryAxis = Vector3.up;
                joint.connectedAnchor = new Vector3(-325.2f, 121.1f, 0);
                deltaAngle = Mathf.FloorToInt(joint.transform.localEulerAngles.z);
                break;
        }

        // Adjusts the angular limit with the current euler angle 
        deltaAngle = (deltaAngle > 180) ? Mathf.FloorToInt(deltaAngle - 360) : Mathf.FloorToInt(deltaAngle);  // https://answers.unity.com/questions/554743/how-to-calculate-transformlocaleuleranglesx-as-neg.html
        tempAngle1.limit = -angleLimits[modeitr % 5] - deltaAngle;
        joint.lowAngularXLimit = tempAngle1;
        tempAngle2.limit = angleLimits[modeitr % 5] - deltaAngle;
        joint.highAngularXLimit = tempAngle2;
    }


    /*
        @brief: draws the buttons and slider to change modes, hides extras, 
        and rotates joints 
    */
    void OnGUI() {
        // Hides arm-shells, 8020 stand, Table, and Desktop-Workspace
        if (GUI.Button(new Rect(10, 25, 100, 20), "Hide")) {
            foreach (GameObject shell in shells) {
                shell.SetActive(!shell.activeSelf);
            }
        }

        // button to alternate between joints / freedom of movements 
        if (GUI.Button(new Rect(10, 50, 150, 20), mode)) {
            mode = rigidBodyNames[modeitr++ % 5];  // changes the mode 
            setJointMotor();
            setKinematic();  // cycles through the rigid - bodies to set "isKinematic" property 
            setRotationAxis();
        }
    }
}
