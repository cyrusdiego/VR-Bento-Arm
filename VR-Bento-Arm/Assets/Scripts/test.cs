using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class test : MonoBehaviour
{

    void OnCollisionStay(Collision other)
    {
        Collision_force = other.impulse / Time.fixedDeltaTime;
    }
    Vector3 Collision_force;
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        print(Collision_force);
    }
}
