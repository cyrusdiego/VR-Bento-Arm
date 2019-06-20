/* 
    BLINC LAB VIPER Project 
    UDPConection.cs 
    Created by: Cyrus Diego June 17, 2019 

    Connects with BrachIOplexus through UDP connection 
 */
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UDPConnection : MonoBehaviour
{
    // Network information 
    public Int32 port = 30000;
    public IPAddress local = IPAddress.Parse("127.0.0.1");

    // .NET classes for multi-threading and establishing a udp connection 
    private UdpClient client;
    private Thread recievingThread;
    private IPEndPoint endpoint;

    public GameObject[] gameObjects = new GameObject[2];
    private byte[] packet = new byte[30];

    void Start()
    {
        print("Initializing udp connection with brachIOplexus...");

        // Initialize udp connection and seperate thread 
        client = new UdpClient(port);
        endpoint = new IPEndPoint(local,port);
        recievingThread = new Thread(recieve);

        print("Starting seperate thread...");
        recievingThread.Start();
    }


    void FixedUpdate()
    {
        // combine low and high bytes https://stackoverflow.com/questions/29745111/combining-2-bytes-highbyte-lowbyte-to-a-signed-int-in-c-sharp
        // Little endian 
        byte[] velocityPkg = new byte[2];
        velocityPkg[0] = packet[2];
        velocityPkg[1] = packet[3];
        float velocity = BitConverter.ToInt16(velocityPkg,0);
        velocity *= 0.11f * Mathf.PI / 30;
        float[] pkg = new float[2];
        pkg[0] = packet[0];
        pkg[1] = velocity;
        gameObjects[0].SendMessage("recieveInput", pkg);
    }

    /*
        @brief: recieves and stores incoming packets from brachIOplexus  
    */
    void recieve()
    {
        print("Waiting for data...");
        while(true)
        {
            try
            {
                packet = client.Receive(ref endpoint); 
            }
            catch (Exception err)
            {
                print(err.ToString());            
            }
        }
    }
}
