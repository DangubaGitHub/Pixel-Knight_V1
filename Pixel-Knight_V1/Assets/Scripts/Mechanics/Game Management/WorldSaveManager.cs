using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveManager : MonoBehaviour
{
    public string currentWorld;

    public static WorldSaveManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SaveWorld();
    }


   void SaveWorld()
    {
        currentWorld = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("Save World", currentWorld);
    }
}
