using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKnightAnimationController : MonoBehaviour
{
    //////////////////////////////////////////////////////////// Player State //////////

    [Header("VFX")]
    [SerializeField] Transform slimeKnite;
    [SerializeField] GameObject lostArmorAir;
    [SerializeField] GameObject lostArmorGround;
    [SerializeField] GameObject slimeKnightDeath;

    //////////////////////////////////////////////////////////// Player States //////////

    [Header("Player States")]
    public bool zero_Hit;
    public bool one_Hit;
    public bool two_Hit;
    public bool three_Hit;
    public bool noArmor;
    public bool hurt;

    //////////////////////////////////////////////////////////// Animation //////////

    string currentState;

    ///////////////////////////////////// zero_Hit ///////

    const string ZERO_STILL = "SK_0_Hit_Still";
    const string ZERO_IDLE_RIGHT = "SK_0_Hit_Idle_Right";
    const string ZERO_IDLE_LEFT = "SK_0_Hit_Idle_Left";
    const string ZERO_UP_RIGHT = "SK_0_Hit_Up_Right";
    const string ZERO_UP_LEFT = "SK_0_Hit_Up_Left";
    const string ZERO_DOWN_RIGHT = "SK_0_Hit_Down_Right";
    const string ZERO_DOWN_LEFT = "SK_0_Hit_Down_Left";

    ///////////////////////////////////// one_Hit ///////

    const string ONE_IDLE_RIGHT = "SK_1_Hit_Idle_Right";
    const string ONE_IDLE_LEFT = "SK_1_Hit_Idle_Left";
    const string ONE_UP_RIGHT = "SK_1_Hit_Up_Right";
    const string ONE_UP_LEFT = "SK_1_Hit_Up_Left";
    const string ONE_DOWN_RIGHT = "SK_1_Hit_Down_Right";
    const string ONE_DOWN_LEFT = "SK_1_Hit_Down_Left";

    ///////////////////////////////////// two_Hit ///////

    const string TWO_IDLE_RIGHT = "SK_2_Hit_Idle_Right";
    const string TWO_IDLE_LEFT = "SK_2_Hit_Idle_Left";
    const string TWO_UP_RIGHT = "SK_2_Hit_Up_Right";
    const string TWO_UP_LEFT = "SK_2_Hit_Up_Left";
    const string TWO_DOWN_RIGHT = "SK_2_Hit_Down_Right";
    const string TWO_DOWN_LEFT = "SK_2_Hit_Down_Left";

    ///////////////////////////////////// three_Hit ///////

    const string THREE_IDLE_RIGHT = "SK_3_Hit_Idle_Right";
    const string THREE_IDLE_LEFT = "SK_3_Hit_Idle_Left";
    const string THREE_UP_RIGHT = "SK_3_Hit_Up_Right";
    const string THREE_UP_LEFT = "SK_3_Hit_Up_Left";
    const string THREE_DOWN_RIGHT = "SK_3_Hit_Down_Right";
    const string THREE_DOWN_LEFT = "SK_3_Hit_Down_Left";

    ///////////////////////////////////// no Armor ///////

    const string NO_ARMOR_IDLE_RIGHT = "SK_No_Armor_Idle_Right";
    const string NO_ARMOR_IDLE_LEFT = "SK_No_Armor_Idle_Left";
    const string NO_ARMOR_UP_RIGHT = "SK_No_Armor_Up_Right";
    const string NO_ARMOR_UP_LEFT = "SK_No_Armor_Up_Left";
    const string NO_ARMOR_DOWN_RIGHT = "SK_No_Armor_Down_Right";
    const string NO_ARMOR_DOWN_LEFT = "SK_No_Armor_Down_Left";

    ///////////////////////////////////// no Armor Hurt ///////

    const string HURT_IDLE_RIGHT = "SK_No_Armor_Hurt_Idle_Right";
    const string HURT_IDLE_LEFT = "SK_No_Armor_Hurt_Idle_Left";
    const string HURT_UP_RIGHT = "SK_No_Armor_Hurt_Up_Right";
    const string HURT_UP_LEFT = "SK_No_Armor_Hurt_Up_Left";
    const string HURT_DOWN_RIGHT = "SK_No_Armor_Hurt_Down_Right";
    const string HURT_DOWN_LEFT = "SK_No_Armor_Hurt_Down_Left";


    ///////////////////////////////////// Effects ///////

    const string LOST_ARMOR_AIR = "Lost_Armor_Air";
    const string LOST_ARMOR_GROUND = "Lost_Armor_Ground";
    const string DEATH_ANIMATION = "Death_Animation";

    //////////////////////////////////////////////////////////// DEclerations //////////

    Rigidbody2D rb2d;
    Animator anim;
    public static SlimeKnightAnimationController instance;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        instance = this;
    }

    void Start()
    {
        zero_Hit = true;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (SlimeKnightController.instance.isAlive)
        {
            if (zero_Hit)
            {
                if (SlimeKnightController.instance.isGrounded)
                {
                    if (SlimeKnightController.instance.armorVelocityX > 0)
                    {
                        ChangeAnimationState(ZERO_IDLE_RIGHT);
                    }

                    else if (SlimeKnightController.instance.armorVelocityX < 0)
                    {
                        ChangeAnimationState(ZERO_IDLE_LEFT);
                    }
                }

                else if (!SlimeKnightController.instance.isGrounded)
                {
                    if (SlimeKnightController.instance.armorVelocityX > 0)
                    {
                        if (rb2d.velocity.y > 0)
                        {
                            ChangeAnimationState(ZERO_UP_RIGHT);
                        }

                        else if (rb2d.velocity.y < 0)
                        {
                            ChangeAnimationState(ZERO_DOWN_RIGHT);
                        }
                    }

                    if (SlimeKnightController.instance.armorVelocityX < 0)
                    {
                        if (rb2d.velocity.y > 0)
                        {
                            ChangeAnimationState(ZERO_UP_LEFT);
                        }

                        else if (rb2d.velocity.y < 0)
                        {
                            ChangeAnimationState(ZERO_DOWN_LEFT);
                        }
                    }
                }
            }

            else if (one_Hit)
            {
                if (SlimeKnightController.instance.isGrounded)
                {
                    if (SlimeKnightController.instance.armorVelocityX > 0)
                    {
                        ChangeAnimationState(ONE_IDLE_RIGHT);
                    }

                    else if (SlimeKnightController.instance.armorVelocityX < 0)
                    {
                        ChangeAnimationState(ONE_IDLE_LEFT);
                    }
                }

                else if (!SlimeKnightController.instance.isGrounded)
                {
                    if (SlimeKnightController.instance.armorVelocityX > 0)
                    {
                        if (rb2d.velocity.y > 0)
                        {
                            ChangeAnimationState(ONE_UP_RIGHT);
                        }

                        else if (rb2d.velocity.y < 0)
                        {
                            ChangeAnimationState(ONE_DOWN_RIGHT);
                        }
                    }

                    if (SlimeKnightController.instance.armorVelocityX < 0)
                    {
                        if (rb2d.velocity.y > 0)
                        {
                            ChangeAnimationState(ONE_UP_LEFT);
                        }

                        else if (rb2d.velocity.y < 0)
                        {
                            ChangeAnimationState(ONE_DOWN_LEFT);
                        }
                    }
                }
            }

            else if (two_Hit)
            {
                if (SlimeKnightController.instance.isGrounded)
                {
                    if (SlimeKnightController.instance.armorVelocityX > 0)
                    {
                        ChangeAnimationState(TWO_IDLE_RIGHT);
                    }

                    else if (SlimeKnightController.instance.armorVelocityX < 0)
                    {
                        ChangeAnimationState(TWO_IDLE_LEFT);
                    }
                }

                else if (!SlimeKnightController.instance.isGrounded)
                {
                    if (SlimeKnightController.instance.armorVelocityX > 0)
                    {
                        if (rb2d.velocity.y > 0)
                        {
                            ChangeAnimationState(TWO_UP_RIGHT);
                        }

                        else if (rb2d.velocity.y < 0)
                        {
                            ChangeAnimationState(TWO_DOWN_RIGHT);
                        }
                    }

                    if (SlimeKnightController.instance.armorVelocityX < 0)
                    {
                        if (rb2d.velocity.y > 0)
                        {
                            ChangeAnimationState(TWO_UP_LEFT);
                        }

                        else if (rb2d.velocity.y < 0)
                        {
                            ChangeAnimationState(TWO_DOWN_LEFT);
                        }
                    }
                }
            }

            else if (three_Hit)
            {
                if (SlimeKnightController.instance.isGrounded)
                {
                    if (SlimeKnightController.instance.armorVelocityX > 0)
                    {
                        ChangeAnimationState(THREE_IDLE_RIGHT);
                    }

                    else if (SlimeKnightController.instance.armorVelocityX < 0)
                    {
                        ChangeAnimationState(THREE_IDLE_LEFT);
                    }
                }

                else if (!SlimeKnightController.instance.isGrounded)
                {
                    if (SlimeKnightController.instance.armorVelocityX > 0)
                    {
                        if (rb2d.velocity.y > 0)
                        {
                            ChangeAnimationState(THREE_UP_RIGHT);
                        }

                        else if (rb2d.velocity.y < 0)
                        {
                            ChangeAnimationState(THREE_DOWN_RIGHT);
                        }
                    }

                    if (SlimeKnightController.instance.armorVelocityX < 0)
                    {
                        if (rb2d.velocity.y > 0)
                        {
                            ChangeAnimationState(THREE_UP_LEFT);
                        }

                        else if (rb2d.velocity.y < 0)
                        {
                            ChangeAnimationState(THREE_DOWN_LEFT);
                        }
                    }
                }
            }

            else if (noArmor)
            {
                if (SlimeKnightController.instance.isGrounded)
                {
                    if (SlimeKnightController.instance.armorVelocityX > 0)
                    {
                        ChangeAnimationState(NO_ARMOR_IDLE_RIGHT);
                    }

                    else if (SlimeKnightController.instance.armorVelocityX < 0)
                    {
                        ChangeAnimationState(NO_ARMOR_IDLE_LEFT);
                    }
                }

                else if (!SlimeKnightController.instance.isGrounded)
                {
                    if (SlimeKnightController.instance.armorVelocityX > 0)
                    {
                        if (rb2d.velocity.y > 0)
                        {
                            ChangeAnimationState(NO_ARMOR_UP_RIGHT);
                        }

                        else if (rb2d.velocity.y < 0)
                        {
                            ChangeAnimationState(NO_ARMOR_DOWN_RIGHT);
                        }
                    }

                    if (SlimeKnightController.instance.armorVelocityX < 0)
                    {
                        if (rb2d.velocity.y > 0)
                        {
                            ChangeAnimationState(NO_ARMOR_UP_LEFT);
                        }

                        else if (rb2d.velocity.y < 0)
                        {
                            ChangeAnimationState(NO_ARMOR_DOWN_LEFT);
                        }
                    }
                }
            }

            else if (hurt)
            {
                if (SlimeKnightController.instance.isGrounded)
                {
                    if (SlimeKnightController.instance.armorVelocityX > 0)
                    {
                        ChangeAnimationState(HURT_IDLE_RIGHT);
                    }

                    else if (SlimeKnightController.instance.armorVelocityX < 0)
                    {
                        ChangeAnimationState(HURT_IDLE_LEFT);
                    }
                }

                else if (!SlimeKnightController.instance.isGrounded)
                {
                    if (SlimeKnightController.instance.armorVelocityX > 0)
                    {
                        if (rb2d.velocity.y > 0)
                        {
                            ChangeAnimationState(HURT_UP_RIGHT);
                        }

                        else if (rb2d.velocity.y < 0)
                        {
                            ChangeAnimationState(HURT_DOWN_RIGHT);
                        }
                    }

                    if (SlimeKnightController.instance.armorVelocityX < 0)
                    {
                        if (rb2d.velocity.y > 0)
                        {
                            ChangeAnimationState(HURT_UP_LEFT);
                        }

                        else if (rb2d.velocity.y < 0)
                        {
                            ChangeAnimationState(HURT_DOWN_LEFT);
                        }
                    }
                }
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
