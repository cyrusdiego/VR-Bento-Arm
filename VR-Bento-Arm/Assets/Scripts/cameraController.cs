/* 
    BLINC LAB VIPER Project 
    cameraPosition.cs 
    Created by: Cyrus Diego July 2, 2019

    Class to store camera positions and set the camera object to the 
    specified location
 */


using UnityEngine;
using System.Collections.Generic;

public class cameraController : MonoBehaviour
{
    private List<Vector3> positions = new List<Vector3>();
    private int positionItr = 0;
    public Transform headset = null;

    void Update()
    {
        for(int i = 0; i < 4; i++)
        {
            byte val = UDPConnection.udp.cameraArray[i];
            // Test this to see if it will catch the default
            if(val != 0 && val != 255)
            {
                switch(i)
                {
                    case 0:
                        save();
                        break;
                    case 1:
                        next();
                        break;
                    case 2:
                        clear();
                        break;
                    case 3:
                        delete(val);
                        break;
                }
            }
        }
    }

    private void save()
    {
        positions.Add(headset.position);
        UDPConnection.udp.cameraArray[0] = 0;
        positionItr = positions.Count - 1;
    }
    private void next()
    {
        positionItr = ((++positionItr) % positions.Count);
        headset.position = positions[positionItr];
        UDPConnection.udp.cameraArray[1] = 0;

    }
    private void clear()
    {
        positions.Clear();
        positionItr = 0;
        UDPConnection.udp.cameraArray[2] = 0;
    }
    private void delete(byte index)
    {
        positions.RemoveAt(index);
        UDPConnection.udp.cameraArray[4] = 255;
    }
}
