using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerController : MonoBehaviour
{
    ////////////////////////////// Movement //////////

    [SerializeField] float moveSpeed;


    ////////////////////////////// Activation //////////

    [Header("Activation")]
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] Transform PlayerCheck;
    [SerializeField] float PlayerCheckRadius;
    [SerializeField] bool isActive;
    [SerializeField] bool foundDirection;

    [SerializeField] GameObject Player;

    ////////////////////////////// Animation //////////

    const string STILL = "Crawler_Still";
    const string WALK = "Crawler_Walk";
    string currentState;

    Animator anim;
    Rigidbody2D rb2d;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        isActive = Physics2D.OverlapCircle(PlayerCheck.position, PlayerCheckRadius, PlayerLayer);

        if (isActive)
        {
            ChangeAnimationState(WALK);

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
            if (PlayerAnimationManager.instance.isArmor)
            {
                moveSpeed = -moveSpeed;
            }

            if (PlayerAnimationManager.instance.isFire)
            {
                moveSpeed = -moveSpeed;
            }

            if (PlayerAnimationManager.instance.isIce)
            {
                moveSpeed = -moveSpeed;
            }

            if (PlayerAnimationManager.instance.isBasic == true)
            {
                ChangeAnimationState(STILL);
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
