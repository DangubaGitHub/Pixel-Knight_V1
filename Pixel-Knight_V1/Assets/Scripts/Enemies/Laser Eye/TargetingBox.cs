using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingBox : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            LaserEyeController.instance.isOpening = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LaserEyeController.instance.isOpening = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Wall" ||
            other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
