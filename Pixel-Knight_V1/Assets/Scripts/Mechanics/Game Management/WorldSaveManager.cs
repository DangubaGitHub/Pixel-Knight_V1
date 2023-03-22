using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveManager : MonoBehaviour
{
    [SerializeField] string currentWorld;

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
