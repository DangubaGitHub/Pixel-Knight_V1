using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiterFlyingController : MonoBehaviour
{
    ////////////////////////////// Activation //////////

    [Header("Activation")]
    [SerializeField] bool isActive;

    [SerializeField] GameObject Player;

    ////////////////////////////// Animation Controlls //////////

    const string STILL = "Biter_Flying_Stationary_Still";
    const string MOVE = "Biter_Flying_Stationary_Move";

    string currentState;

    ////////////////////////////// Declerations //////////

    Rigidbody2D rb2d;
    Animator anim;
    public static BiterFlyingController instance;

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
        if (isActive)
        {
            ChangeAnimationState(MOVE);
        }

        else
        {
            ChangeAnimationState(STILL);
        }

        Vector3 characterScale = transform.localScale;

        if (Player.transform.position.x <= transform.position.x)
        {
            characterScale.x = -1;
        }

        else if (Player.transform.position.x > transform.position.x)
        {
            characterScale.x = 1;
        }

        transform.localScale = characterScale;
    }

    private void OnBecameVisible()
    {
        isActive = true;
    }

    private void OnBecameInvisible()
    {
        isActive = false;
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
