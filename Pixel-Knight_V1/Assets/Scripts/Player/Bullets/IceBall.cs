using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour
{
    Rigidbody2D rb2d;

    float force = 25;

    [SerializeField] GameObject enemyDeathEffect;
    [SerializeField] GameObject bulletDestroyAnimation;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb2d.velocity = transform.right * force;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
        {
            Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.CompareTag("Wall"))
        {
            Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy") ||
            other.CompareTag("Enemy Wizard") ||
            other.CompareTag("Slime") ||
            other.CompareTag("Slime Red") ||
            other.CompareTag("Slime Purple"))
        {
            Instantiate(enemyDeathEffect, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy Invulnerable Bounce") ||
            other.CompareTag("Enemy Invulnerable Damaging") ||
            other.CompareTag("Slime Blue"))
        {
            Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.CompareTag("Bomb Worm"))
        {
            if (!BombWormController.instance.enraged)
            {
                BombWormController.instance.isChanging = true;
                Destroy(gameObject);
            }

            if (BombWormController.instance.enraged)
            {
                Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        if (other.gameObject.tag == ("Summer Boss"))
        {
            if (!SlimeKnightHealthController.instance.lostArmor ||
               SlimeKnightAnimationController.instance.noArmor && SlimeKnightHealthController.instance.invulnerable > 0)
            {
                Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            if (SlimeKnightAnimationController.instance.zero_Hit)
            {
                Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            if (SlimeKnightAnimationController.instance.one_Hit)
            {
                Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            if (SlimeKnightAnimationController.instance.two_Hit)
            {
                Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            if (SlimeKnightAnimationController.instance.three_Hit)
            {
                Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            if (SlimeKnightAnimationController.instance.hurt)
            {
                Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
