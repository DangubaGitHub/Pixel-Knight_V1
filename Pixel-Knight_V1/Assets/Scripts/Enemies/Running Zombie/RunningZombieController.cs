using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningZombieController : MonoBehaviour
{
    ////////////////////////////// Movement //////////

    [SerializeField] float moveSpeed;
    //[SerializeField] GameObject enemyDeathEffect;
    //[SerializeField] bool touchesWall;

    ////////////////////////////// Activation //////////

    [Header("Activation")]
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] Transform PlayerCheck;
    [SerializeField] float PlayerCheckRadius;
    [SerializeField] bool isActive;
    [SerializeField] bool foundDirection;

    [SerializeField] GameObject Player;

    ////////////////////////////// Animation Controlls //////////

    const string ZOMBIE_STILL = "Zombie_Still";
    const string ZOMBIE_RUN = "Zombie_Run";
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
        
    }

    void Update()
    {
        isActive = Physics2D.OverlapCircle(PlayerCheck.position, PlayerCheckRadius, PlayerLayer);

        if (isActive)
        {
            ChangeAnimationState(ZOMBIE_RUN);

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

            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
        }

        else
        {
            ChangeAnimationState(ZOMBIE_STILL);

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
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            ChangeDirection();
        }

        if (other.CompareTag("Enemy"))
        {
            ChangeDirection();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Ground") ||
            other.CompareTag("Ground 2"))
        {
            ChangeDirection();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(PlayerAnimationManager.instance.isArmor)
            {
                ChangeDirection();
            }

            if(PlayerAnimationManager.instance.isFire)
            {
                ChangeDirection();
            }

            if(PlayerAnimationManager.instance.isIce)
            {
                ChangeDirection();
            }

            if (PlayerAnimationManager.instance.isBasic == true)
            {
                ChangeAnimationState(ZOMBIE_STILL);
            }
        }
    }

    void ChangeDirection()
    {
        moveSpeed = -moveSpeed;
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
