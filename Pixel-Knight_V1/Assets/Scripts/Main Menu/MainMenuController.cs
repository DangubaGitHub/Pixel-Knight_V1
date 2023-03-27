using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] GameObject ContinueButton;

    void Start()
    {
        if (PlayerPrefs.HasKey("Save World"))
        {
            ContinueButton.SetActive(true);
        }

        else
            ContinueButton.SetActive(false);
        

    }

    void Update()
    {
        
    }

    public void ContinueGame()
    {
        LevelLoaderController.Instance.LoadSavedLevel();
        PlayerPrefs.DeleteKey("Coin Count");
        PlayerPrefs.DeleteKey("Lives Save");
        PlayerPrefs.DeleteKey("Armor Power");
        PlayerPrefs.DeleteKey("Fire Power");
        PlayerPrefs.DeleteKey("Ice Power");
    }

    public void NewGame()
    {
        LevelLoaderController.Instance.LoadNextLevel();
        PlayerPrefs.DeleteKey("Coin Count");
        PlayerPrefs.DeleteKey("Lives Save");
        PlayerPrefs.DeleteKey("Armor Power");
        PlayerPrefs.DeleteKey("Fire Power");
        PlayerPrefs.DeleteKey("Ice Power");
    }

    public void ExitGame()
    {
        LevelLoaderController.Instance.QuitGame();
        PlayerPrefs.DeleteKey("Coin Count");
        PlayerPrefs.DeleteKey("Lives Save");
        PlayerPrefs.DeleteKey("Armor Power");
        PlayerPrefs.DeleteKey("Fire Power");
        PlayerPrefs.DeleteKey("Ice Power");
    }
}
