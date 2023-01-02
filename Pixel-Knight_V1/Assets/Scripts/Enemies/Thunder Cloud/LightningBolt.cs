using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    //////////////////// Movement ///

    [SerializeField] float fallSpeed;

    //////////////////// Lightning Hit ///

    [SerializeField] GameObject lightningHitPrefab;

    //////////////////// Declerations ///

    SpriteRenderer sr;
    CapsuleCollider2D capsulCollider;
    Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        capsulCollider = GetComponent<CapsuleCollider2D>();
    }

    void Start()
    {
        rb2d.velocity = new Vector2(0f, fallSpeed);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Ground")
        {
            Instantiate(lightningHitPrefab, transform.position, transform.rotation);
            capsulCollider.enabled = false;
            sr.enabled = false;
            Invoke("Destroy", 0.1f);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
    
