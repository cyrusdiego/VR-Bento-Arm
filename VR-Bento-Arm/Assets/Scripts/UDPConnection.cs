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

    public GameObject[] gameObjects = new GameObject[1];
    private Dictionary<string,float> rotation = new Dictionary<string, float>();
    // Start is called before the first frame update
    void Start()
    {
        print("Initializing udp connection with brachIOplexus!!!");
        client = new UdpClient(port);
        endpoint = new IPEndPoint(local,port);
        recievingThread = new Thread(recieve);

        print("Starting seperate thread...");
        recievingThread.Start();
        rotation["W"] = 0;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        gameObjects[0].SendMessage("recieveInput",rotation["W"]);
    }

    void recieve()
    {
        print("Receiving Data...");
        while(true)
        {
            try
            {
                byte[] data = client.Receive(ref endpoint);

                string text = Encoding.UTF8.GetString(data);
                   
                if(text == "W")
                {
                    rotation["W"] = 1;
                }
                if(text == "stop")
                {
                    rotation["W"] = 0;
                }
                if(text == "S")
                {
                    rotation["W"] = -1;
                }
            }
            catch (Exception err)
            {
                print(err.ToString());            
            }
        }
    }
}
