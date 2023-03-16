using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour
{
    Rigidbody2D rb2d;

    float force = 25;

    [SerializeField] GameObject enemyDeathEffect;
    [SerializeField] GameObject bulletDestroyAnimation;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb2d.velocity = transform.right * force;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
        {
            Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.CompareTag("Wall"))
        {
            Instantiate(bulletDestroyAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
