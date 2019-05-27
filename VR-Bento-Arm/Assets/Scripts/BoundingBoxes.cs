using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBoxes : MonoBehaviour
{
    public GameObject[] armBoxes = new GameObject[3];
    public GameObject[] armShells = new GameObject[6];
    private int mode = 0, counter = 0;

    void Start()
    {
        foreach (GameObject armbox in armBoxes)
        {
            Component[] components = armbox.GetComponents(typeof(BoxCollider));
            foreach (BoxCollider box in components)
            {
                box.enabled = false;
            }
            
        }
    }

    void Update()
    {
        CheckKeyPress();
    }

    private void CheckKeyPress()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            mode++;
            if(mode % 2 == 0)
            {
                foreach (GameObject armbox in armBoxes)
                {
                    Component[] components = armbox.GetComponents(typeof(BoxCollider));
                    foreach (BoxCollider box in components)
                    {
                        box.enabled = !box.enabled;
                    }
                }
            }
            else 
            {
                foreach (GameObject armshell in armShells)
                {
                    armshell.SetActive(!armshell.activeSelf);
                }
            }
        }
    }       

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 25, 100, 20), "Hide")) 
        {
            foreach (GameObject armshell in armShells) 
            {
                armshell.SetActive(!armshell.activeSelf);
            }
        }
    } 
}
