using UnityEngine;
using Valve.VR;

public class ArmTracker : MonoBehaviour 
{
    const float dX = 0;
    const float dY = 0;
    const float dZ = -0.20f;
    const float roll = 0;
    const float yaw = 45;
    const float pitch = 0;

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
        print("printing delta displacement");
        print("dx: " + delta_displacement.x + " dy: " + delta_displacement.y + " dz: " + delta_displacement.z);
        print("printing delta rotation");
        print(delta_rotation);
        print(delta_rotation.w);
        //Get current Tracker pose
        Vector3 tracker_position = Tracker.transform.position;
        Quaternion tracker_rotation = Tracker.transform.rotation;
        //Transform current Tracker pose to Accessory pose
        capsule.GetComponent<Transform>().position = tracker_position + (tracker_rotation *
        delta_rotation) * delta_displacement;
        capsule.GetComponent<Transform>().rotation = tracker_rotation * delta_rotation;

    }
}
