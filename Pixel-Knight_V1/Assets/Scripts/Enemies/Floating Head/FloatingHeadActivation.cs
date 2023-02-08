using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHeadActivation : MonoBehaviour
{
    private void OnBecameVisible()
    {
        FloatingHeadController.instance.isActive = true;
    }

    private void OnBecameInvisible()
    {
        FloatingHeadController.instance.isActive = false;
    }
}
