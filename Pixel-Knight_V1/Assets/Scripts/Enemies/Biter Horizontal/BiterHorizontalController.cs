using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiterHorizontalController : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator anim;

    ////////////////////  ANIMATIONS  ///

    const string BITING = "Biter_Horizontal_Still";
    const string STILL = "Biter_Horizontal_Biting";

    string currentState;

    ////////////////////  Activation  ///

    [Header("Activation")]
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] Transform PlayerCheck;
    [SerializeField] float PlayerCheckRadius;
    [SerializeField] bool isActive;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        ChangeAnimationState(STILL);
    }

    void Update()
    {
        isActive = Physics2D.OverlapCircle(PlayerCheck.position, PlayerCheckRadius, PlayerLayer);

        if(isActive)
        {

        }

        else
        {
            ChangeAnimationState(STILL);
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
