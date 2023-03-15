using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    //Collider2D capsuleCollider2d;

    [Header("Movement")]
    public float speedX;
    float moveX;

    [Header("Jumping")]
    [SerializeField] float jumpSpeed;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;
    
    public bool isGrounded;
    
    float jumpTimeCounter;
    [SerializeField] float jumpTime;
    bool isJumping;

    [SerializeField] bool touchesCeiling;

    public float mushroomBounceForce;

    [Header("Magic")]
    public Transform firePoint;
    [SerializeField] GameObject fireBulletPrefab;
    [SerializeField] float fireSpeed;
    [SerializeField] GameObject iceBulletPrefab;
    [SerializeField] float iceSpeed;
    public bool isFireAttacking;
    public bool isIceAttacking;
    public bool isFireAirAttacking;
    public bool isIceAirAttacking;

    ////////////////////////////////////////////////// Fire Magic ////////////

    float MagicTime;
    [SerializeField] float MagicTimer;


    [Header("Extra's")]
    public float bounceForce;
    
    [SerializeField] float knockBackLength;
    [SerializeField] float knockBackForce;
    float knockBackCounter;

    public bool lookingRight;
    public bool lookingLeft;

    public bool isDead;

    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        //capsuleCollider2d = GetComponent<Collider2D>();
    }

    void Start()
    {
        isDead = false;

        MagicTime = 1.5f;
        MagicTimer = MagicTime;
    }

    void Update()
    {
        MagicTimer -= Time.deltaTime;

        if (!isDead)
        {
            if (knockBackCounter <= 0)
            {

                moveX = Input.GetAxis("Horizontal");

                isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

                if (Input.GetButtonDown("Jump") && isGrounded && !touchesCeiling)
                {
                    isJumping = true;
                    jumpTimeCounter = jumpTime;
                    rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
                }

                if (Input.GetButton("Jump") && isJumping == true && !touchesCeiling)
                {
                    if (jumpTimeCounter > 0)
                    {
                        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
                        jumpTimeCounter -= Time.deltaTime;
                    }

                    else
                    {
                        isJumping = false;
                    }
                }

                if (Input.GetButtonUp("Jump"))
                {
                    isJumping = false;
                }



                if (PlayerAnimationManager.instance.isFire)
                {
                    if (MagicTimer < 0)
                    {
                        if (Input.GetButtonDown("Fire"))
                        {
                            if (!isFireAttacking && isGrounded)
                            {
                                isFireAttacking = true;
                                FireMagic();
                                Invoke("MagicComplete", 0.4f);
                                ResetMagicTimer();
                            }

                            else if (!isFireAirAttacking && !isGrounded)
                            {
                                isFireAirAttacking = true;
                                FireMagic();
                                Invoke("MagicComplete", 0.4f);
                                ResetMagicTimer();
                            }
                        }
                    }
                }

                if (PlayerAnimationManager.instance.isIce)
                {
                    if (MagicTimer < 0)
                    {
                        if (Input.GetButtonDown("Fire"))
                        {
                            if (!isIceAttacking && isGrounded)
                            {
                                isIceAttacking = true;

                                IceMagic();
                                Invoke("MagicComplete", 0.4f);
                                ResetMagicTimer();
                            }

                            else if (!isIceAirAttacking && !isGrounded)
                            {
                                isIceAirAttacking = true;
                                IceMagic();
                                Invoke("MagicComplete", 0.4f);
                                ResetMagicTimer();
                            }
                        }
                    }
                }
            }

            else if (knockBackCounter > 0)
            {
                knockBackCounter -= Time.deltaTime;

                if (lookingRight == true && lookingRight == false)
                {
                    rb2d.velocity = new Vector2(knockBackForce, rb2d.velocity.y);
                }

                if (lookingLeft == true && lookingRight == false)
                {
                    rb2d.velocity = new Vector2(knockBackForce, rb2d.velocity.y);
                }
            }
        }

        
    }

    void FixedUpdate()
    {
        if (knockBackCounter <= 0)
        {
            rb2d.velocity = new Vector2(moveX * speedX * Time.deltaTime, rb2d.velocity.y);
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ceiling")
        {
            //rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            touchesCeiling = true;
            jumpTimeCounter = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ceiling")
        {
            touchesCeiling = false;
        }
    }

    void ResetMagicTimer()
    {
        MagicTimer = MagicTime;
    }

    void FireMagic()
    {
            Instantiate(fireBulletPrefab, firePoint.position, firePoint.rotation);
    }

    void IceMagic()
    {
        Instantiate(iceBulletPrefab, firePoint.position, firePoint.rotation);
    }

    void MagicComplete()
    {
        isFireAttacking = false;
        isIceAttacking = false;
        isFireAirAttacking = false;
        isIceAirAttacking = false;
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        rb2d.velocity = new Vector2(0, knockBackForce);
    }

    public void BounceOnEnemy()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, bounceForce);
    }

    public void BounceOnMushroom()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, mushroomBounceForce);
    }
}
