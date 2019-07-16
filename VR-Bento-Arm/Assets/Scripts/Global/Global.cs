using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Global", menuName = "brachIOplexus/GlobalVariables")]
public class Global : ScriptableObject
{
    public Tuple<float,float>[] brachIOplexusControl = new Tuple<float, float>[6]; 
    public float[] SteamVRControl = new float[6];
    public bool controlToggle = false;
    public byte timerTrigger = 255;
    public byte[] cameraArray = new byte[4];
}