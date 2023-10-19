using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectScript : MonoBehaviour
{
    public void OnOneButtonClick()
    {
        StartCoroutine(LevelButtonClick());
    }

    public void OnTwoButtonClick()
    {
        StartCoroutine(LevelButtonTwoClick());
    }

    public void OnThreeButtonClick()
    {
        StartCoroutine(LevelButtonThreeClick());
    }

    IEnumerator LevelButtonClick()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("CoffeeBrawl");
    }
    
    IEnumerator LevelButtonTwoClick()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Level2");
    }
    
    IEnumerator LevelButtonThreeClick()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Moon Mercenary");
    }
}
