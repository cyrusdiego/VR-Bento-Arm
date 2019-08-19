/* 
    BLINC LAB VIPER Project 
    GameTask_Global.cs
    Created by: Cyrus Diego August 12, 2019
 */
using UnityEngine;

[CreateAssetMenu(fileName = "GameTask_Global", menuName = "brachIOplexus/GameTask_Global")]
public class GameTask_Global : ScriptableObject
{
    // Cube on top of blue cup
    public bool step1;
    // Red cup on top of cube
    public bool step2;
}