using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKnightHealthController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (SlimeKnightAnimationController.instance.zero_Hit)
            {
                SlimeKnightAnimationController.instance.zero_Hit = false;
                SlimeKnightAnimationController.instance.one_Hit = true;
            }

            else if (SlimeKnightAnimationController.instance.one_Hit)
            {
                SlimeKnightAnimationController.instance.one_Hit = false;
                SlimeKnightAnimationController.instance.two_Hit = true;
            }

            else if (SlimeKnightAnimationController.instance.two_Hit)
            {
                SlimeKnightAnimationController.instance.two_Hit = false;
                SlimeKnightAnimationController.instance.three_Hit = true;
            }
        }
    }
}
