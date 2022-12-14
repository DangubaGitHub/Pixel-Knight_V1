using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator anim;

    string currentState;
    const string DESTROY = "fireball_destroy";

    float force = 10;

    [SerializeField] GameObject enemyDeathEffect;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        rb2d.velocity = transform.right * force;
        StartCoroutine(DelayDestroy());
        Invoke("DestroyBall", 3.2f);
    }

    void Update()
    {
        
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(2);
        ChangeAnimationState(DESTROY);
    }

    void DestroyBall()
    {
        Destroy(gameObject);
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            Instantiate(enemyDeathEffect, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
        }
    }
}
