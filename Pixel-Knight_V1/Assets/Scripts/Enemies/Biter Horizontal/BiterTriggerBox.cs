using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiterTriggerBox : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground") ||
            other.CompareTag("Ground 2"))
        {
           
        }
    }
}
