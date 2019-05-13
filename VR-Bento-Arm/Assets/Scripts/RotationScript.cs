/* BLINC LAB VR-BENTO-ARM Project
 * RotationScript.cs 
 * Created by: Cyrus Diego May 7, 2019 
 * 
 * Controls rotation of the virtual bento arm using a slider and button GUI on screen
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    private Dictionary<string, Rigidbody> robotRigidBody = new Dictionary<string, Rigidbody>();
    private Dictionary<string, Tuple<float, float>> torque_velocityVals = new Dictionary<string, Tuple<float, float>>();

    private string[] rigidBodyNames = { "Shoulder", "Elbow", "Forearm Rotation", "Wrist Flexion", "Open Hand" };
    public float[] torqueVals = { 1.319f, 2.436f, 0.733f, 0.611f, 0.977f };
    public float[] velocityVals = { 6.27f, 5.13f, 737f, 9.90f, 9.90f }; // rpm 

    public GameObject[] shells = new GameObject[6];
    public Rigidbody[] rigidBodies = new Rigidbody[5];

    public float turnRate = 0.2f;
    private float sliderValue = 0.0f;
    private string mode;
    private int modeitr = 0;

    private HingeJoint joint = null;
    private JointMotor motor;

    // Start is called before the first frame update
    void Start()
    {
        mode = rigidBodyNames[modeitr++];  // sets the first mode 

        // convert to rad / s
        for(int j = 0; j < 5; j++)
        {
            velocityVals[j] *= (Mathf.PI / 30);
        }

        int i = 0;
        foreach (Rigidbody rigidbody in rigidBodies)// adds to the dictionaries for easy lookup 
        {
            robotRigidBody.Add(rigidBodyNames[i], rigidbody);  
            torque_velocityVals.Add(rigidBodyNames[i], new Tuple<float, float>(torqueVals[i], velocityVals[i]));
            i++;
        }
        setKinematic();
        setJointMotor();
        Debug.Log(joint);
    }

    private void setJointMotor()
    {
        joint = robotRigidBody[mode].gameObject.GetComponent<HingeJoint>();
        motor = joint.motor;
    }

    // stops the movement of all the joints in between modes 
    private void setAngularVelocity()
    {
        foreach (string name in rigidBodyNames)
        {
            robotRigidBody[name].angularVelocity = Vector3.zero;
        }

    }

    // Ensures the current joint being rotated is not a kinematic object and the rest are 
    private void setKinematic()
    {
        for(int i = modeitr % 5; i < 5; i++)
        {
            robotRigidBody[rigidBodyNames[i]].isKinematic = true;
        }
        robotRigidBody[mode].isKinematic = false;
    }

    /*
     * Rotates the rigid body specified by mode about a specific axes  
     */ 

    private void rotateX()
    {
        robotRigidBody[mode].AddRelativeTorque(sliderValue * turnRate, 0, 0);
    }
    private void rotateY()
    {
        robotRigidBody[mode].AddRelativeTorque(0, sliderValue * turnRate, 0);
    }
    private void rotateZ()
    {
        robotRigidBody[mode].AddRelativeTorque(0, 0, sliderValue * turnRate);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // rotates the rigid body about specific axes based on the mode 
        //switch (modeitr % 5)
        //{
        //    case 0:
        //        rotateY();
        //        break;
        //    case 1:
        //        rotateY();
        //        break;
        //    case 2:
        //        rotateZ();
        //        break;
        //    case 3:
        //        rotateX();
        //        break;
        //    case 4:
        //        rotateZ();
        //        break;

        //}
        if(sliderValue == 0)
        {
            motor.force = 0;
            motor.targetVelocity = 0;
        } else
        {
            motor.force = torque_velocityVals[mode].Item1;
            motor.targetVelocity = torque_velocityVals[mode].Item2;
        }

        joint.motor = motor;
        joint.useMotor = true;
    }

    // Creates buttons and slider 
    void OnGUI()
    {
        // Hides arm-shells, 8020 stand, Table, and Desktop-Workspace
        if (GUI.Button(new Rect(10, 25, 100, 20), "Hide"))
        {
            foreach (GameObject shell in shells)
            {
                shell.SetActive(!shell.activeSelf);
            }
        }

        // creates slider to move the arm 
        sliderValue = GUI.HorizontalSlider(new Rect(10, 75, 100, 30), sliderValue, -10.0f, 10.0f);

        // resets the slider when mouse is released 
        if (Input.GetMouseButtonUp(0)) // the "0" is refering to a button mapping
        {
            sliderValue = 0;

        }

        // button to alternate between joints / freedom of movements 
        if (GUI.Button(new Rect(10, 50, 150, 20), mode))
        {
            mode = rigidBodyNames[modeitr++ % 5];  // changes the mode 
            setJointMotor();
            setKinematic();  // cycles through the rigid - bodies to set "isKinematic" property 
            setAngularVelocity();  // resets the angular velocity of all the joints to ensure it stops moving 
        }


    }
}
