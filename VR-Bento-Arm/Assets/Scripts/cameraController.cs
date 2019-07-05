/* 
    BLINC LAB VIPER Project 
    cameraPosition.cs 
    Created by: Cyrus Diego July 2, 2019

    Class to store camera positions and set the camera object to the 
    specified location
 */


using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public class cameraController : MonoBehaviour
{
    private List<Vector3> positions = new List<Vector3>();
    private int positionItr = 0;
    private string jsonStoragePath = @"C:\Users\Trillian\Documents\VR-Bento-Arm\brachIOplexus\Example1\resources\unityCameraPositions";
    public Transform headset = null;

    void Awake()
    {
        loadCameraPositions();
    }

    void Update()
    {
        for(int i = 0; i < 4; i++)
        {
            byte val = UDPConnection.udp.cameraArray[i];
            // Test this to see if it will catch the default
            if(val != 255)
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
                    print("ordered to load camera positons from the temp workspace");
                    
                        loadCameraPositions();
                        break;
                }
            }
        }
    }

    private void save()
    {
        positions.Add(headset.position);
        UDPConnection.udp.cameraArray[0] = 255;
        positionItr = positions.Count - 1;
        saveToJson();
    }

    private void next()
    {
        positionItr = ((++positionItr) % positions.Count);
        headset.position = positions[positionItr];
        UDPConnection.udp.cameraArray[1] = 255;

    }

    private void clear()
    {
        positions.Clear();
        positionItr = 0;
        UDPConnection.udp.cameraArray[2] = 255;
        deleteJson();
    }

    private void loadCameraPositions()
    {
        print("loading contents");
        string[] contents = Directory.GetFiles(jsonStoragePath);
        print("size of contents: " + contents.Length);
        for(int i = 0; i < contents.Length; i++)
        {
            string fileName = contents[i];
            string filePath = Path.Combine(jsonStoragePath,fileName);
            using(StreamReader reader = new StreamReader(filePath))
            {
                string jsonContents = reader.ReadToEnd();
                cameraData data = JsonUtility.FromJson<cameraData>(jsonContents);
                Vector3 positionData = new Vector3(data.x, data.y, data.z);
                positions.Add(positionData);
            }
        }
        if(positions.Count > 0)
        {
            headset.position = positions[0];
        }
        if(UDPConnection.udp.cameraArray[3] != 255)
        {
            UDPConnection.udp.cameraArray[3] = 255;
        }
        print("size of positions: " + positions.Count);
    }

    private void saveToJson()
    {
        string fileName = $"Position{positions.Count - 1}";
        string filePath = Path.Combine(jsonStoragePath,fileName);
        Vector3 position = headset.position;
        cameraData data = new cameraData();
        data.x = position.x;
        data.y = position.y;
        data.z = position.z;
        string jsonCameraData = JsonUtility.ToJson(data);
        if(!File.Exists(filePath))
        {
            File.WriteAllText(filePath,jsonCameraData);
        }
    }

    private void deleteJson()
    {
        DirectoryInfo di = new DirectoryInfo(jsonStoragePath);

        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete(); 
        }
    }
}

[Serializable]
public class cameraData
{
    public float x;
    public float y;
    public float z;

}
