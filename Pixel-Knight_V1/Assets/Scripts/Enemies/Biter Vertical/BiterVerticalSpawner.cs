using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiterVerticalSpawner : MonoBehaviour
{
    [SerializeField] GameObject biterVerticalPrefab;
    [SerializeField] Transform biterVertivalSpawnPoint;

    float spawnTimerCountdown;
    [SerializeField] float spawnTime;

    void Start()
    {
        spawnTimerCountdown = spawnTime;
    }

    void Update()
    {
        spawnTimerCountdown -= Time.deltaTime;

        if(spawnTimerCountdown <= 0)
        {
            Instantiate(biterVerticalPrefab, biterVertivalSpawnPoint.position, transform.rotation);

            spawnTimerCountdown = spawnTime;
        }


    }
}
