using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardShooting : MonoBehaviour
{
    [SerializeField] GameObject magicProjectile;
    [SerializeField] Transform projectilePos;

    float timer;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 2)
        {
            timer = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(magicProjectile, projectilePos.position, Quaternion.identity);
    }
}
