﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(9810,0,0);
    }
}
