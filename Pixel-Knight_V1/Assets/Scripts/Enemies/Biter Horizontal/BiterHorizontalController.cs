using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiterHorizontalController : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator anim;

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

    ////////////////////  GroundCheck  ///

    [Header("Ground Check")]
    [SerializeField] Transform RaycastPoint;
    [SerializeField] LayerMask GroundLayer;

    ////////////////////  Movement  ///

    [Header("Movement")]
    [SerializeField] float horizontalSpeed;
    [SerializeField] float verticalForce;
    [SerializeField] bool foundDirection;
    [SerializeField] GameObject Player;

    [SerializeField] bool isGrounded;

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
            ChangeAnimationState(BITING);

            if (!foundDirection)
            {
                if (Player.transform.position.x > transform.position.x)
                {
                    horizontalSpeed = -horizontalSpeed;
                }

                if (Player.transform.position.x < transform.position.x)
                {
                    horizontalSpeed = -horizontalSpeed;
                }

                foundDirection = true;
            }

            if (isGrounded)
            {
                rb2d.velocity = new Vector2(horizontalSpeed, verticalForce);
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
            characterScale.x = 1;
        }

        else if (rb2d.velocity.x > 0.1f)
        {
            characterScale.x = -1;
        }

        transform.localScale = characterScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
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
                horizontalSpeed = -horizontalSpeed;
                //ChangeAnimationState(BITING);
            }

            if (PlayerAnimationManager.instance.isFire)
            {
                horizontalSpeed = -horizontalSpeed;
                //ChangeAnimationState(BITING);
            }

            if (PlayerAnimationManager.instance.isIce)
            {
                horizontalSpeed = -horizontalSpeed;
                //ChangeAnimationState(BITING);
            }

            if (PlayerAnimationManager.instance.isBasic)
            {
                ChangeAnimationState(STILL);
            }
        }

        if(other.gameObject.tag == "Enemy")
        {
            horizontalSpeed = -horizontalSpeed;
        }

        if(other.gameObject.tag == "Wall")
        {
            horizontalSpeed = -horizontalSpeed;
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
