/* 
    BLINC LAB VIPER Project 
    Feedback.cs
    Created by: Cyrus Diego July 24, 2019

    This class takes the motors of the bento arm and fills position and 
    velovity arrays to be sent to brachIOplexus 
 */
using UnityEngine;


public class Feedback : MonoBehaviour
{
    // Array that holds each module of the Bento Arm
    public GameObject[] motors = null;
    public Global global = null;

    // Temporary variables used to hold angular position and velocity of joint
    private float position;
    private float velocity;

    // Temporary variables used to hold Rigidbody and Transform components of the joint
    private Rigidbody rb;
    private Transform tf;

    /*
        @brief: function called when script instance is being loaded
    */
    void Awake()
    {
        // Creates position and velocity arrays with size of motorCount
        global.position = new float[global.motorCount];
        global.velocity = new float[global.motorCount];
    }

    /*
        @brief: function runs at a fixed rate of 1 / fixed time step
    */
    void FixedUpdate()
    {
        // Loop through each module
        // ** Future Development: avoid using a for loop
        for(int i = 0; i < global.motorCount; i++)
        {
            // Assign the temp variables to Rigidybody and Transform of i'th joint
            rb = motors[i].GetComponent<Rigidbody>();
            tf = motors[i].GetComponent<Transform>();
            
            // Get the angular position and the angular velocity of the i'th joint
            switch(i)
            {
                case 0:
                    position = tf.localRotation.eulerAngles.y;
                    velocity = rb.angularVelocity.y;
                    break;
                case 1:
                    position = tf.localRotation.eulerAngles.x;
                    velocity = rb.angularVelocity.x;
                    break;
                case 2:
                    position = tf.localRotation.eulerAngles.z;
                    velocity = rb.angularVelocity.z;
                    break;
                case 3:
                    position = tf.localRotation.eulerAngles.x;
                    velocity = rb.angularVelocity.x;
                    break;
                case 4:
                    position = tf.localRotation.eulerAngles.y;
                    velocity = rb.angularVelocity.y;
                    break;
            }

            // Fill the position and velocity arrays 
            global.position[i] = position;
            global.velocity[i] = velocity;
        }
    }
}