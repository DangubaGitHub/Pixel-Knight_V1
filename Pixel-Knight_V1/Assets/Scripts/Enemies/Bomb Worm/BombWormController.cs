using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWormController : MonoBehaviour
{
    ////////////////////////////// Attack //////////

    [SerializeField] GameObject bombPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] bool isAttacking;
    [SerializeField] bool nearWall;

    ////////////////////////////// Movement //////////

    [Header("Movement")]
    [SerializeField] float velocityX;
    [SerializeField] float enragedVelocityX;
    [SerializeField] bool foundDirection;

    [SerializeField] float timeBetweenAttacks;
    float nextAttackTime;

    [SerializeField] bool turning;

    [SerializeField] bool hasChanged;
    public bool isChanging;

    ////////////////////////////// Activation //////////

    [Header("Activation")]
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] Transform PlayerCheck;
    [SerializeField] float PlayerCheckRadius;
    [SerializeField] bool isActive;

    [SerializeField] GameObject Player;
    public bool isAlive;

    ////////////////////////////// Animation Controlls //////////

    const string STILL = "Bomb_Worm_Still";
    const string MOVE = "Bomb_Worm_Move";
    const string ATTACK = "Bomb_Worm_Attack";
    const string TURN = "Bomb_Worm_Turn";
    const string HIT = "Bomb_Worm_Hit";

    const string A_STILL = "A_Bomb_Worm_Still";
    const string A_MOVE = "A_Bomb_Worm_Move";
    const string A_ATTACK = "A_Bomb_Worm_Attack";
    const string A_TURN = "A_Bomb_Worm_Turn";

    string currentState;

    public bool enraged;

    ////////////////////////////// Declerations //////////

    Rigidbody2D rb2d;
    Animator anim;
    public static BombWormController instance;

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
            if (!foundDirection)
            {
                if (Player.transform.position.x > transform.position.x && transform.localScale.x == -1)
                {
                    ChangeDirection();
                }

                if (Player.transform.position.x < transform.position.x && transform.localScale.x == 1)
                {
                    ChangeDirection();
                }

                foundDirection = true;
            }

            //////////////////////////////////////////////////////////// Normal //////////

            if (!enraged && !isChanging)
            {
                if (!turning && !isAttacking)
                {
                    rb2d.velocity = new Vector2(velocityX, 0);

                    ChangeAnimationState(MOVE);
                }

                else if (turning)
                {
                    rb2d.velocity = new Vector2(0, 0);//
                    ChangeAnimationState(TURN);
                }

                else if (isAttacking)
                {
                    rb2d.velocity = new Vector2(0, 0);//
                    ChangeAnimationState(ATTACK);
                }
            }

            //////////////////////////////////////////////////////////// Enraged //////////


            


            if (enraged && !isChanging)
            {
                if (!turning && !isAttacking)
                {
                    rb2d.velocity = new Vector2(enragedVelocityX, 0);

                    ChangeAnimationState(A_MOVE);
                }

                else if (turning)
                {
                    rb2d.velocity = new Vector2(0, 0);//
                    ChangeAnimationState(A_TURN);
                }

                else if (isAttacking)
                {
                    rb2d.velocity = new Vector2(0, 0);//
                    ChangeAnimationState(A_ATTACK);
                }
            }

            if (isChanging)
            {
                ChangeAnimationState(HIT);
                rb2d.velocity = new Vector2(0, 0);
            }

            //////////////////////////////////////////////////////////// Attack //////////

            if (!nearWall)
            {
                if (Time.time > nextAttackTime)
                {
                    isAttacking = true;
                    nextAttackTime = Time.time + timeBetweenAttacks;
                }
            }
        }

        else
        {
            if(!enraged)
            {
                ChangeAnimationState(STILL);
            }

            else if (enraged)
            {
                ChangeAnimationState(A_STILL);
            }

            rb2d.velocity = new Vector2(0, 0);
            foundDirection = false;
        }

        //////////////////////////////////////////////////////////// Flip Player //////////

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
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall" ||
            other.gameObject.tag == "Enemy")
        {
            turning = true;
            ChangeDirection();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Wall"))
        {
            nearWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            nearWall = false;
        }
    }

    public void HasChanged()
    {
        isChanging = false;
        enraged = true;
    }

    void ChangeDirection()
    {
        velocityX = -velocityX;
        enragedVelocityX = -enragedVelocityX;
    }

    public void EndAttack()
    {
        isAttacking = false;
        nextAttackTime = Time.time + Random.Range(2, 4);
    }

    public void EndTurning()
    {
        turning = false;
    }

    public void Bomb()
    {
        Instantiate(bombPrefab, firePoint.position, Quaternion.identity);
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
