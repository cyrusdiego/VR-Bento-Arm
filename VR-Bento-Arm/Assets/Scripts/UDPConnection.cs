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
    private Tuple<float,float>[] rotationArray = new Tuple<float, float>[5]; 

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

        for(int i = 0; i < rotationArray.Length; i++)
        {
            rotationArray[i] = new Tuple<float,float>(0,0);
        }

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
        @brief: takes the packet byte array and parses through it to determine 
        direction and velocity of each motor  
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

    byte low_byte(ushort number)
    {
        return (byte)(number & 0xff);
    }

    bool validate()
    {
        int checksum = 0;
        for(int i = 4; i < packetList.Count - 1; i++)
        {
            checksum += packetList[i];
        }

        if(checksum > 255)
        {
            checksum = low_byte((UInt16)~checksum);
        }
        else
        {
            checksum = ~checksum;
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

    float getVelocity(UInt16 low, UInt16 hi)
    {
        // Merging two arrays 
        // https://stackoverflow.com/questions/59217/merging-two-arrays-in-net
        
        byte[] lowByte = BitConverter.GetBytes(low);
        byte[] hiByte = BitConverter.GetBytes(hi);
        byte[] combined = new byte[lowByte.Length + hiByte.Length];

        Array.Copy(hiByte, combined, hiByte.Length);
        Array.Copy(lowByte, 0, combined, hiByte.Length, lowByte.Length);
        print("low == " + low + " high == " + hi + " combined == " + combined);

        float velocity = BitConverter.ToInt16(combined,0) * rpmToRads;
        print("velocity == " + velocity);
        return velocity;
    }

    void parsePacket()
    {
        if(validate())
        {
            int length = packetList[3] / 4;
            for(int i = 1; i < length + 1; i++)
            {
                float direction = packetList[4*i + 3];
                float velocity = getVelocity(packetList[4*i + 1],packetList[4*i + 2]);
                rotationArray[packetList[4*i]] = new Tuple<float, float>(direction,velocity);
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
