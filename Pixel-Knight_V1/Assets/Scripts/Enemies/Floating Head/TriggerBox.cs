using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Wall"))
        {
            FloatingHeadController.instance.velocity = -FloatingHeadController.instance.velocity;
        }
    }
}
