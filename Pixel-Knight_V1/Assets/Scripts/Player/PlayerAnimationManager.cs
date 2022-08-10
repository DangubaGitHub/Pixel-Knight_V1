using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{


    string currentState;

    //Basic Player

    const string BP_IDLE = "bp_idle";
    const string BP_RUN = "bp_run";
    const string BP_JUMP = "bp_jump";
    const string BP_FALL = "bp_fall";

    //Armor Player

    //Fire Player

    //Ice Player

    

    public static PlayerAnimationManager instance;

    Animator anim;
    Rigidbody2D rb2d;

    private void Awake()
    {
        instance = this;

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {

        if (PlayerController.instance.isGrounded)
        {
            if (rb2d.velocity.x == 0)
            {
                ChangeAnimationState(BP_IDLE);
            }

            if (rb2d.velocity.x != 0)
            {
                ChangeAnimationState(BP_RUN);
            }
        }



        if (rb2d.velocity.y > 0f && PlayerController.instance.isGrounded == false)
        {
            ChangeAnimationState(BP_JUMP);
        }

        if (rb2d.velocity.y < 0f && PlayerController.instance.isGrounded == false)
        {
            ChangeAnimationState(BP_FALL);
        }
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
