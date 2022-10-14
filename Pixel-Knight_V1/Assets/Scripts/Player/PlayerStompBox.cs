using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStompBox : MonoBehaviour
{
    [SerializeField] GameObject enemyDeathEffect;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);

            PlayerController.instance.BounceOnEnemy();

            Instantiate(enemyDeathEffect, other.transform.position, other.transform.rotation);
        }
    }
}
