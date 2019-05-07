﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float robotShoulderSliderValue = 0.0f;
    public float robotElbowSliderValue = 0.0f;

    public Transform Shoulder = null;
    public Transform Elbow = null; 

    public float shoulderTurnRate = 0.2f;
    public float elbowTurnRate = 0.2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!(Shoulder.eulerAngles.y >= 90 && Shoulder.eulerAngles.y < 270))
        {
            Shoulder.Rotate(0, robotShoulderSliderValue * shoulderTurnRate, 0, Space.Self);

        }
        else if (Shoulder.eulerAngles.y > 90 && Shoulder.eulerAngles.y <= 180)
        {
            Shoulder.eulerAngles = new Vector3(0, 89, 0);  // buggy constrain just in this direction, sometimes stops
            Debug.Log(Shoulder.eulerAngles.y);

        }
        else if (Shoulder.eulerAngles.y <= 270 && Shoulder.eulerAngles.y > 180)
        {
            Shoulder.eulerAngles = new Vector3(0, 270, 0);
        }

        Elbow.Rotate(0, 0, robotElbowSliderValue * elbowTurnRate, Space.Self);

        if (Input.GetMouseButtonUp(0)) // the "0" is refering to a button mapping
        {
            //resets the sliders back to 0 when you lift up on the mouse click down.
            robotShoulderSliderValue = 0;
            robotElbowSliderValue = 0;
        }
    }
    private void OnGUI()
    {
        //creates the slider and sets it 25 pixels in x, 80 in y, 100 wide and 30 tall.
        robotShoulderSliderValue = GUI.HorizontalSlider(new Rect(25, 80, 100, 30), robotShoulderSliderValue, -10.0f, 10.0f);
        robotElbowSliderValue = GUI.HorizontalSlider(new Rect(25, 100, 100, 30), robotElbowSliderValue, -10.0f, 10.0f);

    }
}
