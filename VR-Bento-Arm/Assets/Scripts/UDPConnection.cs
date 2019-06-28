/* 
    BLINC LAB VIPER Project 
    UDPConection.cs 
    Created by: Cyrus Diego June 17, 2019 

    Connects with BrachIOplexus through UDP connection 
    - used https://www.youtube.com/watch?v=iSxifRKQKAA for the singleoton design
    to make this one instance of the class globally accessible 
 */

using System;
using System.Net;
using System.Net.Sockets; 
using System.Threading;  
using UnityEngine;
using UnityEngine.SceneManagement;

public class UDPConnection : MonoBehaviour
{
    #region Variables
    // Singleoton pattern
    public static UDPConnection udp;

    // Holds direction and velocity
    // roationArray[0] is undefined
    public Tuple<float,float>[] rotationArray = new Tuple<float, float>[6]; 

    // Network information 
    public Int32 portRX = 30004;
    public Int32 portTX = 30005;
    public IPAddress local = IPAddress.Parse("127.0.0.1");

    // .NET classes for multi-threading and establishing a udp connection 
    private UdpClient clientRX;
    private UdpClient clientTX;
    private Thread threadRX;
    private Thread threadTX;
    private IPEndPoint endpointRX;
    private IPEndPoint endpointTX;
    private bool exit;

    // convert servo vals -> rpm -> rad/s
    private float rpmToRads = 0.11f * Mathf.PI / 30;   

    private byte scene;
    private byte activeScene;

    #endregion

    #region Unity API

    /*
        @brief: function runs upon startup 
    */
    void Awake()
    {
        scene = 0;
        // Singleoton pattern
        udp = this;

        clearRotationArray();
        if(SceneManager.GetActiveScene().name == "BentoArm_AcerVR")
        {
            activeScene = 0;
        }
        else
        {
            activeScene = 1;
        }

        // Initialize udp connection and seperate thread 
        clientRX = new UdpClient(portRX);
        endpointRX = new IPEndPoint(local,portRX);
        threadRX = new Thread(Recieve);

        clientTX = new UdpClient();
        endpointTX = new IPEndPoint(local,portTX);
        threadTX = new Thread(Send);

        exit = false;
        threadRX.Start();
        threadTX.Start();
    }

    void Update()
    {
        if(scene != 0)
        {
            updateScene();
        }
    }

    /*
        @brief: kills thread when the game object is destroyed 
    */
    void OnDestroy()
    {
        exit = true;
        clientRX.Close();
        clientTX.Close();
    }

    void updateScene()
    {
        exit = true;
        clientRX.Close();
        clientTX.Close();
        if(scene == 1)
        {
            SceneManager.LoadScene("BentoArm_AcerVR");
        }
        else if(scene == 2)
        {
            SceneManager.LoadScene("BentoArm_AcerVRNOARMSHELLS");
        }
        scene = 0;
    }

    #endregion

    #region Utilities

    /*
        @brief: retrieves the lower byte of a UInt16 number
    */
    byte low_byte(ushort number)
    {
        return (byte)(number & 0xff);
    }

    /*
        @brief: checks if the packet recieved is correct:
        double header: 255
        checksum = ~foreach_servo(id + velocity(l) + velocity(h) + state) 
    */
    bool validate(ref byte[] packet, byte start, byte end)
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
        @brief: sets the rotation array to zero's 
    */
    void clearRotationArray()
    {
        for(int i = 0; i < rotationArray.Length; i++)
        {
            rotationArray[i] = new Tuple<float,float>(0,0);
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

    #endregion

    #region UDPTX

    void Send()
    {
        try
        {
            byte[] packet = new byte[4];
            packet[0] = 255;
            packet[1] = 255;
            packet[2] = activeScene;
            packet[3] = calcCheckSum(ref packet,2,3);

            clientTX.Send(packet,packet.Length,endpointTX);
        }
        catch (Exception ex)
        {
            print(ex.ToString());
        }

    }

    #endregion

    #region UDPRX

    /*
        @brief: recieves and stores incoming packets from brachIOplexus  
    */
    void Recieve()
    {
        while(!exit)
        {
            try
            {
                // https://stackoverflow.com/questions/5932204/c-sharp-udp-listener-un-blocking-or-prevent-revceiving-from-being-stuck
                if(clientRX.Available > 0)
                {
                    byte[] packet = clientRX.Receive(ref endpointRX); 
                    parsePacket(ref packet);
                }
            }
            catch (Exception err)
            {
                print(err.ToString());            
            }
        }
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

    /*
        @brief: using the packet recieved from BrachIOplexus, fill the 
        rotationArray array with velocity and direction values. rotationArray
        is globally accessible to the rotation classes. 
    */
    void parsePacket(ref byte[] packet)
    {
        clearRotationArray();
        if(validate(ref packet, 4, (byte)(packet.Length - 1)))
        {
            if(packet[2] == 0)
            {
                int length = packet[3] / 4;
                for(byte i = 1; i < length + 1; i++)
                {
                    float direction = packet[4*i + 3];
                    float velocity = getVelocity(packet[4*i + 1],packet[4*i + 2]);
                    rotationArray[packet[4*i] + 1] = new Tuple<float, float>(direction,velocity);
                }
            }
            else
            {
                if(packet[3] == 1)
                {
                    clearRotationArray();
                }
                scene = packet[4];
            }
        }
    }

    #endregion
}
