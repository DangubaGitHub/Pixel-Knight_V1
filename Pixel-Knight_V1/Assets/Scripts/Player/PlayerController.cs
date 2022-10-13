using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;

    [Header("Movement")]
    public float speedX;
    float moveX;

    [Header("Jumping")]
    [SerializeField] float jumpSpeed;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;
    public bool isGrounded;

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

    [Header("Extra's")]
    public float bounceForce;

    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
        }

        if (PlayerAnimationManager.instance.isFire)
        {
            if (Input.GetButtonDown("Fire"))
            {
                if (!isFireAttacking && isGrounded)
                {
                    isFireAttacking = true;
   
                    FireMagic();

                    Invoke("MagicComplete", 0.4f);
                }

                else if(!isFireAirAttacking && !isGrounded)
                {
                    isFireAirAttacking = true;

                    FireMagic();

                    Invoke("MagicComplete", 0.4f);
                }
            }
        }

        if(PlayerAnimationManager.instance.isIce)
        {
            if(Input.GetButtonDown("Fire"))
            {
                if (!isIceAttacking && isGrounded)
                {
                    isIceAttacking = true;

                    IceMagic();

                    Invoke("MagicComplete", 0.4f);
                }

                else if(!isIceAirAttacking && !isGrounded)
                {
                    isIceAirAttacking = true;

                    IceMagic();

                    Invoke("MagicComplete", 0.4f);
                }
            }
        }
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(moveX * speedX * Time.deltaTime, rb2d.velocity.y);
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

    public void BounceOnEnemy()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, bounceForce);
    }
}
