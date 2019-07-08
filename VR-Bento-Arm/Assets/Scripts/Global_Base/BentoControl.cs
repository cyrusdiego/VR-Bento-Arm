using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BentoControl", menuName = "brachIOplexus/BentoArm")]
public class BentoControl : ScriptableObject
{
    public Tuple<float,float>[] rotationArray = new Tuple<float, float>[6]; 

}