using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerSpikeController : MonoBehaviour
{
    //////////////////// Movement ///

    [SerializeField] float moveSpeed;


    //////////////////// Activation ///

    [Header("Activation")]
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] Transform PlayerCheck;
    [SerializeField] float PlayerCheckRadius;
    [SerializeField] bool isActive;
    [SerializeField] bool foundDirection;

    [SerializeField] GameObject Player;

    //////////////////// Animation ///

    const string STILL = "Crawler_Spike_Still";
    const string WALK = "Crawler_Spike_Walk";
    const string DOWN = "Crawler_Spike_Crouch_Down";
    const string UP = "Crown_Spike_Crouch_Up";

    string currentState;

    //////////////////// Declerations ///

    Rigidbody2D rb2d;
    Animator anim;

    private void Awake()
    {
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

        }

        else
        {
            ChangeAnimationState(STILL);
        }
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
