using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerSpikeController : MonoBehaviour
{
    //////////////////// Movement ///

    [SerializeField] float moveSpeed;
    [SerializeField] Transform player;
    [SerializeField] float crouchDistance;
    [SerializeField] bool isCrouching;

    //////////////////// Activation ///

    [Header("Activation")]
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] Transform PlayerCheck;
    [SerializeField] float PlayerCheckRadius;
    [SerializeField] bool isActive;
    [SerializeField] bool foundDirection;

    [SerializeField] GameObject Player;

    //////////////////// Animation ///

    const string STILL = "Crawler_Spike_Still";
    const string WALK = "Crawler_Spike_Walk";
    const string DOWN = "Crawler_Spike_Crouch_Down";
    const string UP = "Crown_Spike_Crouch_Up";

    string currentState;

    //////////////////// Declerations ///

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

            if (Vector2.Distance(transform.position, player.position) < crouchDistance)
            {
                ChangeAnimationState(DOWN);
                rb2d.velocity = new Vector2(0f, 0f);
                isCrouching = true;
            }


            if (isCrouching)
            {
            if (Vector2.Distance(transform.position, player.position) > crouchDistance && isCrouching == true)
                {
                    ChangeAnimationState(UP);
                    isCrouching = false;
                }
            }
        }

        else
        {
            ChangeAnimationState(STILL);

            rb2d.velocity = new Vector2(0f, 0f);

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
        if (other.CompareTag("Ground"))
        {
            moveSpeed = -moveSpeed;
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
                moveSpeed = -moveSpeed;
            }

            else if (PlayerAnimationManager.instance.isFire)
            {
                moveSpeed = -moveSpeed;
            }

            else if (PlayerAnimationManager.instance.isIce)
            {
                moveSpeed = -moveSpeed;
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
