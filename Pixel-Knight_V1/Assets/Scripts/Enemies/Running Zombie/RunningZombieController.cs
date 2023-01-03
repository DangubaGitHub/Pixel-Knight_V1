using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningZombieController : MonoBehaviour
{
    ////////////////////////////// Movement //////////

    [SerializeField] float moveSpeed;
    [SerializeField] GameObject enemyDeathEffect;
    [SerializeField] bool touchesWall;
    [SerializeField] BoxCollider2D boxCollider;

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
        boxCollider = GetComponent<BoxCollider2D>();
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
                    moveSpeed = -moveSpeed;
                }

                if (Player.transform.position.x < transform.position.x && transform.localScale.x == 1)
                {
                    moveSpeed = -moveSpeed;
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
             moveSpeed = -moveSpeed;
        }

        if (other.CompareTag("Enemy"))
        {
             moveSpeed = -moveSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
        {
            moveSpeed = -moveSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(PlayerAnimationManager.instance.isArmor)
            {
                moveSpeed = -moveSpeed;
            }

            if(PlayerAnimationManager.instance.isFire)
            {
                moveSpeed = -moveSpeed;
            }

            if(PlayerAnimationManager.instance.isIce)
            {
                moveSpeed = -moveSpeed;
            }

            if (PlayerAnimationManager.instance.isBasic == true)
            {
                ChangeAnimationState(ZOMBIE_STILL);
            }
        }
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
