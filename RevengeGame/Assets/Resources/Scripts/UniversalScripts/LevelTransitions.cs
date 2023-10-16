using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransitions : MonoBehaviour
{
    private string currentLevel;

    public int enemiesDefeated = 0;
    private int winCondition;

    private GameObject transition;
    public bool transitioning = false;
    private GameObject cutsceneBG;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().name;
        try { winCondition = FindGameObjectsInLayer(8).Length; }
        catch (NullReferenceException) { winCondition = 100; }
        transition = GameObject.Find("LevelTransition");
        cutsceneBG = gameObject.transform.GetChild(0).gameObject;
        StartCoroutine(FadeIn());
        CheckCutscene();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesDefeated == winCondition)
        {
            switch (currentLevel)
            {
                case "LevelThree":
                    //
                    break;
                case "LevelTwo":
                    if (!transitioning) { StartCoroutine(FadeOut("LevelThree_Cutscene")); }
                    break;
                case "LevelOne":
                    if (!transitioning) { StartCoroutine(FadeOut("LevelTwo_Cutscene")); }
                    break;
                default:
                    //
                    break;
            }
        }
    }

    void CheckCutscene()
    {
        if (currentLevel.EndsWith("_Cutscene"))
        {
            Image bg = cutsceneBG.GetComponent<Image>();
            switch (currentLevel)
            {
                case "LevelThree_Cutscene":
                    bg.sprite = Resources.Load<Sprite>("2D/L3-Cutscene_1");
                    break;
                case "LevelTwo_Cutscene":
                    bg.sprite = Resources.Load<Sprite>("2D/L2-Cutscene_1");
                    break;
                case "LevelOne_Cutscene":
                    bg.color = Color.black;
                    break;
                default:
                    break;
            }
        }
        else { cutsceneBG.SetActive(false); }
    }

    public IEnumerator FadeIn()
    {
        transitioning = true;
        Image image = transition.GetComponent<Image>();
        Color color = image.color;

        image.color = Color.black;
        while (color.a > 0)
        {
            color.a -= 0.1f;
            image.color = color;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        transitioning = false;
        transition.SetActive(false);
    }

    public IEnumerator FadeOut(string level)
    {
        transitioning = true;
        transition.SetActive(true);
        Image image = transition.GetComponent<Image>();
        Color color = image.color;

        image.color = Color.clear;
        while (color.a != 1)
        {
            color.a += 0.1f;
            image.color = color;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        transitioning = false;
        SceneManager.LoadScene(level);
    }

    GameObject[] FindGameObjectsInLayer(int layer) // http://answers.unity3d.com/questions/179310/how-to-find-all-objects-in-specific-layer.html_
    {
        var goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var goList = new List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer)
            {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }
        return goList.ToArray();
    }
}
