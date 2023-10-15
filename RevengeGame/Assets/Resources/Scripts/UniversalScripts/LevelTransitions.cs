using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransitions : MonoBehaviour
{
    public int enemiesDefeated = 0;
    private int winCondition;

    private GameObject transition;
    public bool transitioning = false;

    // Start is called before the first frame update
    void Start()
    {
        winCondition = FindGameObjectsInLayer(8).Length;
        transition = GameObject.Find("LevelTransition");
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesDefeated == winCondition)
        {
            string currentLevel = SceneManager.GetActiveScene().name;
            switch (currentLevel)
            {
                case "LevelThree":
                    //
                    break;
                case "LevelTwo":
                    if (!transitioning) { StartCoroutine(FadeOut("LevelThree")); }
                    break;
                case "LevelOne":
                    if (!transitioning) { StartCoroutine(FadeOut("LevelTwo")); }
                    break;
                default:
                    //
                    break;
            }
        }
    }

    IEnumerator FadeIn()
    {
        transitioning = true;
        Image image = transition.GetComponent<Image>();
        Color color = image.color;

        image.color = Color.black;
        while (color.a > 0)
        {
            color.a -= 0.1f;
            image.color = color;
            yield return new WaitForSeconds(0.1f);
        }
        transitioning = false;
        transition.SetActive(false);
    }

    IEnumerator FadeOut(string level)
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
            yield return new WaitForSeconds(0.1f);
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
