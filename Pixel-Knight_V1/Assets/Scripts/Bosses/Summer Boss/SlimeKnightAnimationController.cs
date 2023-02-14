using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKnightAnimationController : MonoBehaviour
{
    //////////////////////////////////////////////////////////// Player State //////////

    [SerializeField] bool zero_Hit;
    [SerializeField] bool one_Hit;
    [SerializeField] bool two_Hit;
    [SerializeField] bool three_Hit;
    [SerializeField] bool noArmor;

    //////////////////////////////////////////////////////////// Animation //////////

    string currentState;

    ///////////////////////////////////// zero_Hit ///////

    const string ZERO_STILL = "SK_0_Hit_Still";
    const string ZERO_IDLE_RIGHT = "SK_0_Idle_Right";
    const string ZERO_IDLE_LEFT = "SK_0_Idle_Left";
    const string ZERO_UP_RIGHT = "SK_0_Up_Right";
    const string ZERO_UP_LEFT = "SK_0_Up_LEFT";
    const string ZERO_DOWN_RIGHT = "SK_0_Down_Right";
    const string ZERO_DOWN_LEFT = "SK_0_Down_Left";

    ///////////////////////////////////// one_Hit ///////

    const string ONE_STILL = "SK_1_Hit_Still";
    const string ONE_IDLE_RIGHT = "SK_1_Idle_Right";
    const string ONE_IDLE_LEFT = "SK_1_Idle_Left";
    const string ONE_UP_RIGHT = "SK_1_Up_Right";
    const string ONE_UP_LEFT = "SK_1_Up_LEFT";
    const string ONE_DOWN_RIGHT = "SK_1_Down_Right";
    const string ONE_DOWN_LEFT = "SK_1_Down_Left";

    ///////////////////////////////////// two_Hit ///////

    const string TWO_STILL = "SK_2_Hit_Still";
    const string TWO_IDLE_RIGHT = "SK_2_Idle_Right";
    const string TWO_IDLE_LEFT = "SK_2_Idle_Left";
    const string TWO_UP_RIGHT = "SK_2_Up_Right";
    const string TWO_UP_LEFT = "SK_2_Up_LEFT";
    const string TWO_DOWN_RIGHT = "SK_2_Down_Right";
    const string TWO_DOWN_LEFT = "SK_2_Down_Left";

    ///////////////////////////////////// three_Hit ///////

    const string THREE_STILL = "SK_3_Hit_Still";
    const string THREE_IDLE_RIGHT = "SK_3_Idle_Right";
    const string THREE_IDLE_LEFT = "SK_3_Idle_Left";
    const string THREE_UP_RIGHT = "SK_3_Up_Right";
    const string THREE_UP_LEFT = "SK_3_Up_LEFT";
    const string THREE_DOWN_RIGHT = "SK_3_Down_Right";
    const string THREE_DOWN_LEFT = "SK_3_Down_Left";

    ///////////////////////////////////// no Armor ///////

    const string NO_ARMOR_STILL = "No_Armor_Still";
    const string NO_ARMOR_IDLE_RIGHT = "No_Armor_Idle_Right";
    const string NO_ARMOR_IDLE_LEFT = "No_Armor_Idle_Left";
    const string NO_ARMOR_UP_RIGHT = "No_Armor_Up_Right";
    const string NO_ARMOR_UP_LEFT = "No_Armor_Up_LEFT";
    const string NO_ARMOR_DOWN_RIGHT = "No_Armor_Down_Right";
    const string NO_ARMOR_DOWN_LEFT = "No_Armor_Down_Left";

    ///////////////////////////////////// Effects ///////

    const string LOST_ARMOR_AIR = "Lost_Armor_Air";
    const string LOST_ARMOR_GROUND = "Lost_Armor_Ground";
    const string DEATH_ANIMATION = "Death_Animation";

    //////////////////////////////////////////////////////////// DEclerations //////////

    Animator anim;
    public static SlimeKnightAnimationController instance;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        instance = this;
    }

    void Start()
    {
        
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

            }

            else if (one_Hit)
            {

            }

            else if (two_Hit)
            {

            }

            else if (three_Hit)
            {

            }

            else if (noArmor)
            {

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
