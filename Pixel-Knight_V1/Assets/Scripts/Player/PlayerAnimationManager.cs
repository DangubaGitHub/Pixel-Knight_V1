using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public bool isBasic;
    public bool isArmor;
    public bool isFire;
    public bool isIce;

    string currentState;

    //Basic Player

    const string BP_IDLE = "bp_idle";
    const string BP_RUN = "bp_run";
    const string BP_JUMP = "bp_jump";
    const string BP_FALL = "bp_fall";

    //Armor Player

    const string AP_IDLE = "ap_idle";
    const string AP_RUN = "ap_run";
    const string AP_JUMP = "ap_jump";
    const string AP_FALL = "ap_fall";

    //Fire Player

    //Ice Player



    public static PlayerAnimationManager instance;

    Animator anim;
    Rigidbody2D rb2d;

    private void Awake()
    {
        instance = this;

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 characterScale = transform.localScale;

        if (rb2d.velocity.x < -0.1f)
        {
            characterScale.x = -1;
            PlayerController.instance.firePoint.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (rb2d.velocity.x > 0.1f)
        {
            characterScale.x = 1;
            PlayerController.instance.firePoint.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        transform.localScale = characterScale;
    }

    private void FixedUpdate()
    {
        if (isBasic)
        {
            if (PlayerController.instance.isGrounded)
            {
                if (rb2d.velocity.x == 0)
                {
                    ChangeAnimationState(BP_IDLE);
                }

                if (rb2d.velocity.x != 0)
                {
                    ChangeAnimationState(BP_RUN);
                }
            }

            if (rb2d.velocity.y > 0f && PlayerController.instance.isGrounded == false)
            {
                ChangeAnimationState(BP_JUMP);
            }

            if (rb2d.velocity.y < 0f && PlayerController.instance.isGrounded == false)
            {
                ChangeAnimationState(BP_FALL);
            }
        }

        if (isArmor)
        {
            if (PlayerController.instance.isGrounded)
            {
                if (rb2d.velocity.x == 0)
                {
                    ChangeAnimationState(AP_IDLE);
                }

                if (rb2d.velocity.x != 0)
                {
                    ChangeAnimationState(AP_RUN);
                }
            }

            if (rb2d.velocity.y > 0f && PlayerController.instance.isGrounded == false)
            {
                ChangeAnimationState(AP_JUMP);
            }

            if (rb2d.velocity.y < 0f && PlayerController.instance.isGrounded == false)
            {
                ChangeAnimationState(AP_FALL);
            }
        }
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
