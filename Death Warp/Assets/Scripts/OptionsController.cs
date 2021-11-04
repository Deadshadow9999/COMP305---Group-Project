using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour
{

    public bool gameIsPaused = false;
    private GameObject optionsMenu;

    private void Start()
    {
        optionsMenu = GameObject.Find("OptionsPanel");
    }

    // Unpauses the game
    public void ResumeGame()
    {
        gameIsPaused = false;
        Time.timeScale = 1;
        optionsMenu.SetActive(false);
    }

    // Pauses the game
    public void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0;
        optionsMenu.SetActive(true);
    }

    // Sends the player back to the main menu
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
