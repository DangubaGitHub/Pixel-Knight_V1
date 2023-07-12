using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    GameObject player;
    [SerializeField] float velocity;


    const string MAGIC = "Magic_Projectile";
    string currentState;

    public bool hitPlayer;

    Animator anim;
    Rigidbody2D rb2d;
    public static MagicProjectile instance;

    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        ChangeAnimationState(MAGIC);

        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        rb2d.velocity = new Vector2(direction.x, direction.y).normalized * velocity;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation + 180);

        AudioController.instance.PlaySFX(23);                                             /////// SFX //
    }

    void Update()
    {
        if(hitPlayer)
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
