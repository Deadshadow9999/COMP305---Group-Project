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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Controls()
    {
        transform.parent.GetChild(1).gameObject.SetActive(true);
        transform.parent.GetChild(0).gameObject.SetActive(false);
    }

    public void SwitchToMainMenuScene()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        transform.parent.GetChild(1).gameObject.SetActive(false);
        transform.parent.GetChild(0).gameObject.SetActive(true);
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
