using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // private Rigidbody rb = null;
    // private int x = 1;
    // Start is called before the first frame update
    void Start()
    {
        //rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update(){
        // if(Input.GetKeyDown(KeyCode.W)){
        //         Debug.Log("got inside");
        //         x = 0;
        //     }
        //     Debug.Log(x);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // if(x == 1){
        //     rb.AddTorque(0,5000,0);
        //     Debug.Log("added torque");
        // }
        // else{
        //     rb.angularVelocity = Vector3.zero;
        // }
        // Debug.Log(rb.angularVelocity);
    }
    
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision");
    }
    
}
