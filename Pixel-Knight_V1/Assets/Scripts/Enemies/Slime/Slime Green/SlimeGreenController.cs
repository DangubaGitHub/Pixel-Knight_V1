using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGreenController : MonoBehaviour
{
    ////////////////////////////// Movement //////////

    public float velocityX;
    public float velocityY;
    [SerializeField] bool foundDirection;
    public bool isGrounded;

    float timeBetweenJumps;
    float nextJumpTime;

    ////////////////////////////// Activation //////////

    [Header("Activation")]
    [SerializeField] bool isActive;

    [SerializeField] GameObject Player;
    public bool isAlive;

    ////////////////////////////// Animation Controlls //////////

    const string STILL = "Slime_Green_Still";
    const string IDLE = "Slime_Green_Idle";
    const string UP = "Slime_Green_Up";
    const string DOWN = "Slime_Green_Down";
    const string DEATH = "Slime_Green_Death";

    string currentState;

    [SerializeField] GameObject enemyDeathEffect;

    ////////////////////////////// Declerations //////////

    Rigidbody2D rb2d;
    Animator anim;
    CircleCollider2D circleCollider;
    BoxCollider2D boxCollider;
    //public static SlimeGreenController instance;

    private void Awake()
    {
        //instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        isAlive = true;
    }

    void Update()
    {
        if (isActive)
        {
            if (isAlive)
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

                if (isGrounded)
                {
                    ChangeAnimationState(IDLE);
                    rb2d.velocity = new Vector2(velocityX, rb2d.velocity.y);

                    if(Time.time > nextJumpTime)
                    {
                        rb2d.velocity = new Vector2(rb2d.velocity.x, velocityY);
                        nextJumpTime = Time.time + Random.Range(2, 4);
                    }
                }

                if (!isGrounded)
                {
                    if (rb2d.velocity.y > 0)
                    {
                        ChangeAnimationState(UP);
                    }

                    else if (rb2d.velocity.y < 0)
                    {
                        ChangeAnimationState(DOWN);
                    }
                }
            }

            else if(!isAlive && isGrounded)
            {
                
                ChangeAnimationState(DEATH);
            }
        }

        else
        {
            ChangeAnimationState(STILL);
            foundDirection = false;
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
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
        if(other.CompareTag("Ground") ||
            other.CompareTag("Ground 2") ||
            other.CompareTag("Spikes") ||
            other.CompareTag("Enemy"))
        {
            isGrounded = true;
        }

        if (other.CompareTag("Player Stomp Box"))
        {
            if (!isGrounded)
            {
                PlayerController.instance.BounceOnEnemy();

                Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }

            else if (isGrounded)
            {
                isAlive = false;
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
                PlayerController.instance.BounceOnEnemy();
                Destroy(gameObject, 1.4f);
            }
        }

        if (other.CompareTag("Player Fire Magic") ||
            other.CompareTag("Player Ice Magic"))
        {
            Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Spikes"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Ground") ||
            other.CompareTag("Ground 2") ||
            other.CompareTag("Spikes") ||
            other.CompareTag("Enemy"))
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Wall" ||
            other.gameObject.tag == "Turn Around Trigger")
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
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
