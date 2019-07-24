using UnityEngine;

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

        }
        else
        {
            
        }

        if(global.reset)
        {
            
        }
        else
        {
            
        }
    }
}
