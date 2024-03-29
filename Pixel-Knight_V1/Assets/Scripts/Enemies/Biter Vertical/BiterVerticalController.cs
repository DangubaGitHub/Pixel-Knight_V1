using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiterVerticalController : MonoBehaviour
{
    ////////////////////  ANIMATIONS  ///

    const string STILL = "Biter_Vertical_Still";
    const string UP = "Biter_Vertical_Up";
    const string NEUTRAL = "Biter_Vertical_Neutral";
    const string DOWN = "Biter_Vertical_Down";

    string currentState;

    [SerializeField] GameObject enemyDeathEffect;

    ////////////////////  Movement  ///

    [SerializeField] float verticalForce;


    Animator anim;
    Rigidbody2D rb2d;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb2d.velocity = new Vector2(0f, verticalForce);
    }

    void Update()
    {
        if(rb2d.velocity.y > 0)
        {
            //gameObject.tag = "Enemy Invulnerable Damaging";
            ChangeAnimationState(UP);
        }

        if(rb2d.velocity.y == 0)
        {
            //gameObject.tag = "Enemy";
            ChangeAnimationState(NEUTRAL);
        }

        if(rb2d.velocity.y < 0)
        {
            //gameObject.tag = "Enemy";
            ChangeAnimationState(DOWN);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player Stomp Box"))
        {
            if (!PlayerController.instance.isGrounded)
            {
                if (rb2d.velocity.y <= 0)
                {
                    PlayerController.instance.BounceOnEnemy();

                    Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);

                    AudioController.instance.PlaySFX(5);                                             /////// SFX // 

                    Destroy(gameObject);
                }
            }
        }

        if (other.CompareTag("Player Fire Magic") ||
            other.CompareTag("Player Ice Magic"))
        {
            Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
            Destroy(other.gameObject);

            AudioController.instance.PlaySFX(5);                                             /////// SFX // 

            Destroy(gameObject);
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
