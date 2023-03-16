using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerController : MonoBehaviour
{
    ////////////////////////////// Movement //////////

    [SerializeField] float moveSpeed;
    public bool isCrouching;
    public float crouchTimerCountdown;
    [SerializeField] bool timerSet;
     
    ////////////////////////////// Activation //////////

    [Header("Activation")]
    [SerializeField] bool isActive;
    [SerializeField] bool foundDirection;

    [SerializeField] GameObject Player;

    ////////////////////////////// Animation //////////

    const string STILL = "Crawler_Still";
    const string WALK = "Crawler_Walk";
    const string CROUCH = "Crawler_Crouch";
    string currentState;

    [SerializeField] GameObject bulletDestroyAnimation;

    Animator anim;
    Rigidbody2D rb2d;
    //public static CrawlerController instance; 

    private void Awake()
    {
        //instance = this;
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
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
                    moveSpeed = -moveSpeed;
                }

                if (Player.transform.position.x < transform.position.x && transform.localScale.x == 1)
                {
                    moveSpeed = -moveSpeed;
                }

                foundDirection = true;
            }

            if (!isCrouching)
            {
                ChangeAnimationState(WALK);

                rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            }

            if(isCrouching)
            {
                ChangeAnimationState(CROUCH);
                rb2d.velocity = new Vector2(0f, 0f);
                crouchTimerCountdown -= Time.deltaTime;
            }

            if (crouchTimerCountdown <= 0)
            {
                isCrouching = false;
            }
        }

        else
        {
            ChangeAnimationState(STILL);

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

        if (other.CompareTag("Player Stomp Box"))
        {
            if (!PlayerController.instance.isGrounded)
            {
                PlayerController.instance.BounceOnEnemy();
                isCrouching = true;
                crouchTimerCountdown = 1f;
            }
        }

        if (other.CompareTag("Player Fire Magic") ||
            other.CompareTag("Player Ice Magic"))
        {
            Instantiate(bulletDestroyAnimation, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground") ||
            other.CompareTag("Ground 2"))
        {
            ChangeDirection();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (PlayerAnimationManager.instance.isBasic == true)
            {
                ChangeAnimationState(STILL);
            }

            else if (PlayerAnimationManager.instance.isArmor)
            {
                ChangeDirection();
            }

            else if (PlayerAnimationManager.instance.isFire)
            {
                ChangeDirection();
            }

            else if (PlayerAnimationManager.instance.isIce)
            {
                ChangeDirection();
            }
        }

        if (other.gameObject.tag == "Turn Around Trigger")
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        moveSpeed = -moveSpeed;
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
