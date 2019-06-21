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
using System.Diagnostics; // process
using UnityEngine;
using UnityEditor;

public class UDPConnection : MonoBehaviour
{
    // Singleoton pattern
    public static UDPConnection udp;

    // Holds direction and velocity
    public Tuple<float,float> shoulderRotation;
    public Tuple<float,float> elbowRotation;
    public Tuple<float,float> wristRotation;
    public Tuple<float,float> wristFlexion;
    public Tuple<float,float> handRotation;

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

        // Initialize rotation values 
        shoulderRotation = new Tuple<float,float>(0,0);
        elbowRotation = new Tuple<float,float>(0,0);
        wristRotation = new Tuple<float,float>(0,0);
        wristFlexion = new Tuple<float,float>(0,0);
        handRotation = new Tuple<float,float>(0,0);

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
    void Unpack()
    {
        // Get shoulder velocity 
        byte[] velocityPkg = new byte[2];
        velocityPkg[0] = packet[2];  // extract specific bytes from packet
        velocityPkg[1] = packet[3];

        // convert to float and rad/s 
        float velocity = BitConverter.ToInt16(velocityPkg,0);
        velocity *= rpmToRads;

        // Sets shoulder rotation values 
        elbowRotation = new Tuple<float, float>(packet[0],velocity);
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
                // Unpack();
                for(int i = 0; i < packet.Length; i++){
                    print("packet[" + i + "] == " + packet[i]);
                }
            }
            catch (Exception err)
            {
                print(err.ToString());            
            }
        }
    }
}
