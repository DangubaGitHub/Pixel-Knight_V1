using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawnerController : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] clouds;
    bool isSpawning;

    void Start()
    {
        
    }

    void Update()
    {
        if(!isSpawning)
        {
            isSpawning = true;
            Invoke("StartSpawning", Random.Range(8f, 10f));
        }
        //StartCoroutine(SpawnClouds());
        
    }

    IEnumerator SpawnClouds()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        Debug.Log("Cloud Instanciated");
        Instantiate(clouds[Random.Range(0, clouds.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
    }
    
    void StartSpawning()
    {
        Instantiate(clouds[Random.Range(0, clouds.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        isSpawning = false; 
    }
}
