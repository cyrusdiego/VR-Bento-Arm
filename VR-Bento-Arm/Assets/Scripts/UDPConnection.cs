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

    private byte[] outgoing;
    private byte[] feedback;
    private bool task;

    private byte[] timerFeedback;
    private bool timer;

    #endregion

    #region Unity API

    /*
        @brief: function runs upon startup 
    */
    void Awake()
    {
        outgoing = null;
        feedback = null;
        task = false;
        timer = false;
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

    void FixedUpdate()
    {
        outgoing = packetParser.outgoing;
        feedback = packetParser.feedback;
        task = packetParser.task;
        timerFeedback = packetParser.timerFeedback;
        timer = packetParser.timer;
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
    void Send()
    {
        while(!exitTX)
        {
            try
            {
                if(outgoing != null)
                {
                    clientTX.Send(outgoing, outgoing.Length,endpointTX);
                    outgoing = null;
                    packetParser.outgoing = null;
                }
                // if(task)
                // {
                //     clientTX.Send(feedback, feedback.Length,endpointTX);
                // }
                if(timer)
                {
                    clientTX.Send(timerFeedback, timerFeedback.Length,endpointTX);
                    timer = false;
                    packetParser.timer = false;
                }


            }
            catch (Exception err)
            {
                print(err.ToString());            
            }
        }
    }

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
}
