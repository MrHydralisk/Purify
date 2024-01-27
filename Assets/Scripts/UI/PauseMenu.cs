using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MainMenu
{
    [SerializeField]
    public GameObject uIObject;
    [SerializeField]
    public GameObject pauseMenuObject;
    private bool isGamePaused = false; 

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        uIObject.SetActive(false);
        pauseMenuObject.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        uIObject.SetActive(true);
        pauseMenuObject.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
}
