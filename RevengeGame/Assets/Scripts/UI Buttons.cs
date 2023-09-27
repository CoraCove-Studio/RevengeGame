using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
   public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void OnOptionsButtonClick()
    {
        SceneManager.LoadScene("Options Screen");
    }

    public void OnCreditsButtonClick()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnQuitGameClick()
    {
        Application.Quit();
    }
}
