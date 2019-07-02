/* 
    BLINC LAB VIPER Project 
    cameraPosition.cs 
    Created by: Cyrus Diego July 2, 2019

    Class to store camera positions and set the camera object to the 
    specified location
 */


using System;
using UnityEngine;
using System.Collections.Generic;

public class cameraPositions : MonoBehaviour
{
    private List<Transform> positions;

    public cameraPositions()
    {
        positions = new List<Transform>();
    }

    public void save(Transform current)
    {
        positions.Add(current);
    }

    public void clear()
    {
        positions = new List<Transform>();
    }
}
