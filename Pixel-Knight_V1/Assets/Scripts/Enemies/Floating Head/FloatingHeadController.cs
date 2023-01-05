using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHeadController : MonoBehaviour
{
    ////////////////////////////// Movement //////////

    public float velocity;
    [SerializeField] bool foundDirection;

    ////////////////////////////// Activation //////////

    [Header("Activation")]
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] Transform PlayerCheck;
    [SerializeField] float PlayerCheckRadius;
    [SerializeField] bool isActive;

    [SerializeField] GameObject Player;

    ////////////////////////////// Animation Controlls //////////

    const string STILL = "Floating_Head_Still";
    const string MOVE = "Floating_Head_Move";

    string currentState;

    ////////////////////////////// Declerations //////////

    Rigidbody2D rb2d;
    Animator anim;
    public static FloatingHeadController instance;

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
                    velocity = -velocity;
                }

                if (Player.transform.position.x < transform.position.x && transform.localScale.x == 1)
                {
                    velocity = -velocity;
                }

                foundDirection = true;
            }

            rb2d.velocity = new Vector2(velocity, rb2d.velocity.y);
        }

        else
        {
            ChangeAnimationState(STILL);
        }

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



    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
