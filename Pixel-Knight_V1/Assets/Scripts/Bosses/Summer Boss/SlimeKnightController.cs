using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKnightController : MonoBehaviour
{
    [Header("Activation")]
    public bool isActive;
    public bool isAlive;
    bool isArmor;
    bool isNoArmor;

    //////////////////////////////////////////////////////////// Movement //////////


    [Header("Movement")]
    public float armorVelocityX;
    public float armorVelocityY;

    public float noArmorVelocityX;
    public float noArmorJumpVelocityX;
    public float noArmorJumpVelocityY;
    

    public bool isGrounded;

    float nextArmorJumpTime;
    [SerializeField] float timeBetweenArmorjumps;

    float nextNoArmorJump;
    [SerializeField] float timeBetweenNoArmorJumps;

    [SerializeField] Transform playerTransform;

    //////////////////////////////////////////////////////////// Declerations //////////

    Rigidbody2D rb2d;
    public static SlimeKnightController instance;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        instance = this;
    }

    void Start()
    {
        //isGrounded = true;
        isAlive = true;
        isArmor = true;
        armorVelocityX = -armorVelocityX;
    }

    void Update()
    {
        if (isActive)
        {
            if (isAlive)
            {
                if (isArmor)
                {
                    if (isGrounded)
                    {
                        rb2d.velocity = new Vector2(0, rb2d.velocity.y);

                        if (Time.time > nextArmorJumpTime)
                        {
                            rb2d.velocity = new Vector2(rb2d.velocity.x, armorVelocityY);
                            nextArmorJumpTime = Time.time + timeBetweenArmorjumps;
                        }
                    }

                    if (!isGrounded)
                    {
                        rb2d.velocity = new Vector2(armorVelocityX, rb2d.velocity.y);
                    }
                }

                else if (isNoArmor)
                {
                    if (isGrounded)
                    {
                        rb2d.velocity = new Vector2(noArmorVelocityX, rb2d.velocity.y);

                        if (Time.time > nextNoArmorJump)
                        {
                            rb2d.velocity = new Vector2(rb2d.velocity.x, noArmorJumpVelocityY);
                            nextNoArmorJump = Time.time + timeBetweenNoArmorJumps;
                        }
                    }

                    else if (!isGrounded)
                    {
                        rb2d.velocity = new Vector2(noArmorJumpVelocityX, noArmorJumpVelocityY);
                    }
                }
            }

            else if (!isAlive)
            {
                rb2d.velocity = new Vector2(0, 0);
            }
        }

        else
        {
            rb2d.velocity = new Vector2(0, 0);
        }
    }

    void ChangeDirections()
    {
        armorVelocityX = -armorVelocityX;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

        if (other.gameObject.tag == "Wall")
        {
            ChangeDirections();
        }

        if (other.gameObject.tag == "Player")
        {
            if (transform.position.y > playerTransform.position.y)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 30f);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (other.CompareTag("Player"))
        {
            if (transform.position.y > playerTransform.position.y)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 30f);
            }

            if (PlayerAnimationManager.instance.isBasic && PlayerHealthController.instance.invincibleLength <= 0)
            {
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            }

            else
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 30f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    */
}
