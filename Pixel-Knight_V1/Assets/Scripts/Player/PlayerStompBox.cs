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
        if (PlayerController.instance.rb2d.velocity.y <= 0)
        {
            if (other.CompareTag("Enemy") ||
                other.CompareTag("Enemy Wizard"))
            {
                Destroy(other.gameObject);

                PlayerController.instance.BounceOnEnemy();

                Instantiate(enemyDeathEffect, other.transform.position, other.transform.rotation);
            }

            if (other.CompareTag("Enemy Invulnerable Bounce"))
            {
                PlayerController.instance.BounceOnEnemy();
                CrawlerController.instance.isCrouching = true;
                CrawlerController.instance.crouchTimerCountdown = 1f;
            }

            if (other.CompareTag("Slime"))
            {
                if (!SlimeGreenController.instance.isGrounded)
                {
                    Destroy(other.gameObject);

                    PlayerController.instance.BounceOnEnemy();

                    Instantiate(enemyDeathEffect, other.transform.position, other.transform.rotation);
                }

                else if (SlimeGreenController.instance.isGrounded)
                {
                    SlimeGreenController.instance.isAlive = false;

                    PlayerController.instance.BounceOnEnemy();

                    Destroy(other.gameObject, 1.7f);
                }
            }

            if(other.CompareTag("Slime Purple"))
            {
                SlimePurpleController.instance.isAlive = false;

                PlayerController.instance.BounceOnEnemy();

                Destroy(other.gameObject, 1.7f);
            }

            if (other.CompareTag("Slime Red"))
            {
                if (!SlimeRedController.instance.isGrounded)
                {
                    Destroy(other.gameObject);

                    PlayerController.instance.BounceOnEnemy();

                    Instantiate(enemyDeathEffect, other.transform.position, other.transform.rotation);
                }
            }

            if (other.CompareTag("Slime Blue"))
            {
                if(!SlimeBlueController.instance.isGrounded)
                {
                    Destroy(other.gameObject);

                    PlayerController.instance.BounceOnEnemy();

                    Instantiate(enemyDeathEffect, other.transform.position, other.transform.rotation);
                }

                if(SlimeBlueController.instance.isGrounded)
                {
                    SlimeBlueController.instance.isAlive = false;

                    PlayerController.instance.BounceOnEnemy();

                    Destroy(other.gameObject, 1.7f);
                }
            }

            if (other.CompareTag("Bomb Worm"))
            {
                if (!BombWormController.instance.enraged)
                {
                    PlayerController.instance.BounceOnEnemy();
                    BombWormController.instance.isChanging = true;
                }

                else if (BombWormController.instance.enraged == true)
                {
                    PlayerController.instance.BounceOnEnemy();
                }
            }

            if (other.CompareTag("Summer Boss"))
            {
                PlayerController.instance.BounceOnEnemy();
            }
        }

        if (other.CompareTag("Mushroom"))
        {
            PlayerController.instance.BounceOnMushroom();
            MushroomController.instance.mushroomIsActive = true;
        }
    }
}
