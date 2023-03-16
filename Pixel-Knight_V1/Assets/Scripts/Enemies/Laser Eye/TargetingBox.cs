using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingBox : MonoBehaviour
{
    LaserEyeController laserEyeController;
    [SerializeField] GameObject LaserEye;

    private void Awake()
    {
        laserEyeController = LaserEye.GetComponent<LaserEyeController>();
    }

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
            laserEyeController.isOpening = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            laserEyeController.isOpening = false;
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
