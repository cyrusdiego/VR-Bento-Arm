/* 
    BLINC LAB VIPER Project 
    Parser.cs 
    Created by: Cyrus Diego July 23, 2019 

    This class acts as an intermediate between the UDP connection pipeline and 
    the game objects within the task
 */
using System;
using UnityEngine;

public class Parser : MonoBehaviour
{
    // convert servo vals -> rpm -> rad/s
    private float rpmToRads = 0.11f * Mathf.PI / 30;  

    public Global global = null;

    // Used to destroy headset whenever the scene is reloaded
    public GameObject VRHeadset = null;
    // Destroy(VRHeadset);

    // Acknowledgement array used to send to brachIOplexus
    public byte[] outgoing;
    // Feedback array used to send to brachIOplexus
    public byte[] feedback;
    // Flag used to see if a task has been loaded
    public bool task;
    // Flag to start / stop timer
    public bool timer;
    // byte array to send timer feedback
    public byte[] timerFeedback;

    // Array's to hold positions and velocity of bento arm
    private float[] currentPosition = new float[5];
    private float[] currentVelocity = new float[5];

    void Awake()
    {
        outgoing = null;
        task = global.task;
    }

    void FixedUpdate()
    {
        if(global == null)
        {
            print("became null");
        }
        task = global.task;
        timer = global.timer;
        // if(task)
        // {
        //     sendFeedback();
        // }
        if(timer)
        {
            timerToggle();
        }
    }

    /*
        @brief: UDPConnection.cs calls this method to parse the incoming 
        packets from brachIOplexus and determines the course of action
    */
    public void parsePacket(ref byte[] packet)
    {
        bool valid;
        byte type;
        valid = validate(ref packet);

        if(valid)
        {
            type = packet[2];

            switch(type)
            {
                // packet will load the task specified 
                case 0:
                    print("got loader packet");
                    Loader(ref packet);
                    break;
                // packet will control the arm 
                case 1:
                    // print("got a control packet");
                    Control(ref packet);
                    break;
                // packet will initialize startup sequence
                case 2:
                    print("got startup packet");
                    Startup();
                    break;
                // packet will setup DOF limits of arm
                case 4:
                    print("got a DOF limits packet");
                    break;
                // packet will save / move camera positions 
                case 5:
                    print("got a camera packet");
                    Camera(ref packet);
                    break;
                // packet will end / reset / pause scene 
                case 6:
                    print("got a scene control packet");
                    SceneControl(ref packet);
                    break;
            }
        }
    }

    // Using global position and velocity array's fills feedback byte array to send to brachIOplexus
    private void sendFeedback()
    {
        int startIdx;
        feedback = new byte[25];
        feedback[0] = 255;
        feedback[1] = 255;
        feedback[2] = 3;
        feedback[3] = (byte)(25);

        startIdx = 4;

        for(int i = 0; i < global.motorCount; i++)
        {
            if(currentPosition[i] == (int)global.position[i] && currentVelocity[i] == (int)global.velocity[i])
            {
                continue;
            }

            byte lowP;
            byte highP;
            byte lowV;
            byte highV; 

            lowP = low_byte((UInt16)global.position[i]);
            highP = high_byte((UInt16)global.position[i]); 
            lowV = low_byte((UInt16)global.velocity[i]);
            highV = high_byte((UInt16)global.velocity[i]);
 
            feedback[startIdx] = lowP;
            feedback[startIdx + 1] = highP;
            feedback[startIdx + 2] = lowV;
            feedback[startIdx + 3] = highV;

            startIdx += 4;
        }
        feedback[feedback.Length - 1] = calcCheckSum(ref feedback);
    }

    private void Camera(ref byte[] packet)
    {

    }

    // Fills global variables to handle scene control (SceneControl.cs)
    private void SceneControl(ref byte[] packet)
    {
        bool pause;
        bool end;
        bool reset;

        pause = Convert.ToBoolean(packet[4]);
        end = Convert.ToBoolean(packet[5]);
        reset = Convert.ToBoolean(packet[6]);

        global.pause = pause;
        global.end = end;
        global.reset = reset;
    }

    // Fills global variables to handle controlling the arm (Motor.cs)
    private void Control(ref byte[] packet)
    {
        if(global.pause || global.startup)
        {
            return;
        }
        clearRotationArray();

        int length = packet[3] / 4;
        for(byte i = 1; i < length + 1; i++)
        {
            float direction = packet[4*i + 3];
            float velocity = getVelocity(packet[4*i + 1],packet[4*i + 2]);
            global.brachIOplexusControl[packet[4*i]] = new Tuple<float, float>(direction,velocity);

        }
    }

    // Fills global variables to handle loading the correct task (SceneLoader.cs)
    private void Loader(ref byte[] packet)
    {
        for(int i = 4; i < 8; i++)
        {
            global.loaderPacket[i - 4] = (int)packet[i];
        }

        for(int i = 8; i < packet.Length - 1; i++)
        {
            global.jointLimits[i - 8] = packet[i];
        }

        global.task = true;
    }

    // Fills acknowledgement packet to send to brachIOplexus 
    private void Startup()
    {
        outgoing = new byte[6];
        outgoing[0] = 255;
        outgoing[1] = 255;
        outgoing[2] = 2;
        outgoing[3] = 1;
        outgoing[4] = 1;
        outgoing[5] = calcCheckSum(ref outgoing);

        global.sent = true;

    }

    private void timerToggle()
    {
        timerFeedback = new byte[6];
        timerFeedback[0] = 255;
        timerFeedback[1] = 255;
        timerFeedback[2] = 7;
        timerFeedback[3] = 1;
        timerFeedback[4] = 1;
        timerFeedback[5] = calcCheckSum(ref timerFeedback);

        global.timer = false;
    }
    #region Utilities

    /*
        @brief: sets the rotation array to zero's 
    */
    void clearRotationArray()
    {
        for(int i = 0; i < global.brachIOplexusControl.Length; i++)
        {
            global.brachIOplexusControl[i] = new Tuple<float,float>(0,0);
        }
    }


    /*
        @brief: retrieves the lower byte of a UInt16 number
    */
    private byte low_byte(ushort number)
    {
        return (byte)(number & 0xff);
    }

    private byte high_byte(ushort number)
    {
        return (byte)(number >> 8);
    }

    /*
        @brief: checks if the packet recieved is correct:
        double header: 255
        checksum = ~foreach_servo(id + velocity(l) + velocity(h) + state) 
    */
    private bool validate(ref byte[] packet)
    {
        byte checksum = 0;
        checksum = calcCheckSum(ref packet);

        if (checksum == packet[packet.Length - 1] && packet[0] == 255 && packet[1] == 255)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*
        @brief: calculates the checksum value based on packet recieved
        formula: ~foreach Servo ID(ID + Vel_Lo + Vel_Hi + State) 

        @param: packet to be sent 
    */
    private byte calcCheckSum(ref byte[] packet)
    {
        byte checkSum = 0;

        for (byte i = 4; i < packet.Length - 1; i++)
        {
            checkSum += packet[i];
        }

        if ((byte)~checkSum >= 255)
        {
            checkSum = low_byte((UInt16)~checkSum);
        }
        else
        {
            checkSum = (byte)~checkSum;
        }
        return checkSum;
    }

    /*
        @brief: combines low and high byte values and converts to rad /s velocity
        value
    */
    float getVelocity(byte low, byte hi)
    {
        UInt16 combined = (UInt16)((low) | (hi << 8));

        float velocity = combined * rpmToRads;
        return velocity;
    }

#endregion
}
