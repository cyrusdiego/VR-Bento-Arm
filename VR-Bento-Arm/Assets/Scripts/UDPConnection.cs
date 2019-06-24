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
using System.Collections.Generic;  // List
using System.IO; // memory stream
using System.Runtime.Serialization.Formatters.Binary;  // binary serializer 
using System.Threading;  // multithreading 
using UnityEngine;

public class UDPConnection : MonoBehaviour
{
    // Singleoton pattern
    public static UDPConnection udp;

    // Holds direction and velocity
    // roationArray[0] is undefined
    public Tuple<float,float>[] rotationArray = new Tuple<float, float>[6]; 

    // Network information 
    public Int32 port = 30004;
    public IPAddress local = IPAddress.Parse("127.0.0.1");

    // .NET classes for multi-threading and establishing a udp connection 
    private UdpClient client;
    private Thread recievingThread;
    private IPEndPoint endpoint;
    private bool exit;

    // Byte array retrieved from brachIOplexus 
    private byte[] packet;
    private List<UInt16> packetList;

    // convert servo vals -> rpm -> rad/s
    private float rpmToRads = 0.11f * Mathf.PI / 30;   

    /*
        @brief: function runs upon startup 
    */
    void Start()
    {
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
        print("Initializing udp connection with brachIOplexus...");
        client = new UdpClient(port);
        endpoint = new IPEndPoint(local,port);
        recievingThread = new Thread(Recieve);

        exit = false;
        // Start sub thread 
        print("Starting seperate thread...");
        recievingThread.Start();
    }

    /*
        @brief: takes the packet byte array and converts to a List<UInt16>
        @return: List<UInt16> from brachIOplexus to fill packetList  
    */
    List<UInt16> Unpack()
    {

        List<UInt16> incoming = new List<UInt16>();
        var mStream = new MemoryStream();
        var binFormatter = new BinaryFormatter();
        mStream.Write(packet,0,packet.Length);
        mStream.Position = 0;
        incoming = binFormatter.Deserialize(mStream) as List<UInt16>;

        return incoming;
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
        UInt16 checksum = 0;
        for(int i = 4; i < packetList.Count - 1; i++)
        {
            checksum += packetList[i];
        }

        if((UInt16)~checksum > 255)
        {
            checksum = low_byte((UInt16)~checksum);
        }
        else
        {
            checksum = (UInt16)~checksum;
        }


        if(checksum == packetList[packetList.Count - 1] && packetList[0] == 255 && packetList[1] == 255)
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
    float getVelocity(UInt16 low, UInt16 hi)
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
            int length = packetList[3] / 4;
            for(int i = 1; i < length + 1; i++)
            {
                float direction = packetList[4*i + 3];
                float velocity = getVelocity(packetList[4*i + 1],packetList[4*i + 2]);
                rotationArray[packetList[4*i] + 1] = new Tuple<float, float>(direction,velocity);
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
        print("port " + port + " local addr " + local);
        print("Streaming data...");

        while(!exit)
        {
            try
            {
                packet = client.Receive(ref endpoint); 
                packetList = Unpack();
                parsePacket();
            }
            catch (Exception err)
            {
                print(err.ToString());            
            }
        }
    }
}
