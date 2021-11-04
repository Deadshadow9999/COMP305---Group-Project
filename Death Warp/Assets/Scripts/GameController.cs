using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[System.Serializable]
public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private OptionsController optionsControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        optionsControllerScript = pauseMenu.GetComponent<OptionsController>();
    }

    // Update is called once per frame
    void Update()
    {

        // Show / Hide pause menu
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            optionsControllerScript.gameIsPaused = !optionsControllerScript.gameIsPaused;
        }

        if(optionsControllerScript.gameIsPaused == true)
        {
            pauseMenu.GetComponent<OptionsController>().PauseGame();
        }
        else if(optionsControllerScript.gameIsPaused == false)
        {
            pauseMenu.GetComponent<OptionsController>().ResumeGame();
        }
    }
}
