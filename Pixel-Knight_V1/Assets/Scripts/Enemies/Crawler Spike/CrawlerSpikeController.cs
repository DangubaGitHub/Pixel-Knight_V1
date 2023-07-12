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
    [SerializeField] bool isActive;
    [SerializeField] bool foundDirection;

    [SerializeField] GameObject Player;

    //////////////////// Animation ///

    const string STILL = "Crawler_Spike_Still";
    const string WALK = "Crawler_Spike_Walk";
    const string DOWN = "Crawler_Spike_Crouch_Down";
    const string UP = "Crawler_Spike_Crouch_Up";

    string currentState;

    [SerializeField] GameObject bulletDestroyAnimation;

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
        if (isActive)
        {
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
            ChangeDirection();
        }

        if (other.CompareTag("Enemy"))
        {
            ChangeDirection();
        }

        if (other.CompareTag("Player Fire Magic") ||
            other.CompareTag("Player Ice Magic"))
        {
            Instantiate(bulletDestroyAnimation, other.transform.position, Quaternion.identity);
            AudioController.instance.PlaySFX(12);                                                         /////// SFX //
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
