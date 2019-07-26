using System;
using UnityEngine;

public class Parser : MonoBehaviour
{
    // convert servo vals -> rpm -> rad/s
    private float rpmToRads = 0.11f * Mathf.PI / 30;  

    public Global global = null;

    // Scene triggers
    // Used to hold what scene to switch to 
    // obselete i believe 
    private byte scene;
    private byte activeScene;

    // Used to destroy headset whenever the scene is reloaded
    public GameObject VRHeadset = null;
    // Destroy(VRHeadset);

    public byte[] outgoing;
    public byte[] feedback;
    public bool task;

    private float[] currentPosition = new float[5];
    private float[] currentVelocity = new float[5];

    void Awake()
    {
        outgoing = null;
        task = global.task;
    }

    void FixedUpdate()
    {
        task = global.task;
        if(task)
        {
            sendFeedback();
        }
    }

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
                case 0:
                    print("got loader packet");
                    Loader(ref packet);
                    break;
                case 1:
                    // print("got a control packet");
                    Control(ref packet);
                    break;
                case 2:
                    print("got startup packet");
                    Startup();
                    break;
                case 4:
                    print("got a DOF limits packet");
                    break;
                case 5:
                    print("got a camera packet");
                    Camera(ref packet);
                    break;
                case 6:
                    print("got a scene control packet");
                    SceneControl(ref packet);
                    break;
            }
        }
    }

    private void sendFeedback()
    {
        int startIdx;
        int length;

        length = 0;

        for(int i = 0; i < global.motorCount; i++)
        {
            if(i == 0){
            print("currentPosition[0] == " + currentPosition[0]);
            print("global.position[0] == " + (int)global.position[0]);
            print("currentVelocity[0] == " + currentVelocity[0]);
            print("global.velocity[0] == " + (int)global.velocity[0]);
            }

            if(currentPosition[i] != (int)global.position[i] || currentVelocity[i] != (int)global.velocity[i])
            {
                length += 4;
                currentPosition[i] = (int)global.position[i];
                currentVelocity[i] = (int)global.velocity[i];
            }
        }
        feedback = new byte[length + 5];
        feedback[0] = 255;
        feedback[1] = 255;
        feedback[2] = 3;
        feedback[3] = (byte)(length);

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
            if(i == 0)
            {
                print(lowP + " " + highP);
            }
            startIdx += 4;
        }
        feedback[feedback.Length - 1] = calcCheckSum(ref feedback);
        print("length: " + feedback.Length);
    }

    private void Camera(ref byte[] packet)
    {

    }

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

    private void Control(ref byte[] packet)
    {
        if(global.pause)
        {
            return;
        }
        clearRotationArray();

        int length = packet[3] / 4;
        for(byte i = 1; i < length + 1; i++)
        {
            float direction = packet[4*i + 3];
            float velocity = getVelocity(packet[4*i + 1],packet[4*i + 2]);
            global.brachIOplexusControl[packet[4*i] + 1] = new Tuple<float, float>(direction,velocity);

        }
    }

    private void Loader(ref byte[] packet)
    {
        for(int i = 4; i < packet.Length - 1; i++)
        {
            global.loaderPacket[i - 4] = (int)packet[i];
        }
        global.task = true;
    }

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
        // int start = 4;
        // int end = packet.Length - 1;
        // for (int i = start; i < end; i++)
        // {
        //     checksum += packet[i];
        // }

        // if ((byte)~checksum > 255)
        // {
        //     checksum = low_byte((UInt16)~checksum);
        // }
        // else
        // {
        //     checksum = (byte)~checksum;
        // }


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
    * @brief: calculates the checksum value based on packet recieved
    * formula: ~foreach Servo ID(ID + Vel_Lo + Vel_Hi + State) 
    * 
    * @param: packet to be sent 
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
