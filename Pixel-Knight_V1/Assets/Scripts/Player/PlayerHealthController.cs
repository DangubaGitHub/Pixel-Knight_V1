using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public float invincibleLength;

    public static PlayerHealthController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
     
    }

    void Update()
    {
        if(invincibleLength > 0)
        {
            invincibleLength -= Time.deltaTime;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(PlayerAnimationManager.instance.isArmor == true)
            {
                PlayerAnimationManager.instance.isArmor = false;
                PlayerAnimationManager.instance.isBasic = true;
                Invincible();
            }

            else if(PlayerAnimationManager.instance.isFire == true)
            {
                PlayerAnimationManager.instance.isFire = false;
                PlayerAnimationManager.instance.isBasic = true;
                Invincible();
            }

            else if (PlayerAnimationManager.instance.isIce == true)
            {
                PlayerAnimationManager.instance.isIce = false;
                PlayerAnimationManager.instance.isBasic = true;
                Invincible();
            }

            else if (PlayerAnimationManager.instance.isBasic == true && invincibleLength <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Invincible()
    {
        invincibleLength = 2;
    }
}
