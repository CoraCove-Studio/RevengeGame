using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public Image black;
    public Animator anim;
    public MusicEnd musicSelectorComp; 

    public void OnPlayButtonClick()
    {
        StartCoroutine(PlayButtonClick());
    }

    public void OnPlayTwoButtonClick()
    {
        StartCoroutine(LevelTwoButtonClick());
    }

    public void OnPlayThreeButtonClick()
    {
        StartCoroutine(LevelThreeButtonClick());
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

    public void TempClick()
    {
        StartCoroutine(TempButtonClick());
    }

    public void OnLevelOpSelectorClick()
    {
        StartCoroutine(LevelOpButtonClick());
    }

    public void OnLevelOptionsClick()
    {
        StartCoroutine(OptionsTwoButtonClick());
    }

    public void OnLevelSelectorClick()
    {
        StartCoroutine(LevelButtonClick());
    }

    IEnumerator PlayButtonClick()
    {
        musicSelectorComp.MusicStop();
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(4);
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

    IEnumerator TempButtonClick()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("TempHelpScene");
    }

    IEnumerator LevelOpButtonClick()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Level Select");
    }

    IEnumerator OptionsTwoButtonClick()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("LevelSOptions");
    }

    IEnumerator LevelButtonClick()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Loading Selection");
    }

    IEnumerator LevelThreeButtonClick()
    {
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Loading Screen 2");
    }

    IEnumerator LevelTwoButtonClick()
    {
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Loading Screen 1");
    }
}

