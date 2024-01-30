using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MainMenu
{
    public static PauseMenuManager instance { get; private set; }
    [SerializeField]
    public GameObject pauseMenuObject;
    private bool isPauseMenuOpened = false;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isPauseMenuOpened)
            {
                ClosePauseMenu();
            }
            else
            {
                OpenPauseMenu();
            }
        }
    }

    public void OpenPauseMenu()
    {
        UIManager.instance.DisableUI();
        pauseMenuObject.SetActive(true);
        GameManager.instance.PauseGame();
        isPauseMenuOpened = true;
    }

    public void ClosePauseMenu()
    {
        UIManager.instance.EnableUI();
        pauseMenuObject.SetActive(false);
        GameManager.instance.ResumeGame();
        isPauseMenuOpened = false;
    }

    public void RestartGame()
    {
        GameManager.instance.RestartGame();
    }
}
