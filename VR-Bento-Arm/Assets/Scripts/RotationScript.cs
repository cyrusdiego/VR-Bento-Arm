using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float robotShoulderSliderValue = 0.0f;
    public float robotElbowSliderValue = 0.0f;
    public float robotWristSliderValue = 0.0f;

    public Transform Shoulder = null;
    public Transform Elbow = null;
    public Transform Wrist = null;

    public float shoulderTurnRate = -0.2f;
    public float elbowTurnRate = -0.2f;
    public float wristTurnRate = -0.2f;

    public GameObject[] shells = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
 
        if (!(Shoulder.eulerAngles.y > 90 && Shoulder.eulerAngles.y < 270))
        {
            Shoulder.Rotate(0, Mathf.Floor(robotShoulderSliderValue * shoulderTurnRate), 0, Space.Self);

        }
        else if (Shoulder.eulerAngles.y >= 90 && Shoulder.eulerAngles.y <= 180)
        {
            Shoulder.eulerAngles = new Vector3(0, 90, 0);  

        }
        else if (Shoulder.eulerAngles.y <= 270 && Shoulder.eulerAngles.y > 180)
        {
            Shoulder.eulerAngles = new Vector3(0, 270, 0);
        }
        

        Elbow.Rotate(0, 0, Mathf.Floor(robotElbowSliderValue * elbowTurnRate), Space.Self);

        Wrist.Rotate(0, 0, Mathf.Floor(robotWristSliderValue * wristTurnRate), Space.Self);

        if (Input.GetMouseButtonUp(0)) // the "0" is refering to a button mapping
        {
            //resets the sliders back to 0 when you lift up on the mouse click down.
            robotShoulderSliderValue = 0;
            robotElbowSliderValue = 0;
            robotWristSliderValue = 0;
     
        }
    }
    private void OnGUI()
    {
        //creates the slider and sets it 25 pixels in x, 80 in y, 100 wide and 30 tall.
        robotShoulderSliderValue = GUI.HorizontalSlider(new Rect(25, 80, 100, 30), robotShoulderSliderValue, -10.0f, 10.0f);
        robotElbowSliderValue = GUI.HorizontalSlider(new Rect(25, 120, 100, 30), robotElbowSliderValue, -10.0f, 10.0f);
        robotWristSliderValue = GUI.HorizontalSlider(new Rect(25, 160, 100, 30), robotWristSliderValue, -10.0f, 10.0f);
       
        // shows/hides arm shells in game 
        if(GUI.Button(new Rect(10, 10, 50, 50), "Shells"))
        {
            foreach(GameObject shell in shells)
            {
                shell.SetActive(!shell.activeSelf);
            }
        }

    }
}
