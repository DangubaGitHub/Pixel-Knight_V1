using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{

    ////////////////////////////////////////////////// Enemies //////////   

    [Header("Enemies")]
    [SerializeField] bool enemy;
    [SerializeField] bool enemyInvulnerableBounce;
    [SerializeField] bool greenSlime;
    [SerializeField] bool purpleSlime;
    [SerializeField] bool redSlime;
    [SerializeField] bool blueSlime;
    [SerializeField] bool bombWorm;

    ////////////////////////////////////////////////// Extra's ////////// 

    [SerializeField] GameObject enemyDeathEffect;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (enemy)
            {
                PlayerController.instance.BounceOnEnemy();
                Instantiate(enemyDeathEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }

            if (enemyInvulnerableBounce)
            {
                PlayerController.instance.BounceOnEnemy();
                //isCrouching = true;
                //crouchTimerCountdown = 1f;
            }

            if (greenSlime) 
            {
                //if (!isGrounded)
                {

                }

                //else if (isGrounded)
                {

                }

            }

            if (purpleSlime)
            {

            }

            if (redSlime)
            {

            }

            if (blueSlime)
            {

            }

            if(bombWorm) 
            { 
            
            }
        }
    }
}
