using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class test : MonoBehaviour
{
 
    Thread thread; 

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        thread = new Thread(printing);
        print("starting thread");
        thread.Start();
        print("started thread");
    }
    void printing(){
        while(true)
        print("inside thread");
    }
}
