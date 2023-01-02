using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderCloudController : MonoBehaviour
{
    //////////////////// Activation ///

    [Header("Activation")]
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] Transform PlayerCheck;
    [SerializeField] float PlayerCheckRadius;
    [SerializeField] bool isActive;

    //////////////////// Lightning ///

    [SerializeField] GameObject lightningBoltPrefab;
    float lightningTimeCounter;
    [SerializeField] float lightningTime;
    [SerializeField] Transform firePoint;

    //////////////////// Animation ///

    const string STILL = "Thunder_Cloud_Still";
    const string ACTIVE = "Thunder_Cloud_Active";

    string currentState;

    //////////////////// Declerations ///

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        lightningTimeCounter = lightningTime;
    }

    void Update()
    {
        isActive = Physics2D.OverlapCircle(PlayerCheck.position, PlayerCheckRadius, PlayerLayer);
        
        if (isActive)
        {
            if (lightningTimeCounter > 1)
            {
                ChangeAnimationState(STILL);
            }

            if(lightningTimeCounter < 1)
            {
                ChangeAnimationState(ACTIVE);
            }

            if (lightningTimeCounter <= 0)
            {
                InstantiateLightning();
                //Invoke("InstantiateLightning", 1f);
                lightningTimeCounter = lightningTime;
            }

            lightningTimeCounter -= Time.deltaTime;
        }

        else
        {
            ChangeAnimationState(STILL);
        }
    }

    void InstantiateLightning()
    {
        Instantiate(lightningBoltPrefab, firePoint.position, firePoint.rotation);
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
