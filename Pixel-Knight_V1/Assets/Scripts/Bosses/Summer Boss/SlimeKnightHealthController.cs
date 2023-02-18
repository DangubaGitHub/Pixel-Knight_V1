using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKnightHealthController : MonoBehaviour
{
    public int health;
    float invulnerable;

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
            Invoke("StopHurting", 2);
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
                SlimeKnightAnimationController.instance.zero_Hit = false;
                SlimeKnightAnimationController.instance.one_Hit = true;
            }

            else if (SlimeKnightAnimationController.instance.one_Hit)
            {
                SlimeKnightAnimationController.instance.one_Hit = false;
                SlimeKnightAnimationController.instance.two_Hit = true;
            }

            else if (SlimeKnightAnimationController.instance.two_Hit)
            {
                SlimeKnightAnimationController.instance.two_Hit = false;
                SlimeKnightAnimationController.instance.three_Hit = true;
            }

            else if (SlimeKnightAnimationController.instance.three_Hit)
            {
                SlimeKnightAnimationController.instance.three_Hit = false;
                SlimeKnightAnimationController.instance.noArmor = true;
                SlimeKnightAnimationController.instance.noArmor = true;
            }

            else if (SlimeKnightAnimationController.instance.noArmor && invulnerable <=0)
            {
                SlimeKnightAnimationController.instance.noArmor = false;
                SlimeKnightAnimationController.instance.hurt = true;
                invulnerable = 2;
                health--;
            }
        }
    }

    void StopHurting()
    {
        SlimeKnightAnimationController.instance.hurt = false;
        SlimeKnightAnimationController.instance.noArmor = true;
    }
}
