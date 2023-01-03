using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    Vector3 playerPosition;
    [SerializeField] float velocity;


    const string MAGIC = "Magic_Projectile";
    string currentState;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        playerPosition = FindObjectOfType<PlayerController>().transform.position;
        ChangeAnimationState(MAGIC);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, velocity * Time.deltaTime);

        if(transform.position == playerPosition)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
