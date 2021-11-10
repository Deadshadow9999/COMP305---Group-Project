using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[System.Serializable]
public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private OptionsMenuController optionsMenuController;

    private void Start()
    {
        optionsMenuController = pauseMenu.GetComponent<OptionsMenuController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            optionsMenuController.gameIsPaused = !optionsMenuController.gameIsPaused;
        }

        if(optionsMenuController.gameIsPaused == true)
        {
            optionsMenuController.PauseGame();
        }
        else
        {
            optionsMenuController.ResumeGame();
        }
    }

}
