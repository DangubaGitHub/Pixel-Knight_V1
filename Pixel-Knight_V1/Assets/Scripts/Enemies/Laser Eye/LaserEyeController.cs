using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEyeController : MonoBehaviour
{
    ////////////////////////////// Shooting //////////

    [SerializeField] GameObject laserPrefab;
    [SerializeField] Transform firePoint;

    public bool isOpening;

    ////////////////////////////// Activation //////////

    [Header("Activation")]
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] Transform PlayerCheck;
    [SerializeField] float PlayerCheckRadius;
    [SerializeField] bool isActive;

    [SerializeField] GameObject Player;

    ////////////////////////////// Animation Controlls //////////

    const string STILL = "Laser_Eye_Still";
    const string ACTIVE = "Laser_Eye_Active";
    const string OPENING = "Laser_Eye_Opening";

    string currentState;

    ////////////////////////////// Declerations //////////

    Rigidbody2D rb2d;
    Animator anim;
    public static LaserEyeController instance;

    private void Awake()
    {
        instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        isActive = Physics2D.OverlapCircle(PlayerCheck.position, PlayerCheckRadius, PlayerLayer);

        if (isActive)
        {
            if (!isOpening)
            {
                ChangeAnimationState(ACTIVE);
            }

            else if(isOpening)
            {
                ChangeAnimationState(OPENING);
            }
        }

        else
        {
            ChangeAnimationState(STILL);
        }

        Vector3 characterScale = transform.localScale;

        if (Player.transform.position.x <= transform.position.x)
        {
            characterScale.x = 1;
        }

        else if (Player.transform.position.x > transform.position.x)
        {
            characterScale.x = -1;
        }

        transform.localScale = characterScale;
    }

    public void ShootLaser()
    {
        Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
