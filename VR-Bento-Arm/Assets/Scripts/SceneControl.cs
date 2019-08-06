/* 
    BLINC LAB VIPER Project 
    ScenControl.cs
    Created by: Cyrus Diego July 24, 2019

    Based on global values and control signals from BrachIOplexus, this class
    can end, pause, or reset the current scene 
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public Global global = null;

    void FixedUpdate()
    {
        if(global.pause)
        {
            // pauses game
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if(global.end)
        {
            // ends task and goes back to start screen
            SceneManager.LoadScene(0);
            global.task = false;
            global.end = false;
        }

        if(global.reset)
        {
            // resets the scene 
            int index;

            index = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(index);
            global.reset = false;
        }
    }
}
