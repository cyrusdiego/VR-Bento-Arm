using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UDPRecieve : MonoBehaviour
{
    Thread receiveThread;
    UdpClient client;

    public int port; 

    public string lastReceivedUDPPacket="";
    public string allReceivedUDPPackets="";

    // Start is called before the first frame update
    void Start()
    {
        print("UDPSend.init()");

        port = 904;

        print("Sending to 127.0.0.1 : "+port);
        print("Test-Sending to this port");

        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void ReceiveData()
    {
        print("Receiving Data");
        client = new UdpClient(port);
        while(true)
        {
            try
            {
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 904);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
