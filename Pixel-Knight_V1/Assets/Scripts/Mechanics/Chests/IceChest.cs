using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceChest : MonoBehaviour
{
    const string CLOSED = "chest_closed";
    const string OPEN = "chest_open";

    string currentState;

    [SerializeField] bool atChest;
    [SerializeField] bool isOpen;

    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject icePopOut;

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
        ChangeAnimationState(OPEN);
        isOpen = true;
        GameObject item = Instantiate(icePopOut, spawnPoint.position, spawnPoint.rotation);
        PlayerAnimationManager.instance.isArmor = false;
        PlayerAnimationManager.instance.isBasic = false;
        PlayerAnimationManager.instance.isFire = false;
        PlayerAnimationManager.instance.isIce = true;
        Destroy(item, 1f);
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
