using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator anim;
    SpriteRenderer sr;

    string currentState;
    const string DESTROY = "fireball_destroy";

    [SerializeField] float VelocityX = 15;
    [SerializeField] float VelocityY = 3;

    bool isGrounded;

    [SerializeField] GameObject enemyDeathEffect;
    [SerializeField] GameObject bulletDestroyAnimation;

    BombWormController bombWormController;
    [SerializeField] GameObject BombWorm;

    private void Awake()
    {

        bombWormController = BombWorm.GetComponent<BombWormController>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        rb2d.velocity = transform.right * VelocityX;
    }

    void Update()
    {
        if (isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, VelocityY);
        }

    }

    ////////////////////////////////////////////////////////////////////// Collision //////////
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" ||
            other.gameObject.tag == "Ground 2" ||
            other.gameObject.tag == "Spikes")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" ||
            other.gameObject.tag == "Ground 2" ||
            other.gameObject.tag == "Spikes")
        {
            isGrounded = false;
        }
    }

    ////////////////////////////////////////////////////////////////////// Trigger //////////

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.CompareTag("Summer Boss"))
        {
            /*if (SlimeKnightAnimationController.instance.zero_Hit)
            {
                //rb2d.velocity = new Vector2(0, 0);
                Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
                Destroy(gameObject);
                //sr.enabled = false;
                //rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
                //Invoke("DestroyBall", 1f);
            }

            if (SlimeKnightAnimationController.instance.one_Hit)
            {
                //rb2d.velocity = new Vector2(0, 0);
                Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
                Destroy(gameObject);
                //sr.enabled = false;
                //rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
                //Invoke("DestroyBall", 1f);
            }     
            if (SlimeKnightAnimationController.instance.three_Hit)
            {
                rb2d.velocity = new Vector2(0, 0);
                Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
                sr.enabled = false;
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
                Invoke("DestroyBall", 1f);
            }

            if (SlimeKnightAnimationController.instance.hurt)
            {
                //rb2d.velocity = new Vector2(0, 0);
                Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
                Destroy(gameObject);
                //sr.enabled = false;
                //rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
                //Invoke("DestroyBall", 1f);
            }*/
        }
    }
 
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
