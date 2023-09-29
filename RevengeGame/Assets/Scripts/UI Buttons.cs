using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
   public void OnPlayButtonClick()
    {
        StartCoroutine(PlayButtonClick());
    }

    public void OnOptionsButtonClick()
    {
        StartCoroutine(OptionsButtonClick());
    }

    public void OnCreditsButtonClick()
    {
        StartCoroutine(CreditButtonClick());
    }

    public void OnQuitGameClick()
    {
        StartCoroutine(QuitButtonClick());
    }

    public void OnMenuClick()
    {
        StartCoroutine(MenuButtonClick());
    }

    IEnumerator PlayButtonClick()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Loading Screen");
    }

    IEnumerator OptionsButtonClick()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Options Screen");
    }

    IEnumerator MenuButtonClick()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator CreditButtonClick()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Credits");
    }

    IEnumerator QuitButtonClick()
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
