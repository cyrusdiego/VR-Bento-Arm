using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private List<Collision> collisionObjs = new List<Collision>();
    private int count = 0;
    public GameObject hand = null;
    void Start()
    {

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(Collision collisionobj in collisionObjs)
        {
            if(collisionObjs.ToString() == "Left" || collisionObjs.ToString() == "Right")
            {
                count++;
            }
        }
        if(count % 2 == 0 && count != 0)
        {
            gameObject.transform.position = hand.transform.position;
            gameObject.transform.rotation = hand.transform.rotation;
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        if(!collisionObjs.Contains(other))
        {
            collisionObjs.Add(other);
        }
    }
    
}
