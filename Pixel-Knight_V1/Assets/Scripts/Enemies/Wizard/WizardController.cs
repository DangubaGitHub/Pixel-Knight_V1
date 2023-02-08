using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour
{
    ////////////////////////////// Shooting //////////
    [Header("Shooting")]
    [SerializeField] GameObject magicProjectile;
    [SerializeField] Transform firePoint;

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
    const string VISIBLE = "Wizard_Still_Visible";
    const string ATTACK = "Wizard_Attack";

    string currentState;

    public bool isVisible;

    ////////////////////////////// Declerations //////////

    Rigidbody2D rb2d;
    Animator anim;
    public static WizardController instance;

    private void Awake()
    {
        instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        isActive = Physics2D.OverlapCircle(PlayerCheck.position, PlayerCheckRadius, PlayerLayer);

        if(isActive)
        {
            ChangeAnimationState(ATTACK);
        }

        else
        {
            if (isVisible)
            {
                ChangeAnimationState(VISIBLE);
            }

            else if (!isVisible)
            {
                ChangeAnimationState(STILL);
            }
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

    public void Shoot()
    {
        Instantiate(magicProjectile, firePoint.position, Quaternion.identity);
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
