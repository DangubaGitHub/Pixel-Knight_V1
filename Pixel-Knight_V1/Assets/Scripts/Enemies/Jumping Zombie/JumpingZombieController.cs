using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingZombieController : MonoBehaviour
{
    ////////////////////  MOVEMENT  ///

    [SerializeField] int nextJump;
    float nextTime;
    int jumpForce = 40;
    
    ////////////////////  ANIMATIONS  ///

    const string IDLE = "Jumping_Zombie_Idle";
    const string JUMP = "Jumping_Zombie_Jump";
    const string FALL = "Jumping_Zombie_Fall";
    const string STILL = "Jumping_Zombie_Still";

    string currentState;

    ////////////////////  THE REST  ///

    [SerializeField] bool isGrounded;

    [SerializeField] GameObject enemyDeathEffect;

    [SerializeField] GameObject Player;

    ////////////////////  Activation  ///

    [Header("Activation")]
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] Transform PlayerCheck;
    [SerializeField] float PlayerCheckRadius;
    [SerializeField] bool isActive;

    Rigidbody2D rb2d;
    Animator anim;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        isActive = Physics2D.OverlapCircle(PlayerCheck.position, PlayerCheckRadius, PlayerLayer);

        if (isActive)
        {
            ////////////////////  ANIMATIONS  ///

            if (isGrounded)
            {
                ChangeAnimationState(IDLE);
            }

            if (!isGrounded)
            {
                if (rb2d.velocity.y >= 0)
                {
                    ChangeAnimationState(JUMP);
                }

                else if (rb2d.velocity.y < 0)
                {
                    ChangeAnimationState(FALL);
                }
            }

            ////////////////////  ENEMY LOOK DIRECTION AND FLIPPING  ///

            Vector3 scale = transform.localScale;

            if (Player.transform.position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x);
            }

            else
            {
                scale.x = Mathf.Abs(scale.x) * -1;
            }

            transform.localScale = scale;

            ////////////////////  NEXT JUMP DELAY  ///

            if (Time.time > nextTime)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);

                nextTime = Time.time + nextJump;
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
            other.gameObject.tag == "Grounded 2")
        {
            isGrounded = true;
        }

        if (other.gameObject.tag == "Player")
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground" ||
            other.gameObject.tag == "Ground 2")
        {
            isGrounded = false;
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
