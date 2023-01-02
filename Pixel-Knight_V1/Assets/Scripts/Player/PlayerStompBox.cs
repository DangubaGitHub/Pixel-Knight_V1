using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStompBox : MonoBehaviour
{
    [SerializeField] GameObject enemyDeathEffect;

    BoxCollider2D boxCollider2d;

    private void Awake()
    {
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(PlayerController.instance.isDead == true)
        {
            boxCollider2d.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);

            PlayerController.instance.BounceOnEnemy();

            Instantiate(enemyDeathEffect, other.transform.position, other.transform.rotation);
        }

        if(other.CompareTag("Enemy Invulnerable Bounce"))
        {
            PlayerController.instance.BounceOnEnemy();
            CrawlerController.instance.isCrouching = true;
            CrawlerController.instance.crouchTimerCountdown = 1f;
        }

        if (other.CompareTag("Mushroom"))
        {
            PlayerController.instance.BounceOnMushroom();
            MushroomController.instance.mushroomIsActive = true;
        }
    }
}
