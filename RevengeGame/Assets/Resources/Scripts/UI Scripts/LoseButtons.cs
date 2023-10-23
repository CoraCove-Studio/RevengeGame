using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry()
    {
        LevelTransitions lvlScript = GameObject.Find("InGameUI").GetComponent<LevelTransitions>();
        StartCoroutine(lvlScript.FadeOut("LevelOne"));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
