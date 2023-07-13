using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PauseMenu.instance.PauseUnpause();
        }

        if(other.CompareTag("Enemy Invulnerable Damaging"))
        {
            Destroy(other.gameObject);
        }

    }
}
