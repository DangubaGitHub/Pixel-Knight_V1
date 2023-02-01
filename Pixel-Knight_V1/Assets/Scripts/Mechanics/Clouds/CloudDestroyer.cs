using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDestroyer : MonoBehaviour
{

    [SerializeField] bool cloudContact;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cloud"))
        {
            cloudContact = true;
            Destroy(other.gameObject);
        }
    }
}
