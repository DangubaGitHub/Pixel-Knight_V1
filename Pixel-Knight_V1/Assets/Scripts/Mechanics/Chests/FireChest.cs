using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireChest : MonoBehaviour
{
    const string CLOSED = "Chest_Fire_Closed";
    const string OPEN = "Chest_Fire_Open";

    string currentState;

    [SerializeField] bool atChest;
    [SerializeField] bool isOpen;

    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject firePopOut;

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
        if (atChest)
        {
            if (!isOpen)
            {
                ChestPopOut();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            atChest = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            atChest = false;
        }
    }

    void ChestPopOut()
    {
        AudioController.instance.PlaySFX(18);                              /////// SFX //
        ChangeAnimationState(OPEN);
        isOpen = true;
        GameObject item = Instantiate(firePopOut, spawnPoint.position, spawnPoint.rotation);
        PlayerAnimationManager.instance.ChangeToFire();
        Destroy(item, 1f);
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
