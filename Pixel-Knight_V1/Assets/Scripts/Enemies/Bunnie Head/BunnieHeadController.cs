using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnieHeadController : MonoBehaviour
{
    ////////////////////////////// Jump //////////

    [SerializeField] float velocityY;
    [SerializeField] bool isGrounded;
    [SerializeField] int nextJump;
    float nextTime;
    bool touchedGround;

    ////////////////////////////// Activation //////////

    [Header("Activation")]
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] Transform PlayerCheck;
    [SerializeField] float PlayerCheckRadius;
    [SerializeField] bool isActive;

    [SerializeField] GameObject Player;

    ////////////////////////////// Animation Controlls //////////

    const string STILL = "Bunnie_Head_Still";
    const string IDLE = "Bunnie_Head_Idle";
    const string UP = "Bunnie_Head_Up";
    const string DOWN = "Bunnie_Head_Down";

    string currentState;

    ////////////////////////////// Declerations //////////

    Rigidbody2D rb2d;
    Animator anim;
    public static BunnieHeadController instance;

    private void Awake()
    {
        instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        rb2d.gravityScale = 0;
    }

    void Update()
    {
        isActive = Physics2D.OverlapCircle(PlayerCheck.position, PlayerCheckRadius, PlayerLayer);

        if (isActive)
        {
            if (isGrounded)
            {
                ChangeAnimationState(IDLE);
            }

            else if(rb2d.velocity.y > 0)
            {
                ChangeAnimationState(UP);
            }

            else if (rb2d.velocity.y <= 0)
            {
                ChangeAnimationState(DOWN);
                rb2d.drag = 2;
                rb2d.gravityScale = 2;
            }

            if (touchedGround == true)
            {
                if (Time.time > nextTime)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, velocityY);

                    nextTime = Time.time + nextJump;
                }
            }
        }

        else
        {
            ChangeAnimationState(STILL);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" ||
           other.gameObject.tag == "Ground 2")
        {
            isGrounded = true;
            touchedGround = true;
            rb2d.gravityScale = 9;
            rb2d.drag = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" ||
           other.gameObject.tag == "Ground 2")
        {
            isGrounded = false;
            touchedGround = false;
        }
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
