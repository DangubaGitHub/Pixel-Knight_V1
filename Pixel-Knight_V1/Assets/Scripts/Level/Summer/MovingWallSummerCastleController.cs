using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWallSummerCastleController : MonoBehaviour
{
    Rigidbody2D rb2d;
    public static MovingWallSummerCastleController instance;

    CapsuleCollider2D capsuleCollider;

    //////////////////////////////////////////////////////////// Entrance //////////

    [SerializeField] bool entrance;
    public bool isInside;
    [SerializeField] float entranceVelocityY;

    //////////////////////////////////////////////////////////// Exit //////////

    [SerializeField] bool exit;
    [SerializeField] bool bossIsDead;
    [SerializeField] float exitVelocityY;

    private void Awake()
    {
        instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (entrance)
        {
            capsuleCollider.enabled = capsuleCollider.enabled;

            if (isInside)
            {
                EntranceMovingDown();

                Invoke("StopMovement", 1.1f);
            }
        }

        if (exit)
        {
            if (bossIsDead)
            {
                ExitMovingUp();

                Invoke("StopMovement", 1.1f);
            }
        }
    }

    void EntranceMovingDown()
    {
        rb2d.velocity = new Vector2(0, -entranceVelocityY);
    }

    void ExitMovingUp()
    {
        rb2d.velocity = new Vector2(0, exitVelocityY);
    }

    void StopMovement()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = true;
            SlimeKnightController.instance.isActive = true;
            capsuleCollider.enabled = !capsuleCollider.enabled;
        }
    }
}
