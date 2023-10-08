using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public Image black;
    public Animator anim;
    public int secondsToZero = 5;

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
        StartCoroutine(findAudioAndFadeOut());
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
        StartCoroutine(findAudioAndFadeOut());
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Loading Screen 2");
    }

    IEnumerator LevelTwoButtonClick()
    {
        StartCoroutine(findAudioAndFadeOut());
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Loading Screen 1");
    }

    IEnumerator findAudioAndFadeOut()
    {
        // Find Audio Music in scene
        AudioSource audioMusic = GameObject.Find("AudioManager").GetComponent<AudioSource>();

        // Check Music Volume and Fade Out
        while (audioMusic.volume > 0.2f)
        {
            audioMusic.volume -= Time.deltaTime / secondsToZero;
            yield return null;
        }

        // Make sure volume is set to 0
        audioMusic.volume = 0;

        // Stop Music
        audioMusic.Stop();

        // Destroy
        Destroy(this);
    }
}
