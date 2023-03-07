using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{

    [SerializeField] bool redCoin;
    [SerializeField] bool goldCoin;

    bool isCollected;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            if (redCoin)
            {
                LevelManager.instance.AddRedCoin();

                isCollected = true;

                Destroy(gameObject);
            }

            if (goldCoin)
            {
                LevelManager.instance.AddGoldCoin();

                isCollected = true;

                Destroy(gameObject);
            }
        }
    }
}
