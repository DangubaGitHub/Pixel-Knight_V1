using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public float shortInvulnerability;

    public float invincibleLength;

    public static PlayerHealthController instance;

    [SerializeField] Transform particlePoint;

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

    public int livesCount;

    private void Awake()
    {
        instance = this;
        theSR = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        capsuleCollider2d = GetComponent<Collider2D>();
    }

    void Start()
    {
        shortInvulnerability = 0;

        if (PlayerPrefs.HasKey("Lives Save"))
        {
            livesCount = PlayerPrefs.GetInt("Lives Save");
            UIController.instance.LivesUpdate();
        }

        else
        {
            livesCount = 5;
            UIController.instance.LivesUpdate();   
        }
    }

    void Update()
    {
        if (shortInvulnerability > 0)
        {
            shortInvulnerability -= Time.deltaTime;
        }

        if(invincibleLength > 0)
        {
            invincibleLength -= Time.deltaTime;
            VisibilityHalf();
        }
        else if(invincibleLength <= 0)
        {
            VisibilityFull();
        }

        if(livesCount <= 0) 
        {
            PlayerPrefs.DeleteKey("Lives Save");
        
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Element Burst"))
        {
            if (PlayerAnimationManager.instance.isArmor == true)
            {
                Invoke("ChangeState", 0.1f);
                Invincible();
                LostArmor();
            }

            else if (PlayerAnimationManager.instance.isFire == true)
            {
                Invoke("ChangeState", 0.1f);
                Invincible();
                LostFire();
            }

            else if (PlayerAnimationManager.instance.isIce == true)
            {
                Invoke("ChangeState", 0.1f);
                Invincible();
                LostIce();
            }

            else if (PlayerAnimationManager.instance.isBasic == true && invincibleLength <= 0)
            {
                PlayerController.instance.isDead = true;
                Died();
                Invoke("AfterDeath", 2f);
            }
        }

        if (other.CompareTag("Skeleton Arrow"))
        {
            if (PlayerAnimationManager.instance.isArmor == true)
            {
                Invoke("ChangeState", 0.1f);
                Invincible();
                LostArmor();
                Destroy(other.gameObject);
            }

            else if (PlayerAnimationManager.instance.isFire == true)
            {
                Invoke("ChangeState", 0.1f);
                Invincible();
                LostFire();
                Destroy(other.gameObject);
            }

            else if (PlayerAnimationManager.instance.isIce == true)
            {
                Invoke("ChangeState", 0.1f);
                Invincible();
                LostIce();
                Destroy(other.gameObject);
            }

            else if (PlayerAnimationManager.instance.isBasic == true && invincibleLength > 0)
            {
                Destroy(other.gameObject);
            }

            else if (PlayerAnimationManager.instance.isBasic == true && invincibleLength <= 0)
            {
                //ArrowController.instance.hitPlayer = true;
                PlayerController.instance.isDead = true;
                Died();
                Invoke("AfterDeath", 2f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy" || 
           other.gameObject.tag == "Spikes" ||
           other.gameObject.tag == "Enemy Invulnerable Damaging" || 
           other.gameObject.tag == "Enemy Invulnerable Bounce" ||
           other.gameObject.tag == "Slime" ||
           other.gameObject.tag == "Slime Red" ||
           other.gameObject.tag == "Slime Blue" ||
           other.gameObject.tag == "Skeleton Sword" ||
           other.gameObject.tag == "Bomb Worm" ||
           other.gameObject.tag == "Summer Boss")
        {
            if (shortInvulnerability <= 0)
            {
                if (PlayerAnimationManager.instance.isArmor == true)
                {
                    Invoke("ChangeState", 0.1f);
                    Invincible();
                    LostArmor();
                }

                else if (PlayerAnimationManager.instance.isFire == true)
                {
                    Invoke("ChangeState", 0.1f);
                    Invincible();
                    LostFire();
                }

                else if (PlayerAnimationManager.instance.isIce == true)
                {
                    Invoke("ChangeState", 0.1f);
                    Invincible();
                    LostIce();
                }

                else if (PlayerAnimationManager.instance.isBasic == true && invincibleLength <= 0)
                {
                    PlayerController.instance.isDead = true;
                    Died();
                    Invoke("AfterDeath", 2f);
                    
                }
            }
        }

        if(other.gameObject.tag == "Element Spikes")
        {
            if (PlayerAnimationManager.instance.isArmor == true)
            {
                Invoke("ChangeState", 0.1f);
                Invincible();
                LostArmor();
                PlayerController.instance.BounceOnEnemy();
            }

            else if (PlayerAnimationManager.instance.isFire == true)
            {
                Invoke("ChangeState", 0.1f);
                Invincible();
                LostFire();
                PlayerController.instance.BounceOnEnemy();
            }

            else if (PlayerAnimationManager.instance.isIce == true)
            {
                Invoke("ChangeState", 0.1f);
                Invincible();
                LostIce();
                PlayerController.instance.BounceOnEnemy();
            }

            else if (PlayerAnimationManager.instance.isBasic == true && invincibleLength > 0)
            {
                PlayerController.instance.BounceOnEnemy();
            }

            else if (PlayerAnimationManager.instance.isBasic == true && invincibleLength <= 0)
            {
                PlayerController.instance.isDead = true;
                Died();
                Invoke("AfterDeath", 2f);
                
            }
        }

        if (other.gameObject.tag == "Laser Projectile")
        {
            if (PlayerAnimationManager.instance.isArmor == true)
            {
                Invoke("ChangeState", 0.1f);
                Invincible();
                LostArmor();
                Destroy(other.gameObject);
            }

            else if (PlayerAnimationManager.instance.isFire == true)
            {
                Invoke("ChangeState", 0.1f);
                Invincible();
                LostFire();
                Destroy(other.gameObject);
            }

            else if (PlayerAnimationManager.instance.isIce == true)
            {
                Invoke("ChangeState", 0.1f);
                Invincible();
                LostIce();
                Destroy(other.gameObject);
            }

            else if (PlayerAnimationManager.instance.isBasic == true && invincibleLength > 0)
            {
                Destroy(other.gameObject);
            }

            else if (PlayerAnimationManager.instance.isBasic == true && invincibleLength <= 0)
            {
                LaserProjectile.instance.hitPlayer = true;
                PlayerController.instance.isDead = true;
                Died();
                Invoke("AfterDeath", 2f);
                
            }
        }

        if (other.gameObject.tag == "Magic Projectile")
        {
            if (PlayerAnimationManager.instance.isArmor == true)
            {
                Invoke("ChangeState", 0.1f);
                Invincible();
                LostArmor();
                Destroy(other.gameObject);
            }

            else if (PlayerAnimationManager.instance.isFire == true)
            {
                Invoke("ChangeState", 0.1f);
                Invincible();
                LostFire();
                Destroy(other.gameObject);
            }

            else if (PlayerAnimationManager.instance.isIce == true)
            {
                Invoke("ChangeState", 0.1f);
                Invincible();
                LostIce();
                Destroy(other.gameObject);
            }

            else if (PlayerAnimationManager.instance.isBasic == true && invincibleLength > 0)
            {
                Destroy(other.gameObject);
            }

            else if (PlayerAnimationManager.instance.isBasic == true && invincibleLength <= 0)
            {
                MagicProjectile.instance.hitPlayer = true;
                PlayerController.instance.isDead = true;
                Died();
                Invoke("AfterDeath", 2f);
                Invoke("CallPauseMenuAfterDeath", 4); /////////////////////////////////// Testing //////////
            }
        }

        if (other.gameObject.tag == "Enemy Wizard")
        {
            if (PlayerAnimationManager.instance.isArmor == true)
            {
                Invoke("ChangeState", 0.1f);
                Invincible();
                LostArmor();
                Destroy(other.gameObject);
            }

            else if (PlayerAnimationManager.instance.isFire == true)
            {
                Invoke("ChangeState", 0.1f);
                Invincible();
                LostFire();
                Destroy(other.gameObject);
            }

            else if (PlayerAnimationManager.instance.isIce == true)
            {
                Invoke("ChangeState", 0.1f);
                Invincible();
                LostIce();
                Destroy(other.gameObject);
            }

            else if (PlayerAnimationManager.instance.isBasic == true && invincibleLength <= 0)
            {
                //WizardController.instance.isVisible = true;
                PlayerController.instance.isDead = true;
                Died();
                Invoke("AfterDeath", 2f);
                Invoke("CallPauseMenuAfterDeath", 4); /////////////////////////////////// Testing //////////
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////// Stomper Code Untested /////////

        if (other.gameObject.tag == "Ground" && other.gameObject.tag == "Stomper")
        {
            if (PlayerAnimationManager.instance.isArmor == true)
            {
                Invoke("ChangeState", 0.1f);
                LostArmor();
                Died();
                Invoke("AfterDeath", 2f);
                Invoke("CallPauseMenuAfterDeath", 4); /////////////////////////////////// Testing //////////
            }

            else if (PlayerAnimationManager.instance.isFire == true)
            {
                Invoke("ChangeState", 0.1f);
                LostFire();
                Died();
                Invoke("AfterDeath", 2f);
                Invoke("CallPauseMenuAfterDeath", 4); /////////////////////////////////// Testing //////////
            }

            else if (PlayerAnimationManager.instance.isIce == true)
            {
                Invoke("ChangeState", 0.1f);
                LostIce();
                Died();
                Invoke("AfterDeath", 2f);
                Invoke("CallPauseMenuAfterDeath", 4); /////////////////////////////////// Testing //////////
            }

            else if (PlayerAnimationManager.instance.isBasic == true && invincibleLength <= 0)
            {
                //WizardController.instance.isVisible = true;
                PlayerController.instance.isDead = true;
                Died();
                Invoke("AfterDeath", 2f);
            }
        }
    }

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
        TakeLive();

        PlayerPrefs.DeleteKey("Armor Power");
        PlayerPrefs.DeleteKey("Fire Power");
        PlayerPrefs.DeleteKey("Ice Power");
    }

    void AfterDeath()
    {
        rb2d.constraints = RigidbodyConstraints2D.None;
        rb2d.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb2d.velocity = new Vector2(0f, 30f);
    }

    public void TakeLive()
    {
        AudioController.instance.PlaySFX(16);                              /////// SFX //

        livesCount--;

        UIController.instance.LivesUpdate();

        PlayerPrefs.SetInt("Lives Save", livesCount);
    }

    public void AddLive()
    {
        livesCount++;

        Lives1Up.instance.levelUp = true;

        Invoke("DelayAddLive", .5f);
    }

    void DelayAddLive()
    {
        AudioController.instance.PlaySFX(17);                              /////// SFX //
        UIController.instance.LivesUpdate();
        PlayerPrefs.SetInt("Lives Save", livesCount);
    }

    void CallPauseMenuAfterDeath()
    {
        PauseMenu.instance.PauseUnpause();
    }

    void ChangeState()
    {
        AudioController.instance.PlaySFX(14);                              /////// SFX //

        PlayerAnimationManager.instance.ChangeToBasic();
    }
}
