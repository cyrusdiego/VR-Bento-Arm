using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Global", menuName = "brachIOplexus/GlobalVariables")]
public class Global : ScriptableObject
{
    public float[] SteamVRControl = new float[6];
    public byte timerTrigger = 255;
    public byte[] cameraArray = new byte[4];

    // Array holds direction and velocity respectively for each motor (Motor.cs)
    public Tuple<float,float>[] brachIOplexusControl = new Tuple<float, float>[6]; 
    // Array holds information to load a specific scene / task (SceneLoader.cs)
    public int[] loaderPacket = new int[4];
    // Flag that ensures the acknowledgement packet from Unity has been sent to brachIOplexus (Parser.cs)
    public bool sent;
    // Acknowledgement packet to be sent from Unity to BrachIOplexus (Parser.cs)
    public byte[] outgoing = null;
    // Toggle between brachIOplecus vs VR Controllers to control arm (VRController.cs)
    public bool controlToggle;
    // Toggle to either show or hide arm shells (armShellController.cs)
    public bool armShell;
    // Toggle if there is a VR Headset connected 
    public bool VREnabled;
    // Toggle to pause scene (SceneControl.cs)
    public bool pause;
    // Toggle to end scene (SceneControl.cs)
    public bool end;
    // Toggle to reset scene (SceneControl.cs)
    public bool reset; 
    // Feedback array to send back position of arm (Feedback.cs)
    public float[] position;
    // Feedback array to send back velocity of arm (Feedback.cs)
    public float[] velocity; 
    // Number of motors in virtual bento arm (Feedback.cs)
    public int motorCount = 5;
    // Toggle if task has been loaded (Parser.cs)
    public bool task;
    // Toggle to start / stop timer in brachIOplexus
    public bool timer;
}