using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePurpleController : MonoBehaviour
{
    ////////////////////////////// Movement //////////

    [Header("Movement")]
    public float velocityX;
    [SerializeField] bool foundDirection;
    public bool isGrounded;
    [SerializeField] bool elementActive;

    //float timeBetweenJumps = 2f;
    [SerializeField] float nextElementBurst;

    ////////////////////////////// Element Burst //////////

    [Header("Element Burst")]
    [SerializeField] GameObject elementBurstPrefab;
    [SerializeField] Transform firePoint;

    ////////////////////////////// Activation //////////

    [Header("Activation")]
    [SerializeField] bool isActive;

    [SerializeField] GameObject Player;
    public bool isAlive;

    ////////////////////////////// Animation Controlls //////////

    const string STILL = "Slime_Purple_Still";
    const string MOVE = "Slime_Purple_Move";
    const string ELEMENT = "Slime_Purple_Element";
    const string UP = "Slime_Purple_Up";
    const string DOWN = "Slime_Purple_Down";
    const string DEATH = "Slime_Purple_Death";

    string currentState;

    ////////////////////////////// Declerations //////////

    Rigidbody2D rb2d;
    Animator anim;
    public static SlimePurpleController instance;

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

                if (!elementActive)
                {
                    ChangeAnimationState(MOVE);
                    rb2d.velocity = new Vector2(velocityX, 0);
                }

                else if (elementActive)
                {
                    ChangeAnimationState(ELEMENT);
                    rb2d.velocity = new Vector2(0, 0);
                    Invoke("EndElement", 1.775f);
                }


                if (rb2d.velocity.y > 0)
                {
                    ChangeAnimationState(UP);
                }

                else if (rb2d.velocity.y < 0)
                {
                    ChangeAnimationState(DOWN);
                }

                

                if (Time.time > nextElementBurst)
                {
                    elementActive = true;
                    nextElementBurst = Time.time + Random.Range(1, 3);
                }
            }

            else if (!isAlive && isGrounded)
            {
                ChangeAnimationState(DEATH);
                rb2d.velocity = new Vector2(0, 0);
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
        if (other.CompareTag("Ground") ||
            other.CompareTag("Spikes") ||
            other.CompareTag("Enemy"))
        {
            isGrounded = true;
        }

        if (other.CompareTag("Ground 2"))
        {
            if (rb2d.velocity.y <= 0)
            {
                isGrounded = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Spikes"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground") ||
            other.CompareTag("Ground 2") ||
            other.CompareTag("Spikes") ||
            other.CompareTag("Enemy"))
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall" ||
            other.gameObject.tag == "Turn Around Trigger")
        {
            ChangeDirection();
        }
    }

    void EndElement()
    {
        elementActive = false;
    }

    public void ElementBurst()
    {
        Instantiate(elementBurstPrefab, firePoint.position, Quaternion.identity);
    }

    void ChangeDirection()
    {
        velocityX = -velocityX;
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
