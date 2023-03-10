using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Red Coins")]
    [SerializeField] Image redCoin1;
    [SerializeField] Image redCoin2;
    [SerializeField] Image redCoin3;
    [SerializeField] Image redCoin4;
    [SerializeField] Image redCoin5;
    [SerializeField] Sprite redCoinEmpty;
    [SerializeField] Sprite redCoinFull;


    [Header("Gold Coins")]
    [SerializeField] Text goldCoinCountText;


    [Header("Lives Count")]
    [SerializeField] Text livesCountText;


    [Header("Level Name")]
    [SerializeField] Text levelNameText;

    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void RedCoinUpdate()
    {
        switch (LevelManager.instance.redCoinsCollected) 
        {
            case 0:
                redCoin1.sprite = redCoinEmpty;
                redCoin2.sprite = redCoinEmpty;
                redCoin3.sprite = redCoinEmpty;
                redCoin4.sprite = redCoinEmpty;
                redCoin5.sprite = redCoinEmpty;
                break;

            case 1:
                redCoin1.sprite = redCoinFull;
                redCoin2.sprite = redCoinEmpty;
                redCoin3.sprite = redCoinEmpty;
                redCoin4.sprite = redCoinEmpty;
                redCoin5.sprite = redCoinEmpty;
                break;

            case 2:
                redCoin1.sprite = redCoinFull;
                redCoin2.sprite = redCoinFull;
                redCoin3.sprite = redCoinEmpty;
                redCoin4.sprite = redCoinEmpty;
                redCoin5.sprite = redCoinEmpty;
                break;

            case 3:
                redCoin1.sprite = redCoinFull;
                redCoin2.sprite = redCoinFull;
                redCoin3.sprite = redCoinFull;
                redCoin4.sprite = redCoinEmpty;
                redCoin5.sprite = redCoinEmpty;
                break;

            case 4:
                redCoin1.sprite = redCoinFull;
                redCoin2.sprite = redCoinFull;
                redCoin3.sprite = redCoinFull;
                redCoin4.sprite = redCoinFull;
                redCoin5.sprite = redCoinEmpty;
                break;

            case 5:
                redCoin1.sprite = redCoinFull;
                redCoin2.sprite = redCoinFull;
                redCoin3.sprite = redCoinFull;
                redCoin4.sprite = redCoinFull;
                redCoin5.sprite = redCoinFull;

                PlayerHealthController.instance.AddLive();

                break;
        }
    }

    public void GoldCoinUpdate()
    {
        goldCoinCountText.text = LevelManager.instance.goldCoinsCollected.ToString();
    }

    public void LivesUpdate()
    {
        livesCountText.text = PlayerHealthController.instance.livesCount.ToString();
    }

    public void LevelUpdate()
    {
        levelNameText.text = LevelManager.instance.currentLevel;
    }
}
