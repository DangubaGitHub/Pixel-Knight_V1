using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives1Up : MonoBehaviour
{

    public bool levelUp;

    const string STILL = "1Up_Still";
    const string EXPAND = "1Up_Expand";

    string currentState;

    Animator anim;

    public static Lives1Up instance;

    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        ChangeAnimationState(STILL);
        levelUp = false;
    }


    void Update()
    {
        if (levelUp)
        {
            ChangeAnimationState(EXPAND);
        }

        else
            ChangeAnimationState(STILL);
    }

    public void EndAnimation()
    {
        levelUp = false;
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
