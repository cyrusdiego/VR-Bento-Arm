using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Global", menuName = "brachIOplexus/GlobalVariables")]
public class Global : ScriptableObject
{
    public Tuple<float,float>[] brachIOplexusControl = new Tuple<float, float>[6]; 
    public float[] SteamVRControl = new float[6];
    public bool controlToggle = true;
    public byte timerTrigger = 255;
    public byte[] cameraArray = new byte[4];
    public bool armShell = true;

    // New stuff 
    public int[] loaderPacket = new int[4];
    public bool sent;
    public byte[] outgoing = null;
}