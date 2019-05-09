using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    // init of variables 
    public Transform cameraTransform = null;

    private int cameraSpeed = 500;
    private float cameraRotationSpeed = 1.0f;
    private float minTime = 0.01f;

    // variables for key press times 
    // storing each seperate key press time allows for simultaneous movement in all axes 
    private float yKeyPressTime, xKeyPressTime, zKeyPressTime, wKeyPressTime, 
        aKeyPressTime, sKeyPressTime, dKeyPressTime, upKeyPressTime, 
        downKeyPressTime, leftKeyPressTime, rightKeyPressTime;
    
    // bools to determine what camera rotation is happening 
    private bool xbool, ybool, zbool;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform.position = new Vector3(-185, 125, -605); // arbitrary, this vector gives side profile of arm 
    }

    // move forward / back (along z axis) 
    void forwardback()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            wKeyPressTime = Time.timeSinceLevelLoad;
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (Time.timeSinceLevelLoad - wKeyPressTime > minTime)
            {
                cameraTransform.Translate(new Vector3(0, 0, cameraSpeed * Time.deltaTime));
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            sKeyPressTime = Time.timeSinceLevelLoad;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (Time.timeSinceLevelLoad - sKeyPressTime > minTime)
            {
                cameraTransform.Translate(new Vector3(0, 0, -cameraSpeed * Time.deltaTime));
            }
        }
    }

    // move left / right (along x axis)
    void leftright()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            aKeyPressTime = Time.timeSinceLevelLoad;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (Time.timeSinceLevelLoad - aKeyPressTime > minTime)
            {
                cameraTransform.Translate(new Vector3(-cameraSpeed * Time.deltaTime, 0, 0));
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            dKeyPressTime = Time.timeSinceLevelLoad;

        }
        if (Input.GetKey(KeyCode.D))
        {
            if (Time.timeSinceLevelLoad - dKeyPressTime > minTime)
            {
                cameraTransform.Translate(new Vector3(cameraSpeed * Time.deltaTime, 0, 0));
            }
        }
    }

    // move up and down (along y axis)
    void updown()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            upKeyPressTime = Time.timeSinceLevelLoad;

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Time.timeSinceLevelLoad - upKeyPressTime > minTime)
            {
                cameraTransform.Translate(new Vector3(0, cameraSpeed * Time.deltaTime, 0));
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            downKeyPressTime = Time.timeSinceLevelLoad;

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Time.timeSinceLevelLoad - downKeyPressTime > minTime)
            {
                cameraTransform.Translate(new Vector3(0, -cameraSpeed * Time.deltaTime, 0));
            }
        }
    }

    // rotate about y axis 
    void rotateY()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightKeyPressTime = Time.timeSinceLevelLoad;

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Time.timeSinceLevelLoad - rightKeyPressTime > minTime)
            {
                cameraTransform.Rotate(0, cameraRotationSpeed, 0, Space.Self);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftKeyPressTime = Time.timeSinceLevelLoad;

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Time.timeSinceLevelLoad - leftKeyPressTime > minTime)
            {
                cameraTransform.Rotate(0, -cameraRotationSpeed, 0, Space.Self);
            }
        }
    }

    // rotate about x axis 
    void rotateX()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightKeyPressTime = Time.timeSinceLevelLoad;

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Time.timeSinceLevelLoad - rightKeyPressTime > minTime)
            {
                cameraTransform.Rotate(cameraRotationSpeed, 0, 0, Space.Self);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftKeyPressTime = Time.timeSinceLevelLoad;

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Time.timeSinceLevelLoad - leftKeyPressTime > minTime)
            {
                cameraTransform.Rotate(-cameraRotationSpeed, 0, 0, Space.Self);
            }
        }
    }

    // rotate about z axis 
    void rotateZ()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightKeyPressTime = Time.timeSinceLevelLoad;

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Time.timeSinceLevelLoad - rightKeyPressTime > minTime)
            {
                cameraTransform.Rotate(0, 0, -cameraRotationSpeed, Space.Self);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftKeyPressTime = Time.timeSinceLevelLoad;

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Time.timeSinceLevelLoad - leftKeyPressTime > minTime)
            {
                cameraTransform.Rotate(0, 0, cameraRotationSpeed, Space.Self);
            }
        }


    }

    // Checks if any of XYZ buttons are held down to rotate the camera with respect to XYZ axis 
    private void XYZbuttondown()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            zKeyPressTime = Time.timeSinceLevelLoad;
        }
        if (Input.GetKey(KeyCode.Z))  // is "Z" pressed?
        {
            if (Time.timeSinceLevelLoad - zKeyPressTime > minTime)
            {
                zbool = true;
            }
        } else // if not, reset bool
        {
            zbool = false;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            xKeyPressTime = Time.timeSinceLevelLoad;
        }
        if (Input.GetKey(KeyCode.X))
        {
            if (Time.timeSinceLevelLoad - xKeyPressTime > minTime)
            {
                xbool = true;
            }
        } else
        {
            xbool = false;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            yKeyPressTime = Time.timeSinceLevelLoad;
        }
        if (Input.GetKey(KeyCode.Y))
        {
            if (Time.timeSinceLevelLoad - yKeyPressTime > minTime)
            {
                ybool = true;
            }
        }
        else
        {
            ybool = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        forwardback();
        leftright();
        updown();
        XYZbuttondown();

        if (ybool)
        {
            rotateY();
        }
        if(xbool)
        {
            rotateX();
        }
        if(zbool)
        {
            rotateZ();
        }
    }
}
