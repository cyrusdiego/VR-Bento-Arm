using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BentoControl", menuName = "brachIOplexus/BentoArm")]
public class BentoControl : ScriptableObject
{
    public Tuple<float,float>[] brachIOplexusControl = new Tuple<float, float>[6]; 
    public float[] SteamVRControl = new float[7];
    public bool controlToggle = false;


}