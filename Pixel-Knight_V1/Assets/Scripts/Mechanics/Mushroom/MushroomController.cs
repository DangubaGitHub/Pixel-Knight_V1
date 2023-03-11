using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public bool mushroomIsActive;

    //////////////////// Animation ///

    const string STILL = "Mushroom_Still";
    const string BOUNCE = "Mushroom_Bounce";

    string currentState;

    //////////////////// Declerations ///

    //public static MushroomController instance;

    Animator anim;

    private void Awake()
    {
        //instance = this;
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(mushroomIsActive)
        {
            ChangeAnimationState(BOUNCE);

            Invoke("DeactivateMuchroom", .2f);
        }

        else
        {
            ChangeAnimationState(STILL);
        }
    }

    void DeactivateMuchroom()
    {
        mushroomIsActive = false;
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mushroomIsActive = true;
        }
    }
}
