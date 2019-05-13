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

    private ConfigurableJoint joint = null;
    private JointDrive motor;

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
    }

    private void setJointMotor()
    {
        joint = robotRigidBody[mode].gameObject.GetComponent<ConfigurableJoint>();
        joint.targetRotation = Quaternion.Euler(new Vector3(0, 0, 0));
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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mode == "Elbow")
        {
            joint.connectedAnchor = new Vector3(-130.2f, 125.1f, 0f);
            //robotRigidBody[mode].gameObject.transform.localPosition = new Vector3(-29,-4.32f,0);
        }
        
        if (sliderValue == 0)
        {

            joint.targetAngularVelocity = Vector3.zero;
            motor.maximumForce = 0;
            joint.xDrive = motor;
            joint.targetRotation = Quaternion.Euler(new Vector3(0, 0, 0));


        }
        else if(sliderValue > 0)
        {

            joint.targetAngularVelocity = new Vector3(torque_velocityVals[mode].Item2, 0, 0);

            motor.maximumForce = torque_velocityVals[mode].Item1;
            motor.positionSpring = 1.0f;
            motor.positionDamper = 10000;
            joint.angularXDrive = motor;

        } else
        {
            joint.targetAngularVelocity = new Vector3(torque_velocityVals[mode].Item2 * -1, 0, 0);
            motor.maximumForce = torque_velocityVals[mode].Item1;
            motor.positionSpring = 1.0f;
            motor.positionDamper = 10000;
            joint.angularXDrive = motor;
        }
        Debug.Log(robotRigidBody[mode].gameObject.transform.localPosition);
    }
    private void setRotationAxis()
    {
        joint.axis = Vector3.forward; 
        joint.secondaryAxis = Vector3.up;
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
            setRotationAxis();

        }


    }
}
