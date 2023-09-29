using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   private void Awake()
    {
        SceneManager.LoadSceneAsync((int)SceneIndexes.MainMenu, LoadSceneMode.Additive);
    }

    public void LoadGame()
    {
        SceneManager.UnloadSceneAsync((int)SceneIndexes.MainMenu);
        SceneManager.LoadSceneAsync((int)SceneIndexes.Options, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync((int)SceneIndexes.Credits, LoadSceneMode.Additive);
    }
}
