using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiterHorizontalController : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator anim;

    ////////////////////  ANIMATIONS  ///

    const string STILL = "Biter_Horizontal_Still";
    const string BITING = "Biter_Horizontal_Biting";

    string currentState;

    [SerializeField] GameObject enemyDeathEffect;

    ////////////////////  Activation  ///

    [Header("Activation")]
    [SerializeField] bool isActive;

    ////////////////////  Movement  ///

    [Header("Movement")]
    public float horizontalVelocity;
    [SerializeField] float verticalForce;
    [SerializeField] bool foundDirection;
    [SerializeField] GameObject Player;

    [SerializeField] bool isGrounded;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isActive)
        {
            ChangeAnimationState(BITING);

            if (!foundDirection)
            {
                if (Player.transform.position.x > transform.position.x && transform.localScale.x == -1)
                {
                    ChangeDirection();
                }

                else if (Player.transform.position.x < transform.position.x && transform.localScale.x == 1)
                {
                    ChangeDirection();
                }

                foundDirection = true;
            }

            rb2d.velocity = new Vector2(horizontalVelocity, rb2d.velocity.y);

            if (isGrounded)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, verticalForce);
            }
        }

        else
        {
            ChangeAnimationState(STILL);

            rb2d.velocity = new Vector2(0f, rb2d.velocity.y);

            foundDirection = false;
        }

        //////////////////// Fliping ///

        Vector3 characterScale = transform.localScale;

        if (rb2d.velocity.x < -0.1f)
        {
            characterScale.x = -1;
        }

        else if (rb2d.velocity.x > 0.1f)
        {
            characterScale.x = 1;
        }

        transform.localScale = characterScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground") ||
            other.CompareTag("Ground 2") ||
            other.CompareTag("Spikes"))
        {
            isGrounded = true;
        }

        if (other.CompareTag("Player Stomp Box"))
        {
            if (!PlayerController.instance.isGrounded)
            {
                PlayerController.instance.BounceOnEnemy();

                Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Ground") ||
            other.CompareTag("Ground 2") ||
            other.CompareTag("Spikes"))
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (PlayerAnimationManager.instance.isArmor)
            {
                ChangeDirection();
            }

            if (PlayerAnimationManager.instance.isFire)
            {
                ChangeDirection();
            }

            if (PlayerAnimationManager.instance.isIce)
            {
                ChangeDirection();
            }

            if (PlayerAnimationManager.instance.isBasic)
            {
                ChangeAnimationState(STILL);
            }
        }

        if(other.gameObject.tag == "Enemy")
        {
            ChangeDirection();
        }

        if(other.gameObject.tag == "Wall")
        {
            ChangeDirection();
        }

        if (other.gameObject.tag == "Turn Around Trigger")
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        horizontalVelocity = -horizontalVelocity;
    }

    private void OnBecameVisible()
    {
        isActive = true;
    }

    private void OnBecameInvisible()
    {
        isActive = false;
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
