using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    public Transform cameraTransform = null;
    private int cameraSpeed = 100;
    private float wKeyPressTime;
    private float minTime = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform.position = new Vector3(-185, 125, -605); // arbitrary, this vector gives side profile of arm 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            wKeyPressTime = Time.timeSinceLevelLoad;
   
        }
        if (Input.GetKey(KeyCode.W))
        {
            if(Time.timeSinceLevelLoad - wKeyPressTime > minTime)
            {
                cameraTransform.Translate(new Vector3(0, 0, cameraSpeed * Time.deltaTime));

            }
        }
    }
}
