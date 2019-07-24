using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

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

    public Parser(){}

    public void parsePacket(ref byte[] packet)
    {
        bool valid;
        byte type;
        valid = validate(ref packet, 4, (byte)(packet.Length - 1));

        if(valid)
        {
            type = packet[2];
            switch(type)
            {
                case 0:
                    Initialization(ref packet);
                    break;
            }
        }
    }

    private void Initialization(ref byte[] packet)
    {
        for(int i = 4; i < packet.Length - 1; i++)
        {
            global.loaderPacket[i - 4] = (int)packet[i];
        }
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

    /*
        @brief: checks if the packet recieved is correct:
        double header: 255
        checksum = ~foreach_servo(id + velocity(l) + velocity(h) + state) 
    */
    private bool validate(ref byte[] packet, byte start, byte end)
    {
        byte checksum = 0;
        for(int i = start; i < end; i++)
        {
            checksum += packet[i];
        }

        if((byte)~checksum > 255)
        {
            checksum = low_byte((UInt16)~checksum);
        }
        else
        {
            checksum = (byte)~checksum;
        }


        if(checksum == packet[packet.Length - 1] && packet[0] == 255 && packet[1] == 255)
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
    private byte calcCheckSum(ref byte[] packet, byte start, byte end)
    {
        byte checkSum = 0;

        for (byte i = start; i < end; i++)
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
