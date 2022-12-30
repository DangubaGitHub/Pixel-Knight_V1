using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public float invincibleLength;

    public static PlayerHealthController instance;

    [SerializeField] Transform particlePoint;
    //[SerializeField] Transform lostIcePoint;
    //[SerializeField] Transform lostIronPoint;

    [Header("Particle Prefabs")]
    [SerializeField] GameObject lostFirePrefab;
    [SerializeField] GameObject lostIcePrefab;
    [SerializeField] GameObject lostArmorPrefab;

    [Header("Lost Power Prefab")]
    [SerializeField] GameObject lostFireAnim;
    [SerializeField] GameObject lostIceAnim;
    [SerializeField] GameObject lostArmorAnim;

    SpriteRenderer theSR;
    Rigidbody2D rb2d;
    Collider2D capsuleCollider2d;

    private void Awake()
    {
        instance = this;
        theSR = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        capsuleCollider2d = GetComponent<Collider2D>();
    }

    void Start()
    {
     
    }

    void Update()
    {
        if(invincibleLength > 0)
        {
            invincibleLength -= Time.deltaTime;
            VisibilityHalf();
        }
        else if(invincibleLength <= 0)
        {
            VisibilityFull();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy" || 
           other.gameObject.tag == "Spikes" ||
           other.gameObject.tag == "Enemy Invulnerable Damaging" || 
           other.gameObject.tag == "Enemy Invulnerable Bounce")
        {
            if(PlayerAnimationManager.instance.isArmor == true)
            {
                PlayerAnimationManager.instance.isArmor = false;
                PlayerAnimationManager.instance.isBasic = true;
                //PlayerController.instance.KnockBack();
                Invincible();
                LostArmor();
            }

            else if(PlayerAnimationManager.instance.isFire == true)
            {
                PlayerAnimationManager.instance.isFire = false;
                PlayerAnimationManager.instance.isBasic = true;
                //PlayerController.instance.KnockBack();
                Invincible();
                LostFire();
            }

            else if (PlayerAnimationManager.instance.isIce == true)
            {
                PlayerAnimationManager.instance.isIce = false;
                PlayerAnimationManager.instance.isBasic = true;
                //PlayerController.instance.KnockBack();
                Invincible();
                LostIce();
            }

            else if (PlayerAnimationManager.instance.isBasic == true && invincibleLength <= 0)
            {
                //Destroy(this.gameObject);
                PlayerController.instance.isDead = true;
                Died();
                Invoke("AfterDeath", 2f);
            }
        }
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Spikes"))
        {
            if (PlayerAnimationManager.instance.isArmor == true)
            {
                PlayerAnimationManager.instance.isArmor = false;
                PlayerAnimationManager.instance.isBasic = true;
                //PlayerController.instance.KnockBack();
                Invincible();
                LostArmor();
            }

            else if (PlayerAnimationManager.instance.isFire == true)
            {
                PlayerAnimationManager.instance.isFire = false;
                PlayerAnimationManager.instance.isBasic = true;
                //PlayerController.instance.KnockBack();
                Invincible();
                LostFire();
            }

            else if (PlayerAnimationManager.instance.isIce == true)
            {
                PlayerAnimationManager.instance.isIce = false;
                PlayerAnimationManager.instance.isBasic = true;
                //PlayerController.instance.KnockBack();
                Invincible();
                LostIce();
            }

            else if (PlayerAnimationManager.instance.isBasic == true && invincibleLength <= 0)
            {
                //Destroy(this.gameObject);
                PlayerController.instance.isDead = true;
                Died();
                Invoke("AfterDeath", 2f);
            }
        }
    }*/

    void Invincible()
    {
        invincibleLength = 2;
    }

    void VisibilityHalf()
    {
        theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .6f);
    }

    void VisibilityFull()
    {
        theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
    }

    void LostFire()
    {
        Instantiate(lostFirePrefab, particlePoint.position, particlePoint.rotation);
        Instantiate(lostFireAnim, particlePoint.position, particlePoint.rotation);
    }

    void LostIce()
    {
        Instantiate(lostIcePrefab, particlePoint.position, particlePoint.rotation);
        Instantiate(lostIceAnim, particlePoint.position, particlePoint.rotation);
    }

    void LostArmor()
    {
        Instantiate(lostArmorPrefab, particlePoint.position, particlePoint.rotation);
        Instantiate(lostArmorAnim, particlePoint.position, particlePoint.rotation);
    }

    void Died()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        capsuleCollider2d.enabled = false;
    }

    void AfterDeath()
    {
        //capsuleCollider2d.enabled = true;
        rb2d.constraints = RigidbodyConstraints2D.None;
        rb2d.constraints = RigidbodyConstraints2D.FreezePositionX;
        //rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb2d.velocity = new Vector2(0f, 30f);
    }
}
