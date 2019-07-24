using System;
using UnityEngine;


public class Feedback : MonoBehaviour
{
    public GameObject[] motors = null;
    public Global global = null;

    private float position;
    private float velocity;
    private Rigidbody rb;
    private Transform tf;

    void Start()
    {
        global.position = new float[global.motorCount];
        global.velocity = new float[global.motorCount];
    }
    void FixedUpdate()
    {
        for(int i = 0; i < global.motorCount; i++)
        {
            rb = motors[i].GetComponent<Rigidbody>();
            tf = motors[i].GetComponent<Transform>();
            
            switch(i)
            {
                case 0:
                    position = tf.localRotation.eulerAngles.y;
                    velocity = rb.angularVelocity.y;
                    break;
                case 1:
                    position = tf.rotation.x;
                    velocity = rb.angularVelocity.x;
                    break;
                case 2:
                    position = tf.rotation.z;
                    velocity = rb.angularVelocity.z;
                    break;
                case 3:
                    position = tf.rotation.x;
                    velocity = rb.angularVelocity.x;
                    break;
                case 4:
                    position = tf.rotation.y;
                    velocity = rb.angularVelocity.y;
                    break;
            }

            global.position[i] = position;
            global.velocity[i] = velocity;
        }       
    }
}