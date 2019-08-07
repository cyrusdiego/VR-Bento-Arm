using UnityEngine;


public class Persistent : MonoBehaviour
{
    void Awake()
    {
        // https://answers.unity.com/questions/233284/objects-being-duplicated-with-dontdestroyonload.html
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
