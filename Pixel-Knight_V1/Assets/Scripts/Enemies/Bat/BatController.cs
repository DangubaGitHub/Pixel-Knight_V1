using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    ////////////////////////////// Movement //////////

    [Header("Movement")]
    [SerializeField] float velocityX;
    [SerializeField] float velocityY;

    [SerializeField] Transform topEdge;
    [SerializeField] Transform bottomEdge;

    [SerializeField] bool foundDirection;

    ////////////////////////////// Activation //////////

    [Header("Activation")]
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] Transform PlayerCheck;
    [SerializeField] float PlayerCheckRadius;
    [SerializeField] bool isActive;

    [SerializeField] GameObject Player;

    ////////////////////////////// Animation Controlls //////////

    const string STILL = "Bat_Still";
    const string MOVE = "Bat_Move";

    string currentState;

    ////////////////////////////// Declerations //////////

    Rigidbody2D rb2d;
    Animator anim;
    public static BatController instance;

    private void Awake()
    {
        instance = this;
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
            ChangeAnimationState(MOVE);

            if (!foundDirection)
            {
                if (Player.transform.position.x > transform.position.x && transform.localScale.x == -1)
                {
                    ChangeHorizontalDirection();
                }

                if (Player.transform.position.x < transform.position.x && transform.localScale.x == 1)
                {
                    ChangeHorizontalDirection();
                }

                foundDirection = true;
            }

            rb2d.velocity = new Vector2(velocityX, velocityY);

            if (transform.position.y > transform.position.y + 10)
            {
                ChangeVerticalDirection();
            }

            if (transform.position.y < transform.position.y - 10)
            {
                ChangeVerticalDirection();
            }

            /*if (transform.position.y > topEdge.transform.position.y)
            {
                ChangeVerticalDirection();
            }

            if (transform.position.y < bottomEdge.transform.position.y)
            {
                ChangeVerticalDirection();
            }*/
        }

        else
        {
            ChangeAnimationState(STILL);
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

    void ChangeHorizontalDirection()
    {
        velocityX = -velocityX;
    }

    void ChangeVerticalDirection()
    {
        velocityY = -velocityY;
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
