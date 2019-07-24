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

    // Used to stop threads 
    private bool exitTX;
    private bool exitRX;

    public Parser packetParser = null;    

    private byte[] outgoing = null;

    #endregion

    #region Unity API

    /*
        @brief: function runs upon startup 
    */
    void Awake()
    {

        // Initialize udp connection and seperate thread 
        clientRX = new UdpClient(portRX);
        endpointRX = new IPEndPoint(local,portRX);
        threadRX = new Thread(Recieve);

        clientTX = new UdpClient();
        endpointTX = new IPEndPoint(local,portTX);
        threadTX = new Thread(Send);

        exitTX = false;
        exitRX = false;
        threadRX.Start();
        threadTX.Start();
    }

    void Update()
    {
        outgoing = packetParser.outgoing;

        // if(scene != 0)
        // {
        //     updateScene();
        // }

        // if(global.timerTrigger != 255)
        // {
        //     Send(timer: global.timerTrigger);
        //     global.timerTrigger = 255;
        // }
    }

    /*
        @brief: kills thread when the game object is destroyed 
    */
    void OnDestroy()
    {
        exitRX = true;
        exitTX = true;
        clientRX.Close();
        clientTX.Close();
    }

    #endregion

    #region UDPTX

    void Send()
    {
        while(!exitTX)
        {
            try
            {
                if(outgoing != null)
                {
                    clientTX.Send(outgoing, outgoing.Length,endpointTX);
                    packetParser.outgoing = null;
                }

            }
            catch (Exception err)
            {
                print(err.ToString());            
            }
        }
        // try
        // {
        //     byte[] packet = new byte[6];
        //     packet[0] = 255;
        //     packet[1] = 255;
        //     packet[2] = acknowledge;
        //     packet[3] = scene;
        //     packet[4] = timer;
        //     packet[5] = calcCheckSum(ref packet,2,5);

        //     clientTX.Send(packet,packet.Length,endpointTX);
        // }
        // catch (Exception ex)
        // {
        //     print(ex.ToString());
        // }
    }

    #endregion

    #region UDPRX
    /*
        @brief: recieves and stores incoming packets from brachIOplexus  
    */
    void Recieve()
    {
        while(!exitRX)
        {
            try
            {
                // https://stackoverflow.com/questions/5932204/c-sharp-udp-listener-un-blocking-or-prevent-revceiving-from-being-stuck
                if(clientRX.Available > 0)
                {
                    byte[] packet = clientRX.Receive(ref endpointRX); 
                    packetParser.parsePacket(ref packet);
                }
            }
            catch (Exception err)
            {
                print(err.ToString());            
            }
        }
    }


    /*
        @brief: using the packet recieved from BrachIOplexus, fill the 
        rotationArray array with velocity and direction values. rotationArray
        is globally accessible to the rotation classes. 
    */
    // void parsePacket(ref byte[] packet)
    // {
    //     clearRotationArray();
    //     if(validate(ref packet, 4, (byte)(packet.Length - 1)))
    //     {
    //         if(packet[2] == 0)
    //         {
    //             int length = packet[3] / 4;
    //             for(byte i = 1; i < length + 1; i++)
    //             {
    //                 float direction = packet[4*i + 3];
    //                 float velocity = getVelocity(packet[4*i + 1],packet[4*i + 2]);
    //                 global.brachIOplexusControl[packet[4*i] + 1] = new Tuple<float, float>(direction,velocity);

    //             }
    //         }
    //         else
    //         {
    //             if(packet[10] == 1)
    //             {
    //                 global.controlToggle = true;
    //                 Send(acknowledge: 1);
    //             }

    //             if(packet[3] == 1)
    //             {
    //                 clearRotationArray();
    //             }

    //             global.controlToggle = Convert.ToBoolean(packet[9]);  
                
    //             scene = packet[4]; 

    //             for(int i = 5; i < packet.Length - 3; i++)
    //             {
    //                 global.cameraArray[i - 5] = packet[i];
    //             }
    //         }
    //     }
    // }

    #endregion
}
