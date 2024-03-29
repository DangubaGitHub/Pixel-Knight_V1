using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] float velocityX;
    GameObject player;
    Rigidbody2D rb2d;
    SpriteRenderer sr;

    public bool hitPlayer;

    SkeletonArcherController skeletonArcherController;
    [SerializeField] GameObject skeletonArcher;

    //public static ArrowController instance;

    private void Awake()
    {
        skeletonArcherController = skeletonArcher.GetComponent<SkeletonArcherController>();
        //instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.sortingOrder = 9;
    }

    void Start()
    {

        float rotation = Mathf.Atan2(0, velocityX) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation + 180);

        if (player.transform.position.x > transform.position.x)
        {
            sr.flipX = true;
        }

        else
        {
            sr.flipX = false;
        }

        ArrowStart();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") ||
            other.CompareTag("Ground") ||
            other.CompareTag("Ground 2") ||
            other.CompareTag("Player"))
        {
            sr.sortingOrder = 5;
            Destroy(gameObject, 0.15f);
        }
    }

    void ArrowStart()
    {
        if (sr.flipX == true)
        {
            rb2d.velocity = new Vector2(velocityX, 0f);
        }

        else if (sr.flipX == false)
        {
            rb2d.velocity = new Vector2(-velocityX, 0f);
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
