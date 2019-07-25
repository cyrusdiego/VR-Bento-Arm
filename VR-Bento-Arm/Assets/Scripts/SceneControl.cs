using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public Global global = null;

    void Update()
    {
        if(global.pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if(global.end)
        {
            SceneManager.LoadScene(0);
            global.task = false;
            global.end = false;
        }

        if(global.reset)
        {
            int index;

            index = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(index);
            global.reset = false;
        }
    }
}
