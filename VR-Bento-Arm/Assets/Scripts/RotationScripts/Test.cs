using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Vector3 dir;
    void OnCollisionStay(Collision other)
    {
        Vector3 force = other.impulse / Time.deltaTime;
        dir = force;
        print(force);
    }
    void Update()
    {
        Debug.DrawRay(gameObject.transform.position, 1000 * Vector3.down,Color.yellow);
        Debug.DrawRay(gameObject.transform.position,1000*dir,Color.blue);
        Debug.DrawRay(gameObject.transform.position,-1000*dir,Color.blue);
        Debug.DrawRay(gameObject.transform.position,new Vector3(dir.x,0,0),Color.red);

    }
}
