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
    }

    private void rotateShoulder()
    {
        robotRigidBody[mode].AddTorque(0, sliderValue * turnRate, 0);
        Debug.Log(robotRigidBody[mode]);
        Debug.Log(robotRigidBody[mode].angularVelocity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        switch (modeitr % 5)
        {
            case 0:
                break;
            case 1:
                rotateShoulder();
                //robotTransforms[mode].Rotate(0, Mathf.Floor(sliderValue * turnRate), 0, Space.Self);
                break;
            //case 2:
            //    robotTransforms[mode].Rotate(0, 0, Mathf.Floor(sliderValue * turnRate), Space.Self);
            //    break;
            //case 3:
            //    robotTransforms[mode].Rotate(Mathf.Floor(sliderValue * turnRate), 0, 0, Space.Self);
            //    break;
            //case 4:
            //    robotTransforms[mode].Rotate(0, 0, Mathf.Floor(sliderValue * turnRate), Space.Self);
            //    break;

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

        if (GUI.Button(new Rect(10, 50, 150, 20), mode))
        {
            mode = rigidBodyNames[modeitr++ % 5];
        }

        sliderValue = GUI.HorizontalSlider(new Rect(10, 75, 100, 30), sliderValue, -10.0f, 10.0f);

        if (Input.GetMouseButtonUp(0)) // the "0" is refering to a button mapping
        {
            sliderValue = 0;
        }
    }
}
