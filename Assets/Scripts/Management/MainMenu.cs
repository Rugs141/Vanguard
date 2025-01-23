using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string LevelToLoad = "MainLevel";
    public void PlayGame()
    {
        SceneManager.LoadScene(LevelToLoad);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game now...");
        Application.Quit();
    }
}
