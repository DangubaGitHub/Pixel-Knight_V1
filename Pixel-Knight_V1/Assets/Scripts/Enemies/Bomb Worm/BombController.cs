using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] Transform explosionPoint;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Explosion()
    {
        Instantiate(explosionPrefab, explosionPoint.position, Quaternion.identity);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
