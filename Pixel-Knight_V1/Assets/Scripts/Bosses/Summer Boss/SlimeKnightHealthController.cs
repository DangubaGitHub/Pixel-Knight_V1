using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKnightHealthController : MonoBehaviour
{
    public int health;
    public float invulnerable;
    public bool lostArmor;

    public static SlimeKnightHealthController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        health = 3;
    }

    void Update()
    {
        if (invulnerable > 0)
        {
            invulnerable -= Time.deltaTime;
        }

        if (SlimeKnightAnimationController.instance.hurt)
        {
            Invoke("StopHurting", 1);
        }

        if (health <= 0)
        {
            SlimeKnightController.instance.isAlive = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (SlimeKnightAnimationController.instance.zero_Hit)
            {
                SummerBossExtras();
                SlimeKnightAnimationController.instance.zero_Hit = false;
                SlimeKnightAnimationController.instance.one_Hit = true;
            }

            else if (SlimeKnightAnimationController.instance.one_Hit)
            {
                SummerBossExtras();
                SlimeKnightAnimationController.instance.one_Hit = false;
                SlimeKnightAnimationController.instance.two_Hit = true;
            }

            else if (SlimeKnightAnimationController.instance.two_Hit)
            {
                SummerBossExtras();
                SlimeKnightAnimationController.instance.two_Hit = false;
                SlimeKnightAnimationController.instance.three_Hit = true;
            }

            else if (SlimeKnightAnimationController.instance.three_Hit)
            {
                SlimeKnightController.instance.isNoArmor = true;
                SlimeKnightController.instance.isArmor = false;
                lostArmor = true;
                SummerBossExtras();
                SlimeKnightAnimationController.instance.three_Hit = false;
                SlimeKnightAnimationController.instance.noArmor = true;
                //SlimeKnightAnimationController.instance.noArmor = true;
            }

            else if (SlimeKnightAnimationController.instance.noArmor && invulnerable <=0)
            {
                SummerBossExtras();
                SlimeKnightAnimationController.instance.noArmor = false;
                SlimeKnightAnimationController.instance.hurt = true;
                invulnerable = 1;
                health--;
            }
        }

        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == ("Player Magic"))
        {
            if (SlimeKnightAnimationController.instance.noArmor)
            {
                Destroy(other.gameObject);
                SlimeKnightAnimationController.instance.noArmor = false;
                SlimeKnightAnimationController.instance.hurt = true;
                invulnerable = 1;
                health--;
            }
        }
    }

    void SummerBossExtras()
    {
        PlayerHealthController.instance.shortInvulnerability = 0.5f;
        invulnerable = 0.5f;
        PlayerController.instance.BounceOnEnemy();
    }

    void StopHurting()
    {
        SlimeKnightAnimationController.instance.hurt = false;
        SlimeKnightAnimationController.instance.noArmor = true;
    }
}
