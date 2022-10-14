using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [SerializeField] float timeUntilDestroy;

    void Start()
    {
        
    }

    void Update()
    {
        Destroy(gameObject, timeUntilDestroy);
    }
}
