using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperArmRotate : MonoBehaviour
{
    public float robotUpperArmSliderValue = 0.0f;
    public Transform RobotUpperArm = null;
    public float upperArmTurnRate = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!(RobotUpperArm.eulerAngles.y >= 90 && RobotUpperArm.eulerAngles.y < 270))
        {
            RobotUpperArm.Rotate(0, robotUpperArmSliderValue * upperArmTurnRate, 0, Space.Self);

        }
        else if(RobotUpperArm.eulerAngles.y > 90 && RobotUpperArm.eulerAngles.y <= 180)
        {
            RobotUpperArm.eulerAngles = new Vector3(0, 90, 0);
        }
        else if(RobotUpperArm.eulerAngles.y <= 270 && RobotUpperArm.eulerAngles.y > 180)
        {
            RobotUpperArm.eulerAngles = new Vector3(0, 270, 0);
        }

        Debug.Log(RobotUpperArm.eulerAngles.y);



        if (Input.GetMouseButtonUp(0)) // the "0" is refering to a button mapping
        {
            //resets the sliders back to 0 when you lift up on the mouse click down.
            robotUpperArmSliderValue = 0;
        }
    }
    private void OnGUI()
    {
        //creates the slider and sets it 25 pixels in x, 80 in y, 100 wide and 30 tall.
        robotUpperArmSliderValue = GUI.HorizontalSlider(new Rect(25, 80, 100, 30), robotUpperArmSliderValue, -10.0f, 10.0f);
    }
}
