using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public Player player;
    private bool isGamePaused = false;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void ResumeGame()
    {
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
