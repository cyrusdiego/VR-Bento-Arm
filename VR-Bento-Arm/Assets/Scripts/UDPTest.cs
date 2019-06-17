using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UDPTest : MonoBehaviour
{
    private Int32 port = 30000;
    private IPAddress local = IPAddress.Parse("127.0.0.1");
    UdpClient client;
    Thread recievingThread;
    IPEndPoint endpoint;
    // Start is called before the first frame update
    void Start()
    {
        print("initializing udp client");
        client = new UdpClient(port);
        endpoint = new IPEndPoint(local,port);
        recievingThread = new Thread(recieve);

        print("starting seperate thread");
        recievingThread.Start();
    }

    void recieve()
    {
        print("Receiving Data");
        while(true)
        {
            try
            {
                byte[] data = client.Receive(ref endpoint);

                string text = Encoding.UTF8.GetString(data);

                print(">>"+text);
            }
            catch (Exception err)
            {
                print(err.ToString());            
            }
        }
    }
}
