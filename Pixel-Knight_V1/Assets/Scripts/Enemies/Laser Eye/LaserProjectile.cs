using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    [SerializeField] float velocity;
    GameObject player;
    Rigidbody2D rb2d;

    public bool hitPlayer;

    public static LaserProjectile instance;

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        

        float rotation = Mathf.Atan2(0, velocity) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation + 180);

        if (player.transform.position.x > transform.position.x || player.transform.position.x == transform.position.x)
        {
            rb2d.velocity = new Vector2(velocity, 0f);
        }

        else
        {
            rb2d.velocity = new Vector2(-velocity, 0f);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Wall" ||
            other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (hitPlayer)
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
