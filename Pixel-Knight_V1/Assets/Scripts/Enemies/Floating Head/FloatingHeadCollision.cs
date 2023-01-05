using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHeadCollision : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (PlayerAnimationManager.instance.isArmor)
            {
                FloatingHeadController.instance.velocity = -FloatingHeadController.instance.velocity;
            }

            if (PlayerAnimationManager.instance.isFire)
            {
                FloatingHeadController.instance.velocity = -FloatingHeadController.instance.velocity;
            }

            if (PlayerAnimationManager.instance.isIce)
            {
                FloatingHeadController.instance.velocity = -FloatingHeadController.instance.velocity;
            }
        }

        if (other.gameObject.tag == "Wall")
        {
            FloatingHeadController.instance.velocity = -FloatingHeadController.instance.velocity;
        }
    }
}
