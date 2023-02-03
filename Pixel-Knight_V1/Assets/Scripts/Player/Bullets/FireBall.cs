using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator anim;
    SpriteRenderer sr;

    string currentState;
    const string DESTROY = "fireball_destroy";

    [SerializeField] float VelocityX = 15;
    [SerializeField] float VelocityY = 3;

    bool isGrounded;

    [SerializeField] GameObject enemyDeathEffect;
    [SerializeField] GameObject bulletDestroyAnimation;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        rb2d.velocity = transform.right * VelocityX;
        StartCoroutine(DelayDestroy());
        Invoke("DestroyBall", 3.2f);
    }

    void Update()
    {
        if (isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, VelocityY);
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" ||
            other.gameObject.tag == "Slime" ||
            other.gameObject.tag == "Slime Purple" ||
            other.gameObject.tag == "Slime Blue" ||
            other.gameObject.tag == "Enemy Wizard")
        {
            
            Instantiate(enemyDeathEffect, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Ground" ||
            other.gameObject.tag == "Ground 2" ||
            other.gameObject.tag == "Spikes")
        {
            isGrounded = true;
        }

        if (other.gameObject.tag == "Wall")
        {
            rb2d.velocity = new Vector2(0, 0);
            Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
            sr.enabled = false;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            Invoke("DestroyBall", 1f);
        }

        if (other.gameObject.tag == "Enemy Invulnerable Damaging" ||
            other.gameObject.tag == "Enemy Invulnerable Bounce" ||
            other.gameObject.tag == "Slime Red")
        {
            rb2d.velocity = new Vector2(0, 0);
            Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
            sr.enabled = false;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            Invoke("DestroyBall", 1f);
        }

        if (other.gameObject.tag == "Bomb Worm")
        {
            if (!BombWormController.instance.enraged)
            {
                BombWormController.instance.isChanging = true;
                Destroy(gameObject);
            }

            else if (BombWormController.instance.enraged)
            {
                rb2d.velocity = new Vector2(0, 0);
                Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
                sr.enabled = false;
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
                Invoke("DestroyBall", 1f);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" ||
            other.gameObject.tag == "Ground 2" ||
            other.gameObject.tag == "Spikes")
        {
            isGrounded = false;
        }
    }

    void DestroyBall()
    {
        Destroy(gameObject);
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(2);
        ChangeAnimationState(DESTROY);
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
