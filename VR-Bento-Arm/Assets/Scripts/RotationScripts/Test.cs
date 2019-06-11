using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private bool isTiming = false; 
    private float timer = 0.0f;
    private Rigidbody rb;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if((Input.GetMouseButtonDown(0))){
            rb.isKinematic = false;
            rb.useGravity = true;
            isTiming = true;
        }
        if(isTiming){
            timer+=Time.deltaTime;
        }
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        isTiming = false;
        print(timer);
    }
}
