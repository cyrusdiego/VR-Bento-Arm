using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    private Dictionary<string, Transform> robotTransforms = new Dictionary<string, Transform>();
    public GameObject[] shells = new GameObject[3];
    public Transform[] Transforms = new Transform[5];
    private string[] transformNames = { "Shoulder", "Elbow", "Forearm Rotation", "Wrist Flexion", "Open Hand" };
    public float turnRate = 0.2f;
    private float sliderValue = 0.0f;
    private string mode;
    private int modeitr = 0;

    // Start is called before the first frame update
    void Start()
    {
        mode = transformNames[modeitr++];
        int i = 0;
        foreach (Transform transform in Transforms)
        {
            robotTransforms.Add(transformNames[i], transform);
            i++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        switch(modeitr % 5)
        {
            case 0:
                robotTransforms[mode].Rotate(0, Mathf.Floor(sliderValue * turnRate), 0, Space.Self); 
                break;
            case 1:
                robotTransforms[mode].Rotate(0, Mathf.Floor(sliderValue * turnRate), 0, Space.Self);
                break;
            case 2:
                robotTransforms[mode].Rotate(0, 0, Mathf.Floor(sliderValue * turnRate), Space.Self);
                break;
            case 3:
                robotTransforms[mode].Rotate(Mathf.Floor(sliderValue * turnRate), 0, 0, Space.Self);
                break;
            case 4:
                robotTransforms[mode].Rotate(0, 0, Mathf.Floor(sliderValue * turnRate), Space.Self);
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

        if (GUI.Button(new Rect(10, 50, 150, 20), mode))
        {
            mode = transformNames[modeitr++ % 5];
        }

        sliderValue = GUI.HorizontalSlider(new Rect(10, 75, 100, 30), sliderValue, -10.0f, 10.0f);

        if (Input.GetMouseButtonUp(0)) // the "0" is refering to a button mapping
        {
            sliderValue = 0;
        }
    }
}
