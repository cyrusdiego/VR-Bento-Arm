using UnityEngine;
using Valve.VR;

public class ArmTracker : MonoBehaviour 
{
    const float dX = 0.0100224f;
    const float dY = -0.07616526f;
    const float dZ = 0.4884118f;
    const float roll = 10.854305f;
    const float yaw = 91.8736f;
    const float pitch = 78.805113f;

    public GameObject capsule = null;
    public SteamVR_TrackedObject Tracker;

    void Awake()
    {
        Tracker = capsule.GetComponent<SteamVR_TrackedObject>();
    }

    void Update () 
    {
        //Collect delta rotation and displacement between Tracker and Accessory
        Vector3 delta_displacement = new Vector3(dX, dY, dZ);
        Quaternion delta_rotation = Quaternion.Euler(roll, yaw, pitch);
        //Get current Tracker pose
        Vector3 tracker_position = Tracker.transform.position;
        Quaternion tracker_rotation = Tracker.transform.rotation;
        //Transform current Tracker pose to Accessory pose
        capsule.GetComponent<Transform>().rotation = tracker_rotation * delta_rotation;
        capsule.GetComponent<Transform>().position = tracker_position + (tracker_rotation *
        delta_rotation) * delta_displacement;
    }
}
