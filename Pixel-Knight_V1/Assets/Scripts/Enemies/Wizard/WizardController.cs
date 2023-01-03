using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour
{
    ////////////////////////////// Attack //////////

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject magicPrefab;
    float animationTimeCounter;
    [SerializeField] float animationTime;

    //[SerializeField] float invokeFireMagic;
    //[SerializeField] float invokeResetAttackTimer;
    //bool isFiring;

    //[SerializeField] float timeBetweenAnimations;
    //float nextAnimationTime;
    [SerializeField] float timeBetweenShots;
    float nextShotTime;

    ////////////////////////////// Activation //////////

    [Header("Activation")]
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] Transform PlayerCheck;
    [SerializeField] float PlayerCheckRadius;
    [SerializeField] bool isActive;
    [SerializeField] bool foundDirection;

    [SerializeField] GameObject Player;

    ////////////////////////////// Animation //////////

    const string STILL = "Wizard_Still";
    const string ATTACK = "Wizard_Attack";

    string currentState;

    ////////////////////////////// Declerations //////////

    Rigidbody2D rb2d;
    Animator anim;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        animationTimeCounter = animationTime;
    }

    void Update()
    {
        isActive = Physics2D.OverlapCircle(PlayerCheck.position, PlayerCheckRadius, PlayerLayer);

        if(isActive)
        {
            



            animationTimeCounter -= Time.deltaTime;

            if(animationTimeCounter <= 0)
            {
                ChangeAnimationState(ATTACK);
                
                if(Time.time > nextShotTime)
                {
                    Instantiate(magicPrefab, firePoint.position, firePoint.rotation);
                    nextShotTime = Time.time + timeBetweenShots;
                    animationTimeCounter = animationTime;
                }
            }

            
        }

        else
        {
            ChangeAnimationState(STILL);
        }

        Vector3 characterScale = transform.localScale;

        if (Player.transform.position.x < transform.position.x)
        {
            characterScale.x = -1;
        }

        else if (Player.transform.position.x > transform.position.x)
        {
            characterScale.x = 1;
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
