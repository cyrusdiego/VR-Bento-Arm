using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    private Dictionary<string, Rigidbody> robotRigidBody = new Dictionary<string, Rigidbody>();
    public GameObject[] shells = new GameObject[3];
    public Rigidbody[] rigidBodies = new Rigidbody[5];
    private string[] rigidBodyNames = { "Shoulder", "Elbow", "Forearm Rotation", "Wrist Flexion", "Open Hand" };
    public float turnRate = 0.2f;
    private float sliderValue = 0.0f;
    private string mode;
    private int modeitr = 0;

    // Start is called before the first frame update
    void Start()
    {
        mode = rigidBodyNames[modeitr++];
        
        int i = 0;
        foreach (Rigidbody rigidbody in rigidBodies)
        {
            robotRigidBody.Add(rigidBodyNames[i], rigidbody);
            i++;
        }
        setKinematic();
    }

    private void setAngularVelocity()
    {
        foreach (string name in rigidBodyNames)
        {
            robotRigidBody[name].angularVelocity = Vector3.zero;
        }

    }
    private void setKinematic()
    {
        for(int i = modeitr % 5; i < 5; i++)
        {
            robotRigidBody[rigidBodyNames[i]].isKinematic = true;
        }
        robotRigidBody[mode].isKinematic = false;
    }

    private void rotateX()
    {
        robotRigidBody[mode].AddRelativeTorque(sliderValue * turnRate, 0, 0);
    }
    private void rotateY()
    {
        robotRigidBody[mode].AddRelativeTorque(0, sliderValue * turnRate, 0);
    }
    private void rotateZ()
    {
        robotRigidBody[mode].AddRelativeTorque(0, 0, sliderValue * turnRate);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
        switch (modeitr % 5)
        {
            case 0:
                rotateY();
                break;
            case 1:
                rotateY();
                break;
            case 2:
                rotateZ();
                break;
            case 3:
                rotateX();
                break;
            case 4:
                rotateZ();
                break;

        }
    }

    

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 25, 100, 20), "Shells"))
        {
            foreach (GameObject shell in shells)
            {
                shell.SetActive(!shell.activeSelf);
            }
        }
        sliderValue = GUI.HorizontalSlider(new Rect(10, 75, 100, 30), sliderValue, -10.0f, 10.0f);

        if (Input.GetMouseButtonUp(0)) // the "0" is refering to a button mapping
        {
            sliderValue = 0;

        }
        if (GUI.Button(new Rect(10, 50, 150, 20), mode))
        {
            
            mode = rigidBodyNames[modeitr++ % 5];
            setKinematic();
            setAngularVelocity();
        }


    }
}
