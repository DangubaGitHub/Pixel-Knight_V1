using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinController : MonoBehaviour
{
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
            isCollected = true;

            LevelManager.instance.goldCoinsCollected++;

            Destroy(gameObject);
        }
    }
}
