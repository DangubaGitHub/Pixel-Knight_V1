using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTriggerController : MonoBehaviour
{
    SkeletonWarriorController skeletonWarriorController;
    [SerializeField] GameObject skeletonWarriorParent;

    private void Awake()
    {
        skeletonWarriorController = skeletonWarriorParent.GetComponent<SkeletonWarriorController>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            skeletonWarriorController.ChangeDirection();
        }
    }
}
