using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGreenController : MonoBehaviour
{
    ////////////////////////////// Movement //////////

    public float velocityX;
    public float velocityY;
    [SerializeField] bool foundDirection;
    [SerializeField] bool isGrounded;
    [SerializeField] float nextJumpTime;

    float nextTime;
    //float nextJump = Random.Range(2, 3);

    ////////////////////////////// Activation //////////

    [Header("Activation")]
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] Transform PlayerCheck;
    [SerializeField] float PlayerCheckRadius;
    [SerializeField] bool isActive;

    [SerializeField] GameObject Player;
    [SerializeField] bool isAlive;

    ////////////////////////////// Animation Controlls //////////

    const string STILL = "Slime_Green_Still";
    const string IDLE = "Slime_Green_Idle";
    const string UP = "Slime_Green_Up";
    const string DOWN = "Slime_Green_Down";
    const string DEATH = "Slime_Green_Death";

    string currentState;

    ////////////////////////////// Declerations //////////

    Rigidbody2D rb2d;
    Animator anim;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        isAlive = true;
        nextJumpTime = 1;
    }

    void Update()
    {
        isActive = Physics2D.OverlapCircle(PlayerCheck.position, PlayerCheckRadius, PlayerLayer);

        if (isActive)
        {
            nextJumpTime -= Time.deltaTime;

            if (isAlive)
            {
                ChangeAnimationState(IDLE);

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
                    rb2d.velocity = new Vector2(velocityX, velocityY);
                }

                /*if (nextJumpTime < 0 && nextJumpTime > -0.1f)
                {
                    rb2d.velocity = new Vector2(velocityX, velocityY);
                    //nextJumpTime = Random.Range(1f, 3f);
                }*/

                /*if (Time.time > nextTime)
                {
                    rb2d.velocity = new Vector2(velocityX, velocityY);

                    nextTime = Time.time + Random.Range(2, 4);
                }

                else if (nextJumpTime > 0)
                {
                    rb2d.velocity = new Vector2(0, 0);
                }*/
                if (!isGrounded)
                {
                    if (rb2d.velocity.y > 0.1f)
                    {
                        ChangeAnimationState(UP);
                    }

                    else if (rb2d.velocity.y < -0.1f)
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
            rb2d.velocity = new Vector2(0, 0);
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
            other.CompareTag("Spikes"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Ground") ||
            other.CompareTag("Ground 2") ||
            other.CompareTag("Spikes"))
        {
            isGrounded = false;
            //nextJumpTime = Random.Range(1f, 3f);
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
