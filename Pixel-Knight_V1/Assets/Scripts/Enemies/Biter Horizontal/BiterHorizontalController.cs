using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiterHorizontalController : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator anim;

    ////////////////////  ANIMATIONS  ///

    const string BITING = "Biter_Horizontal_Still";
    const string STILL = "Biter_Horizontal_Biting";

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

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        ChangeAnimationState(STILL);
    }

    void Update()
    {
        isActive = Physics2D.OverlapCircle(PlayerCheck.position, PlayerCheckRadius, PlayerLayer);

        Debug.DrawRay(RaycastPoint.position, Vector2.down * .5f, Color.red);

        if(isActive)
        {
            if (GroundCheck())
            {
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

    bool GroundCheck()
    {
        RaycastHit2D Grounded;

        Grounded = Physics2D.Raycast(RaycastPoint.position, Vector2.down, .5f, GroundLayer);

        return Grounded;
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
