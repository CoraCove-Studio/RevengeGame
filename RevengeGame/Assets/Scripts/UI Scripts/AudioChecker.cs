using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Loading Screen")
        {
            Destroy(gameObject);
        }
        else if (currentScene.name == "Loading Screen 2")
        {
            Destroy(gameObject);
        }
        else if(currentScene.name == "Loading Screen 1")
        {
            Destroy(gameObject);
        }
    }
}
