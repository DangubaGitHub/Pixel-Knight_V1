using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKnightController : MonoBehaviour
{
    [SerializeField] bool isActive;
    public bool isAlive;
    bool isArmor;
    bool isNoArmor;

    //////////////////////////////////////////////////////////// Movement //////////

    public float velocityX;
    public float velocityY;

    public bool isGrounded;

    //////////////////////////////////////////////////////////// Declerations //////////

    Rigidbody2D rb2d;
    public static SlimeKnightController instance;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (isActive)
        {
            if (isAlive)
            {
                if (isArmor)
                {

                }

                else if (isNoArmor)
                {

                }
            }

            else if (!isAlive)
            {

            }
        }

        else
        {

        }

    }
}
