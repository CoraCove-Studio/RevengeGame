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

        if (currentScene.name == "LevelThree_Cutscene")
        {
            Destroy(gameObject);
        }
        else if (currentScene.name == "LevelTwo_Cutscene")
        {
            Destroy(gameObject);
        }
    }
}
