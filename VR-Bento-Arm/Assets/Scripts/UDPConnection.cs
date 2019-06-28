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
using System.Threading;  // multithreading 
using UnityEngine;
using UnityEngine.SceneManagement;

public class UDPConnection : MonoBehaviour
{
    // Singleoton pattern
    public static UDPConnection udp;

    // Holds direction and velocity
    // roationArray[0] is undefined
    public Tuple<float,float>[] rotationArray = new Tuple<float, float>[6]; 

    // Network information 
    public Int32 portRX = 30004;
    public IPAddress local = IPAddress.Parse("127.0.0.1");

    // .NET classes for multi-threading and establishing a udp connection 
    private UdpClient client;
    private Thread recievingThread;
    private IPEndPoint endpoint;
    private bool exit;

    // Byte array retrieved from brachIOplexus 
    private byte[] packet;

    // convert servo vals -> rpm -> rad/s
    private float rpmToRads = 0.11f * Mathf.PI / 30;   

    private byte scene;

    /*
        @brief: function runs upon startup 
    */
    void Start()
    {
        scene = 0;
        // Singleoton pattern
        if(udp == null)
        {
            DontDestroyOnLoad(gameObject);
            udp = this;
        }
        else if(udp != this)
        {
            Destroy(gameObject);
        }

        clearRotationArray();

        // Initialize udp connection and seperate thread 
        client = new UdpClient(portRX);
        endpoint = new IPEndPoint(local,portRX);
        recievingThread = new Thread(Recieve);

        exit = false;
        recievingThread.Start();
    }

    void Update()
    {
        if(scene != 0)
        {
            exit = true;
            client.Close();
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
    }

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
    bool validate()
    {
        byte checksum = 0;
        for(int i = 4; i < packet.Length - 1; i++)
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
        @brief: using the packet recieved from BrachIOplexus, fill the 
        rotationArray array with velocity and direction values. rotationArray
        is globally accessible to the rotation classes. 
    */
    void parsePacket()
    {
        clearRotationArray();
        if(validate())
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

    /*
        @brief: kills thread when the game object is destroyed 
    */
    void OnDestroy()
    {
        exit = true;
    }

    /*
        @brief: recieves and stores incoming packets from brachIOplexus  
    */
    void Recieve()
    {
        while(!exit)
        {
            try
            {
                packet = client.Receive(ref endpoint); 
                parsePacket();
            }
            catch (Exception err)
            {
                print(err.ToString());            
            }
        }
    }
}
