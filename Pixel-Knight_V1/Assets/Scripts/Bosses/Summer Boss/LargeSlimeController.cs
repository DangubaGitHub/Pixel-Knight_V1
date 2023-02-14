using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeSlimeController : MonoBehaviour
{
    [Header("Direction")]
    [SerializeField] bool Left;
    [SerializeField] bool Right;

    //////////////////////////////////////////////////////////// Movement //////////

    [Header("Movement")]
    [SerializeField] float velocityX;
    [SerializeField] float velocityY;

    //float minVelocityX = 20f;
    float minVelocityY = 20f;

    [SerializeField] bool isGrounded;

    float nextJumpTime;

    [SerializeField] bool isAlive;

    //////////////////////////////////////////////////////////// Animation //////////

    const string STILL = "Large_Slime_Still";
    const string IDLE = "Large_Slime_Idle";
    const string UP = "Large_Slime_Up";
    const string DOWN = "Large_Slime_Down";
    const string DEATH = "Large_Slime_Death";
    const string START = "Large_Slime_Start";

    string currentState;

    //////////////////////////////////////////////////////////// Declerations //////////

    Rigidbody2D rb2d;
    Animator anim;
    SpriteRenderer sr;
    public static LargeSlimeController instance;

    private void Awake()
    {
        instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        velocityX = Random.Range(5f, 15f);
        velocityY = Random.Range(30f, 45f);
    }

    void Start()
    {
        if (Left)
        {
            velocityX = -velocityX;
        }

        if (Right)
        {
            //velocityX = velocityX;
        }

        isAlive = true;

        //ChangeAnimationState(START);
    }

    void Update()
    {
        if (isAlive)
        {
            if (isGrounded)
            {
                ChangeAnimationState(IDLE);

                if (Time.time > nextJumpTime)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, velocityY);
                    nextJumpTime = Time.time + Random.Range(2, 4);
                }
            }

            else if(!isGrounded)
            {
                rb2d.velocity = new Vector2(velocityX, rb2d.velocity.y);

                if (rb2d.velocity.y > 0)
                {
                    ChangeAnimationState(UP);
                }

                if (rb2d.velocity.y < 0)
                {
                    ChangeAnimationState(DOWN);
                }
            }
        }

        else
        {
            
        }

        /*if (rb2d.velocity.x > 0)
        {
            sr.flipX = false;
        }

        if (rb2d.velocity.x < 0)
        {
            sr.flipX = true;
        }*/

        if (velocityX < 0)
        {
            sr.flipX = true;
        }

        else if(velocityX > 0)
        {
            sr.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (other.CompareTag("Summer Boss"))
        {
            rb2d.velocity = new Vector2(velocityX, minVelocityY);
        }
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

        if (other.gameObject.tag == "Summer Boss")
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        velocityX = -velocityX;
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
