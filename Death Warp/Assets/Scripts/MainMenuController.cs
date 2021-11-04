using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Game Controls");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
