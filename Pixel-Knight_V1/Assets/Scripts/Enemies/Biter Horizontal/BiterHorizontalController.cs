using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiterHorizontalController : MonoBehaviour
{
    //public static BiterHorizontalController instance;
    Rigidbody2D rb2d;
    Animator anim;

    [SerializeField] bool enteredTrigger;

    ////////////////////  ANIMATIONS  ///

    const string STILL = "Biter_Horizontal_Still";
    const string BITING = "Biter_Horizontal_Biting";

    string currentState;

    ////////////////////  Activation  ///

    [Header("Activation")]
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] Transform PlayerCheck;
    [SerializeField] float PlayerCheckRadius;
    [SerializeField] bool isActive;

    ////////////////////  Movement  ///

    [Header("Movement")]
    public float horizontalVelocity;
    [SerializeField] float verticalForce;
    [SerializeField] bool foundDirection;
    [SerializeField] GameObject Player;

    [SerializeField] bool isGrounded;

    private void Awake()
    {
        //instance = this;
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
            ChangeAnimationState(BITING);

            if (!foundDirection)
            {
                if (Player.transform.position.x > transform.position.x && transform.localScale.x == -1)
                {
                    horizontalVelocity = -horizontalVelocity;
                }

                else if (Player.transform.position.x < transform.position.x)
                {
                    horizontalVelocity = -horizontalVelocity;
                }

                foundDirection = true;
            }

            if (isGrounded)
            {
                rb2d.velocity = new Vector2(horizontalVelocity, verticalForce);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground") ||
            other.CompareTag("Ground 2") ||
            other.CompareTag("Spikes"))
        {
            isGrounded = true;
        }

        if(other.CompareTag("Turn Around Trigger"))
        {
            horizontalVelocity = -horizontalVelocity;
            enteredTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Ground") ||
            other.CompareTag("Ground 2") ||
            other.CompareTag("Spikes"))
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (PlayerAnimationManager.instance.isArmor)
            {
                horizontalVelocity = -horizontalVelocity;
            }

            if (PlayerAnimationManager.instance.isFire)
            {
                horizontalVelocity = -horizontalVelocity;
            }

            if (PlayerAnimationManager.instance.isIce)
            {
                horizontalVelocity = -horizontalVelocity;
            }

            if (PlayerAnimationManager.instance.isBasic)
            {
                ChangeAnimationState(STILL);
            }
        }

        if(other.gameObject.tag == "Enemy")
        {
            horizontalVelocity = -horizontalVelocity;
        }

        if(other.gameObject.tag == "Wall")
        {
            horizontalVelocity = -horizontalVelocity;
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
