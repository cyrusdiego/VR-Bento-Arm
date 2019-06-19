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
    public Int32 port = 30000;
    private IPAddress local = IPAddress.Parse("127.0.0.1");

    UdpClient client;
    Thread recievingThread;
    IPEndPoint endpoint;

    public GameObject[] gameObjects = new GameObject[2];
    private Dictionary<string,float> rotation = new Dictionary<string, float>();
    private byte[] packet = new byte[38];
    // Start is called before the first frame update
    void Start()
    {
        print("Initializing udp connection with brachIOplexus!!!");
        client = new UdpClient(port);
        endpoint = new IPEndPoint(local,port);
        recievingThread = new Thread(recieve);

        print("Starting seperate thread...");
        recievingThread.Start();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if(packet[0] == 0 && packet[1] == 0){
            gameObjects[0].SendMessage("recieveInput", packet[0]);
            return;
        }
        if(packet[0] == 1){
            gameObjects[0].SendMessage("recieveInput", 1);
            return;
        }
        if(packet[1] == 1){
            gameObjects[0].SendMessage("recieveInput", -1);
            return;
        }

        

    }

    void recieve()
    {
        print("Receiving Data...");
        while(true)
        {
            try
            {
                byte[] data = client.Receive(ref endpoint);
                packet = data; 
            }
            catch (Exception err)
            {
                print(err.ToString());            
            }
        }
    }
}
