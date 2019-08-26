/* 
    BLINC LAB VIPER Project 
    Persistent.cs 
    Created by: Cyrus Diego August 7, 2019 

    Attached to a parent game object that hold multiple objects that provide functionality 
    such as an active UDP connection throughout the lifespan of the simulation 
 */
using UnityEngine;

public class Persistent : MonoBehaviour
{
    /*
        @brief: function called when script instance is being loaded
    */
    void Awake()
    {
        // https://answers.unity.com/questions/233284/objects-being-duplicated-with-dontdestroyonload.html
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
