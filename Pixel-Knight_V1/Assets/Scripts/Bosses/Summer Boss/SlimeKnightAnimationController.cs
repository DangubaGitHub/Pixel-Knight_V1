using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKnightAnimationController : MonoBehaviour
{
    //////////////////////////////////////////////////////////// Player State //////////

    [Header("VFX")]
    [SerializeField] Transform slimeKnite;
    [SerializeField] GameObject lostArmorEffectr;
    [SerializeField] Transform lostArmorEffectPoint;
    [SerializeField] GameObject slimeKnightDeath;
    [SerializeField] float timeBetweenNextExplosion;

    public bool animationRunning;

    //////////////////////////////////////////////////////////// Player States //////////

    [Header("Player States")]
    public bool zero_Hit;
    public bool one_Hit;
    public bool two_Hit;
    public bool three_Hit;
    public bool noArmor;
    public bool hurt;

    //////////////////////////////////////////////////////////// Death Animation //////////

    [Header("Death Animation Points")]
    [SerializeField] Transform point1;
    [SerializeField] Transform point2;
    [SerializeField] Transform point3;
    [SerializeField] Transform point4;
    [SerializeField] Transform point5;
    [SerializeField] Transform finalPoint;
    //[SerializeField] Transform[] explosionPoints;

    [Header("Death Animation Prefabs")]
    [SerializeField] GameObject bossExplosion1Prefab;
    [SerializeField] GameObject bossExplosion2Prefab;
    [SerializeField] GameObject bossExplosion3Prefab;
    //[SerializeField] GameObject[] explosionPrefabs;

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
    const string DEAD_RIGHT = "SK_No_Armor_Dead_Right";
    const string DEAD_LEFT = "SK_No_Armor_Dead_Left";


    ///////////////////////////////////// Effects ///////

    const string LOST_ARMOR = "Lost_Armor_Effect";
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
        //StartCoroutine(DeathExplosionAnimation());
        //coroutineRunning = false;
    }

    void Update()
    {
        /*if(coroutineRunning)
        {
            StartCoroutine(DeathExplosionAnimation());
            coroutineRunning = false;
        }*/

        if (!SlimeKnightController.instance.isAlive)
        {
            if (!animationRunning)
            {
                ExplosionAnimation();
                animationRunning = true;
            }
            
        }
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

        else if (!SlimeKnightController.instance.isAlive)
        {
            if (SlimeKnightController.instance.armorVelocityX > 0)
            {
                ChangeAnimationState(DEAD_RIGHT);
            }

            else if (SlimeKnightController.instance.armorVelocityX < 0)
            {
                ChangeAnimationState(DEAD_LEFT);
            }
        }

        if (SlimeKnightHealthController.instance.lostArmor == true)
        {
            Instantiate(lostArmorEffectr, lostArmorEffectPoint.position, Quaternion.identity);
            SlimeKnightHealthController.instance.lostArmor = false;
        }

       
    }

    /*IEnumerator DeathExplosionAnimation()
    {

    
                yield return new WaitForSeconds(.5f);

                //Instantiate(bossExplosion1Prefab, point2.position, Quaternion.identity);

                yield return new WaitForSeconds(timeBetweenNextExplosion);

                //Instantiate(bossExplosion2Prefab, point5.position, Quaternion.identity);

                yield return new WaitForSeconds(timeBetweenNextExplosion);

                //Instantiate(bossExplosion3Prefab, point1.position, Quaternion.identity);

                yield return new WaitForSeconds(timeBetweenNextExplosion);

                //Instantiate(bossExplosion1Prefab, point4.position, Quaternion.identity);

                yield return new WaitForSeconds(timeBetweenNextExplosion);

                //Instantiate(bossExplosion2Prefab, point3.position, Quaternion.identity);

                yield return new WaitForSeconds(timeBetweenNextExplosion);

                //Instantiate(bossExplosion3Prefab, point2.position, Quaternion.identity);

                yield return new WaitForSeconds(timeBetweenNextExplosion);

                //Instantiate(bossExplosion1Prefab, point5.position, Quaternion.identity);

                yield return new WaitForSeconds(timeBetweenNextExplosion);

                //Instantiate(bossExplosion2Prefab, point1.position, Quaternion.identity);

                yield return new WaitForSeconds(timeBetweenNextExplosion);

                //Instantiate(bossExplosion3Prefab, point4.position, Quaternion.identity);

                yield return new WaitForSeconds(timeBetweenNextExplosion);

                //Instantiate(bossExplosion1Prefab, point3.position, Quaternion.identity);

                yield return new WaitForSeconds(timeBetweenNextExplosion);

                //Instantiate(bossExplosion2Prefab, point2.position, Quaternion.identity);

                yield return new WaitForSeconds(timeBetweenNextExplosion);

                //Instantiate(bossExplosion3Prefab, point5.position, Quaternion.identity);

                yield return new WaitForSeconds(timeBetweenNextExplosion);

                Instantiate(lostArmorEffectr, finalPoint.position, Quaternion.identity);
                MovingWallSummerCastleController.instance.bossIsDead = true;
                Destroy(gameObject);
         
    }*/

    void ExplosionAnimation()
    {
        Invoke("Explosion1", .5f);
        Invoke("Explosion2", .7f);
        Invoke("Explosion3", .9f);
        Invoke("Explosion4", 1.1f);
        Invoke("Explosion5", 1.3f);
        Invoke("Explosion6", 1.5f);
        Invoke("Explosion7", 1.7f);
        Invoke("Explosion8", 1.9f);
        Invoke("Explosion9", 2.1f);
        Invoke("Explosion10", 2.3f);
        Invoke("Explosion11", 2.5f);
        Invoke("Explosion12", 2.7f);
        Invoke("FinalExplosion", 2.9f);
    }

    void Explosion1()
    {
        Instantiate(bossExplosion1Prefab, point2.position, Quaternion.identity);
    }

    void Explosion2()
    {
        Instantiate(bossExplosion2Prefab, point5.position, Quaternion.identity);
    }

    void Explosion3()
    {
        Instantiate(bossExplosion3Prefab, point1.position, Quaternion.identity);
    }

    void Explosion4()
    {
        Instantiate(bossExplosion1Prefab, point4.position, Quaternion.identity);
    }

    void Explosion5()
    {
        Instantiate(bossExplosion2Prefab, point3.position, Quaternion.identity);
    }

    void Explosion6()
    {
        Instantiate(bossExplosion3Prefab, point2.position, Quaternion.identity);
    }

    void Explosion7()
    {
        Instantiate(bossExplosion1Prefab, point5.position, Quaternion.identity);
    }

    void Explosion8()
    {
        Instantiate(bossExplosion2Prefab, point1.position, Quaternion.identity);
    }

    void Explosion9()
    {
        Instantiate(bossExplosion3Prefab, point4.position, Quaternion.identity);
    }

    void Explosion10()
    {
        Instantiate(bossExplosion1Prefab, point3.position, Quaternion.identity);
    }

    void Explosion11()
    {
        Instantiate(bossExplosion2Prefab, point2.position, Quaternion.identity);
    }

    void Explosion12()
    {
        Instantiate(bossExplosion3Prefab, point5.position, Quaternion.identity);
    }

    void FinalExplosion()
    {
        Instantiate(lostArmorEffectr, finalPoint.position, Quaternion.identity);
        MovingWallSummerCastleController.instance.bossIsDead = true;
        Destroy(gameObject, .35f);
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
