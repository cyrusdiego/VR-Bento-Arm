using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armShellController : MonoBehaviour
{
    public GameObject[] armShells = new GameObject[3];
    public GameObject[] armStructure = new GameObject[3];
    public Global global = null;

    // Start is called before the first frame update
    void Start()
    {
        global.armShell = true;    
    }

    // Update is called once per frame
    void Update()
    {
        if(global.armShell)
        {
            for(int i = 0; i < armShells.Length; i++)
            {
                armShells[i].SetActive(true);
            }
            for(int i = 0; i < armStructure.Length; i++)
            {
                BoxCollider[] boxes;
                boxes = armStructure[i].GetComponents<BoxCollider>();
                for(int j = 0; j < boxes.Length; j++)
                {
                    boxes[j].enabled = false;
                }
            }
        }
        else
        {
            for(int i = 0; i < armShells.Length; i++)
            {
                armShells[i].SetActive(false);
            }
            for(int i = 0; i < armStructure.Length; i++)
            {
                BoxCollider[] boxes;
                boxes = armStructure[i].GetComponents<BoxCollider>();
                for(int j = 0; j < boxes.Length; j++)
                {
                    boxes[j].enabled = true;
                }
            }
        }
    }
}
