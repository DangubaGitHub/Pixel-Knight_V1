using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningZombieController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject enemyDeathEffect;

    Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            moveSpeed = -moveSpeed;
            FlipEnemy();
        }

        if (other.CompareTag("Enemy"))
        {
            moveSpeed = -moveSpeed;
            FlipEnemy();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            moveSpeed = -moveSpeed;
            FlipEnemy();
            if (PlayerAnimationManager.instance.isBasic == true)
            {
                Instantiate(enemyDeathEffect, other.transform.position, other.transform.rotation);
            }
        }
    }

    void FlipEnemy()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rb2d.velocity.x)), 1f);
    }
}
