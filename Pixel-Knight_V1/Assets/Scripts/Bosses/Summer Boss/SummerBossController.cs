using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummerBossController : MonoBehaviour
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
    public bool isAlive;

    ////////////////////////////// Animation Controlls //////////

    const string STILL = "Giant_Slime_Still";
    const string IDLE = "Giant_Slime__Idle";
    const string UP = "Giant_Slime__Up";
    const string DOWN = "Giant_Slime__Down";
    const string DEATH = "Giant_Slime__Death";

    string currentState;

    ////////////////////////////// Declerations //////////

    Rigidbody2D rb2d;
    Animator anim;
    public static SummerBossController instance;

    private void Awake()
    {
        instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
                    rb2d.velocity = new Vector2(velocityX, rb2d.velocity.y);

                    if (Time.time > nextJumpTime)
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

            else
            {

            }
        }

        else
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (other.CompareTag("Wall"))
        {
            ChangeDirection();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void IsActiveCheck()
    {
        if (MovingWallSummerCastleController.instance.isInside)
        {
            isActive = true;
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
