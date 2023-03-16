using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArcherController : MonoBehaviour
{
    ////////////////////////////// Attack //////////

    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] bool inRange;

    public bool lookingLeft;
    public bool lookingRight;

    ////////////////////////////// Activation //////////

    [Header("Activation")]
    [SerializeField] bool isActive;

    [SerializeField] GameObject Player;
    public bool isAlive;

    ////////////////////////////// Animation Controlls //////////

    const string STILL = "Skeleton_Archer_Still";
    const string IDLE = "Skeleton_Archer_Idle";
    const string ATTACK = "Skeleton_Archer_Attack";

    string currentState;

    [SerializeField] GameObject enemyDeathEffect;
    [SerializeField] GameObject bulletDestroyAnimation;

    ////////////////////////////// Declerations //////////

    Rigidbody2D rb2d;
    Animator anim;
    //public static SkeletonArcherController instance;
    Transform skeletonTransform;

    private void Awake()
    {
        //instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        skeletonTransform = GetComponent<Transform>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (isActive)
        {
            if (!inRange)
            {
                ChangeAnimationState(IDLE);
            }

            else if(inRange)
            {
                ChangeAnimationState(ATTACK);
            }
        }

        else
        {
            ChangeAnimationState(STILL);
        }

        Vector3 characterScale = transform.localScale;

        if (Player.transform.position.x <= transform.position.x)
        {
            characterScale.x = -1;
            lookingLeft = true;
            lookingRight = false;
        }

        else if (Player.transform.position.x > transform.position.x)
        {
            characterScale.x = 1;
            lookingLeft = false;
            lookingRight = true;
        }

        transform.localScale = characterScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }

        if (other.CompareTag("Player Stomp Box"))
        {
            PlayerController.instance.BounceOnEnemy();

            Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

        if (other.CompareTag("Player Fire Magic") ||
            other.CompareTag("Player Ice Magic"))
        {
            Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
    
    public void Attack()
    {
        Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
    }

    private void OnBecameVisible()
    {
        isActive = true;
    }

    private void OnBecameInvisible()
    {
        isActive = false;
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
