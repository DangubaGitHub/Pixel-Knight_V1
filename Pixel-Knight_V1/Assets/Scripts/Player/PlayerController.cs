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

    public Transform firePoint;

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

        
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(moveX * speedX * Time.deltaTime, rb2d.velocity.y);
    }
}
