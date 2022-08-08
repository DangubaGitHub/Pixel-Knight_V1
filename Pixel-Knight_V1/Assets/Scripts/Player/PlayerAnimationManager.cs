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

    private void Awake()
    {
        instance = this;

        anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
