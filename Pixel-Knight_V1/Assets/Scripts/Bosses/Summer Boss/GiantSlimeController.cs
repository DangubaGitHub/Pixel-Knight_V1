using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSlimeController : MonoBehaviour
{
    ////////////////////////////// Movement //////////

    [Header("Movement")]
    public float velocityX;
    public float velocityY;
    public float minVelocityX = 5f;
    public float minVelocityY = 5f;
    //[SerializeField] bool foundDirection;
    public bool isGrounded;

    float timeBetweenJumps;
    float nextJumpTime;

    ////////////////////////////// Activation //////////

    [Header("Activation")]
    public bool isActive;
    public bool isAlive;

    ////////////////////////////// Animation Controlls //////////

    const string STILL = "Giant_Slime_Still";
    const string IDLE = "Giant_Slime_Idle";
    const string UP = "Giant_Slime_Up";
    const string DOWN = "Giant_Slime_Down";
    const string DEATH = "Giant_Slime_Death";

    string currentState;

    ////////////////////////////// Declerations //////////

    Rigidbody2D rb2d;
    Animator anim;
    SpriteRenderer sr;
    CapsuleCollider2D capsuleColl;
    public static GiantSlimeController instance;

    private void Awake()
    {
        instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        capsuleColl = GetComponent<CapsuleCollider2D>();
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
                if (isGrounded)
                {
                    ChangeAnimationState(IDLE);
                    //rb2d.velocity = new Vector2(velocityX, rb2d.velocity.y);

                    if (Time.time > nextJumpTime)
                    {
                        rb2d.velocity = new Vector2(rb2d.velocity.x, velocityY);
                        nextJumpTime = Time.time + 3;
                    }
                }

                else if (!isGrounded)
                {
                    rb2d.velocity = new Vector2(velocityX, rb2d.velocity.y);

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

            else if (!isAlive && isGrounded)
            {
                ChangeAnimationState(DEATH);
                rb2d.velocity = new Vector2(0, 0);
                rb2d.gravityScale = 0;
                capsuleColl.enabled = false;
                Invoke("DelayDestruction", 1f);
            }
        }

        else
        {
            ChangeAnimationState(STILL);
            rb2d.velocity = new Vector2(0, 0);
        }

        if (rb2d.velocity.x > 0)
        {
            sr.flipX = false;
        }

        if (rb2d.velocity.x < 0)
        {
            sr.flipX = true;
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        /*if (other.CompareTag("Summer Boss"))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, minVelocityY);
        }*/
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        velocityX = -velocityX;
    }

    void DelayDestruction()
    {
        Destroy(gameObject);
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
