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


    [SerializeField] Transform particalPoint;
    [SerializeField] GameObject liveUpPrefab;

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

    const string FP_IDLE = "fp_idle";
    const string FP_RUN = "fp_run";
    const string FP_JUMP = "fp_jump";
    const string FP_FALL = "fp_fall";
    const string FP_ATTACK = "fp_attack";
    const string FP_ATTACK_JUMP = "fp_attack_jump";
    const string FP_ATTACK_FALL = "fp_attack_fall";

    //Ice Player

    const string IP_IDLE = "ip_idle";
    const string IP_RUN = "ip_run";
    const string IP_JUMP = "ip_jump";
    const string IP_FALL = "ip_fall";
    const string IP_ATTACK = "ip_attack";
    const string IP_ATTACK_JUMP = "ip_attack_jump";
    const string IP_ATTACK_FALL = "ip_attack_fall";

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
            PlayerController.instance.lookingLeft = true;
            PlayerController.instance.lookingRight = false;
        }
        else if (rb2d.velocity.x > 0.1f)
        {
            characterScale.x = 1;
            PlayerController.instance.firePoint.transform.eulerAngles = new Vector3(0, 0, 0);
            PlayerController.instance.lookingRight = true;
            PlayerController.instance.lookingLeft = false;
        }

        transform.localScale = characterScale;





        if (PlayerController.instance.isDead == false)
        {
            if (isBasic)
            {
                if (PlayerController.instance.isGrounded)
                {
                    if (rb2d.velocity.x < 0.1f && rb2d.velocity.x > -0.1f)
                    {
                        ChangeAnimationState(BP_IDLE);
                    }

                    else
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
                    if (rb2d.velocity.x < 0.1f && rb2d.velocity.x > -0.1f)
                    {
                        ChangeAnimationState(AP_IDLE);
                    }

                    else
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

            if (isFire)
            {
                if (PlayerController.instance.isGrounded && !PlayerController.instance.isFireAttacking)
                {
                    if (rb2d.velocity.x < 0.1f && rb2d.velocity.x > -0.1f)
                    {
                        ChangeAnimationState(FP_IDLE);
                    }

                    else
                    {
                        ChangeAnimationState(FP_RUN);
                    }
                }

                if (rb2d.velocity.y > 0f && PlayerController.instance.isGrounded == false && !PlayerController.instance.isFireAirAttacking)
                {
                    ChangeAnimationState(FP_JUMP);
                }

                else if (rb2d.velocity.y > 0f && PlayerController.instance.isGrounded == false && PlayerController.instance.isFireAirAttacking)
                {
                    ChangeAnimationState(FP_ATTACK_JUMP);
                }

                if (rb2d.velocity.y < 0f && PlayerController.instance.isGrounded == false && !PlayerController.instance.isFireAirAttacking)
                {
                    ChangeAnimationState(FP_FALL);
                }

                else if (rb2d.velocity.y < 0f && PlayerController.instance.isGrounded == false && PlayerController.instance.isFireAirAttacking)
                {
                    ChangeAnimationState(FP_ATTACK_FALL);
                }

                if (PlayerController.instance.isFireAttacking && PlayerController.instance.isGrounded)
                {
                    ChangeAnimationState(FP_ATTACK);
                }
            }

            if (isIce)
            {
                if (PlayerController.instance.isGrounded && !PlayerController.instance.isIceAttacking)
                {
                    if (rb2d.velocity.x < 0.1f && rb2d.velocity.x > -0.1f)
                    {
                        ChangeAnimationState(IP_IDLE);
                    }

                    else
                    {
                        ChangeAnimationState(IP_RUN);
                    }
                }

                if (rb2d.velocity.y > 0f && PlayerController.instance.isGrounded == false && !PlayerController.instance.isIceAirAttacking)
                {
                    ChangeAnimationState(IP_JUMP);
                }

                else if (rb2d.velocity.y > 0f && PlayerController.instance.isGrounded == false && PlayerController.instance.isIceAirAttacking)
                {
                    ChangeAnimationState(IP_ATTACK_JUMP);
                }

                if (rb2d.velocity.y < 0f && PlayerController.instance.isGrounded == false && !PlayerController.instance.isIceAirAttacking)
                {
                    ChangeAnimationState(IP_FALL);
                }

                else if (rb2d.velocity.y < 0f && PlayerController.instance.isGrounded == false && PlayerController.instance.isIceAirAttacking)
                {
                    ChangeAnimationState(IP_ATTACK_FALL);
                }

                if (PlayerController.instance.isIceAttacking && PlayerController.instance.isGrounded)
                {
                    ChangeAnimationState(IP_ATTACK);
                }
            }
        }

        if (PlayerController.instance.isDead == true)
        {
            ChangeAnimationState(BP_FALL);
        }
    }

    public void LiveUpAnimation()
    {
        Instantiate(liveUpPrefab, particalPoint.position, Quaternion.identity);
    }

    private void FixedUpdate()
    {/*
        if (PlayerController.instance.isDead == false)
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

            if (isFire)
            {
                if (PlayerController.instance.isGrounded && !PlayerController.instance.isFireAttacking)
                {
                    if (rb2d.velocity.x == 0)
                    {
                        ChangeAnimationState(FP_IDLE);
                    }

                    if (rb2d.velocity.x != 0)
                    {
                        ChangeAnimationState(FP_RUN);
                    }
                }

                if (rb2d.velocity.y > 0f && PlayerController.instance.isGrounded == false && !PlayerController.instance.isFireAirAttacking)
                {
                    ChangeAnimationState(FP_JUMP);
                }

                else if (rb2d.velocity.y > 0f && PlayerController.instance.isGrounded == false && PlayerController.instance.isFireAirAttacking)
                {
                    ChangeAnimationState(FP_ATTACK_JUMP);
                }

                if (rb2d.velocity.y < 0f && PlayerController.instance.isGrounded == false && !PlayerController.instance.isFireAirAttacking)
                {
                    ChangeAnimationState(FP_FALL);
                }

                else if (rb2d.velocity.y < 0f && PlayerController.instance.isGrounded == false && PlayerController.instance.isFireAirAttacking)
                {
                    ChangeAnimationState(FP_ATTACK_FALL);
                }

                if (PlayerController.instance.isFireAttacking && PlayerController.instance.isGrounded)
                {
                    ChangeAnimationState(FP_ATTACK);
                }
            }

            if (isIce)
            {
                if (PlayerController.instance.isGrounded && !PlayerController.instance.isIceAttacking)
                {
                    if (rb2d.velocity.x == 0)
                    {
                        ChangeAnimationState(IP_IDLE);
                    }

                    if (rb2d.velocity.x != 0)
                    {
                        ChangeAnimationState(IP_RUN);
                    }
                }

                if (rb2d.velocity.y > 0f && PlayerController.instance.isGrounded == false && !PlayerController.instance.isIceAirAttacking)
                {
                    ChangeAnimationState(IP_JUMP);
                }

                else if (rb2d.velocity.y > 0f && PlayerController.instance.isGrounded == false && PlayerController.instance.isIceAirAttacking)
                {
                    ChangeAnimationState(IP_ATTACK_JUMP);
                }

                if (rb2d.velocity.y < 0f && PlayerController.instance.isGrounded == false && !PlayerController.instance.isIceAirAttacking)
                {
                    ChangeAnimationState(IP_FALL);
                }

                else if (rb2d.velocity.y < 0f && PlayerController.instance.isGrounded == false && PlayerController.instance.isIceAirAttacking)
                {
                    ChangeAnimationState(IP_ATTACK_FALL);
                }

                if (PlayerController.instance.isIceAttacking && PlayerController.instance.isGrounded)
                {
                    ChangeAnimationState(IP_ATTACK);
                }
            }
        }

        if(PlayerController.instance.isDead == true)
        {
            ChangeAnimationState(BP_FALL);
        }*/
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
