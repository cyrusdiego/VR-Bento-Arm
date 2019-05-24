using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeArm : MonoBehaviour
{
    public Transform ForeArmShellTransform = null;
    public GameObject Rotations = null;
    private Tuple<string,bool> msg;
    private bool collided = false;
    private Vector3 prevAngles; 


    void Start()
    {
        // gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.AddComponent<Rigidbody>();
        // gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.AddComponent<Rigidbody>();

        // gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Rigidbody>().useGravity = false;
        // gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Rigidbody>().useGravity = false;

        // gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<BoxCollider>().isTrigger = false;
        // gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<BoxCollider>().isTrigger = false;

    }
    // Update is called once per frame
    void FixedUpdate() 
    {
        gameObject.transform.position = ForeArmShellTransform.position;
        gameObject.transform.eulerAngles = ForeArmShellTransform.eulerAngles;

        // if(collided)
        // {
        //     RaycastHit hitInfo; 
        //     if (Physics.Raycast(contactPoint.point, contactPoint.normal, out hitInfo, 10, layerMask.value))
        //     {
        //         msg = new Tuple<string, bool>("Elbow", true);
        //         Rotations.SendMessage("CollisionDetection",msg);
        //     }
        // }

    }

    void OnTriggerEnter(Collider other)
    {
        // if(collided)
        // {
        //     prevAngles = gameObject.transform.eulerAngles;
        //     Debug.Log("prevangle.z == " + prevAngles.z);
        //     Rotations.SendMessage("AngularRestriction", prevAngles);
        //     collided = false;
        // } 
        // else 
        // {
        //     collided = true; 
        //     msg = new Tuple<string, bool>("Elbow", true);
        //     Rotations.SendMessage("CollisionDetection",msg);
        // }

        msg = new Tuple<string, bool>("Elbow", true);
        Rotations.SendMessage("CollisionDetection",msg);
        
    }

    void OnTriggerExit(Collider other)
    {
        msg = new Tuple<string, bool>("Elbow", false);
        Rotations.SendMessage("CollisionDetection", msg);
    }

    void OnCollisionEnter(Collision collision) 
    {
        // Debug.Log("got inside collisoin enter");
        // contactPoint = collision.contacts[0];
        
        // gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<BoxCollider>().isTrigger = true;
        // gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<BoxCollider>().isTrigger = true;

        // Destroy(gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.AddComponent<Rigidbody>());
        // Destroy(gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.AddComponent<Rigidbody>());

        // collided = true;

        msg = new Tuple<string, bool>("Elbow", true);
        Rotations.SendMessage("CollisionDetection",msg);
    }

    void OnCollisionExit(Collision collision) 
    {
        msg = new Tuple<string, bool>("Elbow", false);
        Rotations.SendMessage("CollisionDetection", msg);
    }
}
