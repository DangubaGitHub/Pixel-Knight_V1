using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    void Start()
    {
     
    }

    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(PlayerAnimationManager.instance.isArmor == true)
            {
                PlayerAnimationManager.instance.isArmor = false;
                PlayerAnimationManager.instance.isBasic = true;
            }

            else if(PlayerAnimationManager.instance.isFire == true)
            {
                PlayerAnimationManager.instance.isFire = false;
                PlayerAnimationManager.instance.isBasic = true;
            }

            else if (PlayerAnimationManager.instance.isIce == true)
            {
                PlayerAnimationManager.instance.isIce = false; ;
                PlayerAnimationManager.instance.isBasic = true;
            }

            else if (PlayerAnimationManager.instance.isBasic == true)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
