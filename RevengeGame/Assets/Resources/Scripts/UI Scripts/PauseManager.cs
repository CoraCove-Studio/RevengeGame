using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;
    
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        foreach (Transform child in gameObject.transform) { if (child.gameObject.name != "PauseMenu") { child.gameObject.SetActive(false); } }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        foreach (Transform child in gameObject.transform)
        {
            if (child.gameObject.name != "PauseMenu") { child.gameObject.SetActive(true); }
            else { child.gameObject.SetActive(false); }
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PersistentScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
