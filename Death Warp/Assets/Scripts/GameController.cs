using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Text itemUIText;
    [SerializeField] private Text playerDeathsUIText;

    private int totalGemsCollected = 0;
    private int totalPlayerDeaths = 0;
    private OptionsMenuController optionsMenuController;
    private static GameController _instance;
    public static GameController instance { get { return _instance; } }

    private void Start()
    {
        optionsMenuController = pauseMenu.GetComponent<OptionsMenuController>();
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
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

    private void UpdateItemUI()
    {
        itemUIText.text = " x " + totalGemsCollected;
    }

    public void PickUpGem()
    {
        totalGemsCollected++;
        UpdateItemUI();
    }

    private void UpdateDeathCounter()
    {
        playerDeathsUIText.text = " x " + totalPlayerDeaths;
    }

    public void IncrementDeathCounter()
    {
        totalPlayerDeaths++;
        UpdateDeathCounter();
    }

}
