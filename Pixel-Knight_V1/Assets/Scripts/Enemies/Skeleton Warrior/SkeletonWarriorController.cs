using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorController : MonoBehaviour
{
    ////////////////////////////// Movement //////////

    [Header("Movement")]
    public float velocityX;
    [SerializeField] float attackVelocityX;
    //public float velocityY;
    [SerializeField] bool foundDirection;
    public bool isGrounded;

    [SerializeField] Transform playerTransform;

    [SerializeField] bool isAttacking;

    ////////////////////////////// Activation //////////

    [Header("Activation")]
    [SerializeField] bool isActive;

    [SerializeField] GameObject Player;
    //public bool isAlive;

    ////////////////////////////// Animation Controlls //////////

    const string STILL = "Skeleton_Warrior_Still";
    const string MOVE = "Skeleton_Warrior_Move";
    const string ATTACK = "Skeleton_Warrior_Attack";

    string currentState;

    [SerializeField] GameObject enemyDeathEffect;
    [SerializeField] GameObject bulletDestroyAnimation;

    ////////////////////////////// Declerations //////////

    Rigidbody2D rb2d;
    Animator anim;
    //public static SkeletonWarriorController instance;

    private void Awake()
    {
        //instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (isActive)
        {
            if (!foundDirection)
            {
                if (Player.transform.position.x > transform.position.x && transform.localScale.x == -1)
                {
                    ChangeDirection();
                }

                if (Player.transform.position.x < transform.position.x && transform.localScale.x == 1)
                {
                    ChangeDirection();
                }

                foundDirection = true;
            }

            if (isAttacking)
            {
                ChangeAnimationState(ATTACK);

                if (Player.transform.position.x > transform.position.x)
                {
                    rb2d.velocity = new Vector2(attackVelocityX, 0);
                }

                if (Player.transform.position.x < transform.position.x)
                {
                    rb2d.velocity = new Vector2(-attackVelocityX, 0);
                }
            }

            else
            {
                ChangeAnimationState(MOVE);

                rb2d.velocity = new Vector2(velocityX, 0);
            }
        }

        else 
        {
            ChangeAnimationState(STILL);
            rb2d.velocity = new Vector2(0, 0);
            foundDirection = false;
        }

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
        if (other.CompareTag("Player"))
        {
            isAttacking = true;
        }

        if (other.CompareTag("Player Stomp Box"))
        {
            PlayerController.instance.BounceOnEnemy();

            Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
            AudioController.instance.PlaySFX(5);                                             /////// SFX //
            Destroy(gameObject);
        }

        if (other.CompareTag("Player Fire Magic") ||
            other.CompareTag("Player Ice Magic"))
        {
            Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            AudioController.instance.PlaySFX(5);                                             /////// SFX //
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isAttacking = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall" ||
            other.gameObject.tag == "Player" ||
            other.gameObject.tag == "Enemy")
        {
            ChangeDirection();
        }
    }

    public void ChangeDirection()
    {
        velocityX = -velocityX;
    }

    private void OnBecameVisible()
    {
        isActive = true;
    }

    private void OnBecameInvisible()
    {
        isActive = false;
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
