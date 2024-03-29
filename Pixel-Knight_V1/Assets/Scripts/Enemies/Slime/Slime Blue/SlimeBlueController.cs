using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBlueController : MonoBehaviour
{
    ////////////////////////////// Movement //////////

    [Header("Movement")]
    public float velocityX;
    public float velocityY;
    [SerializeField] bool foundDirection;
    public bool isGrounded;
    public bool elementActive;

    [SerializeField] float timeBetweenJumps;
    float nextJumpTime;
    [SerializeField] bool nearWall;

    ////////////////////////////// Attack //////////

    [Header("Attack")]
    [SerializeField] GameObject elementSpikePrefab;
    [SerializeField] Transform firePoint;

    ////////////////////////////// Activation //////////

    [Header("Activation")]
    [SerializeField] bool isActive;

    [SerializeField] GameObject Player;
    public bool isAlive;

    ////////////////////////////// Animation Controlls //////////

    const string STILL = "Slime_Blue_Still";
    const string ELEMENT = "Slime_Blue_Element";
    const string UP = "Slime_Blue_Up";
    const string DOWN = "Slime_Blue_Down";
    const string DEATH = "Slime_Blue_Death";

    string currentState;

    [SerializeField] GameObject enemyDeathEffect;
    [SerializeField] GameObject bulletDestroyAnimation;

    ////////////////////////////// Declerations //////////

    Rigidbody2D rb2d;
    Animator anim;
    //public static SlimeBlueController instance;

    private void Awake()
    {
        //instance = this;
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

                    if (!nearWall)
                    {
                        ChangeAnimationState(ELEMENT);
                    }
                    //rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                    

                    

                    /*if(!elementActive)
                    {
                        rb2d.velocity = new Vector2(velocityX, velocityY);
                    }*/
                    if(nearWall)
                    {
                        rb2d.velocity = new Vector2(velocityX, velocityY);
                    }


                    if(Time.time > nextJumpTime)
                    {
                        rb2d.velocity = new Vector2(velocityX, velocityY);
                        nextJumpTime = Time.time + timeBetweenJumps;
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

            else if (!isAlive && isGrounded)
            {
                ChangeAnimationState(DEATH);
            }
        }

        else
        {
            ChangeAnimationState(STILL);
            foundDirection = false;
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
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
        if (other.CompareTag("Wall"))
        {
            nearWall = true;
        }

        if (other.CompareTag("Player Stomp Box"))
        {
            if (!isGrounded)
            {
                PlayerController.instance.BounceOnEnemy();

                Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);

                AudioController.instance.PlaySFX(5);                                             /////// SFX //

                Destroy(gameObject);
            }

            else if (isGrounded)
            {
                isAlive = false;
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
                PlayerController.instance.BounceOnEnemy();
                Destroy(gameObject, 1.4f);
            }
        }

        if (other.CompareTag("Player Fire Magic"))
        {
            Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            AudioController.instance.PlaySFX(5);                                             /////// SFX //
            Destroy(gameObject);
        }

        if (other.CompareTag("Player Ice Magic"))
        {
            Instantiate(bulletDestroyAnimation, other.transform.position, Quaternion.identity);
            AudioController.instance.PlaySFX(12);                                                         /////// SFX //
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            nearWall = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            rb2d.velocity = new Vector2(0, 0);//////////
        }

        if (other.gameObject.tag == "Wall" ||
            other.gameObject.tag == "Turn Around Trigger")
        {
            ChangeDirection();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Spikes")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    void ChangeDirection()
    {
        velocityX = -velocityX;
    }

    public void ElementActive()
    {
        elementActive = true;
    }

    public void ElementInactive()
    {
        elementActive = false;
    }

    public void ElementSpikes()
    {
        Instantiate(elementSpikePrefab, firePoint.position, Quaternion.identity);
    }

    public void Jump()
    {
        rb2d.velocity = new Vector2(velocityX, velocityY);
    }

    private void OnBecameVisible()
    {
        isActive = true;
    }

    private void OnBecameInvisible()
    {
        isActive = false;
    }

    public void GroundDeathSound1()
    {
        AudioController.instance.PlaySFX(26);                                             /////// SFX //
    }

    public void GroundDeathSound2()
    {
        AudioController.instance.PlaySFX(5);                                             /////// SFX //
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
