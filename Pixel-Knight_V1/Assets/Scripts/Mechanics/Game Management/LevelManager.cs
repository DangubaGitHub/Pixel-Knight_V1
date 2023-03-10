using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int goldCoinsCollected;

    public int redCoinsCollected;

    public string currentLevel;

    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        redCoinsCollected = 0;

        UIController.instance.LevelUpdate();

        if (PlayerPrefs.HasKey("Coin Count"))
        {
            goldCoinsCollected = PlayerPrefs.GetInt("Coin Count");

            UIController.instance.GoldCoinUpdate();
        }
    }

    void Update()
    {
        /*if(redCoinsCollected >= 5) 
        {
            PlayerAnimationManager.instance.LiveUpAnimation();
            redCoinsCollected = 0;
        }*/
    }

    public void AddRedCoin()
    {
        redCoinsCollected++;

        UIController.instance.RedCoinUpdate();
    }

    public void AddGoldCoin()
    {
        goldCoinsCollected++;

        UIController.instance.GoldCoinUpdate();
    }

    public void LevelEnd()
    {
        PlayerPrefs.SetInt("Coin Count", goldCoinsCollected);
    }
}
