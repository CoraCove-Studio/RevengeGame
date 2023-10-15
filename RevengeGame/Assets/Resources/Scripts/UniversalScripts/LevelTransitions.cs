using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitions : MonoBehaviour
{
    public int enemiesDefeated = 0;
    private int winCondition;

    // Start is called before the first frame update
    void Start()
    {
        winCondition = FindGameObjectsInLayer(8).Length;
        Debug.Log(winCondition);
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
                    SceneManager.LoadScene("LevelThree");
                    break;
                case "LevelOne":
                    SceneManager.LoadScene("LevelTwo");
                    break;
                default:
                    //
                    break;
            }
        }
    }

    GameObject[] FindGameObjectsInLayer(int layer) // http://answers.unity3d.com/questions/179310/how-to-find-all-objects-in-specific-layer.html_
    {
        var goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var goList = new System.Collections.Generic.List<GameObject>();
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
