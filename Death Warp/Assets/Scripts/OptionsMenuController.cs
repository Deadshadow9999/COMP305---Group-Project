using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuController : MonoBehaviour
{

    public bool gameIsPaused;

    public void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        gameIsPaused = false;
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
