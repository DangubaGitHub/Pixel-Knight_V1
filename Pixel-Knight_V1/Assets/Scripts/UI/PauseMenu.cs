using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public bool gameIsPaused;

    public static PauseMenu instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }

        
    }

    public void PauseUnpause()
    {
        if (gameIsPaused)
        {
            gameIsPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }

        else
        {
            gameIsPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
        gameIsPaused = false;
    }
}
