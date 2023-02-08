using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEyeActivation : MonoBehaviour
{
    private void OnBecameVisible()
    {
        LaserEyeController.instance.isActive = true;
    }

    private void OnBecameInvisible()
    {
        LaserEyeController.instance.isActive = false;
    }
}
