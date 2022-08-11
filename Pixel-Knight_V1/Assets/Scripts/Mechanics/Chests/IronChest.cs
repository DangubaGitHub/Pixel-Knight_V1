using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronChest : MonoBehaviour
{
    const string CLOSED = "chest_closed";
    const string OPEN = "chest_open";

    string currentState;

    [SerializeField] bool atChest;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        ChangeAnimationState(CLOSED);
    }


    void Update()
    {
        if(atChest)
        {
            ChangeAnimationState(OPEN);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            atChest = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            atChest = false;
        }
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
