using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] GameObject enemyDeathEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (PlayerAnimationManager.instance.isBasic == true && PlayerHealthController.instance.invincibleLength <= 0)
            {
                //Instantiate(enemyDeathEffect, other.transform.position, other.transform.rotation);
            }
        }
    }
}
