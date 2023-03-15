using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHeadActivation : MonoBehaviour
{
    FloatingHeadController floatingHeadController;
    [SerializeField] GameObject floatingHeadParent;

    private void Awake()
    {
        floatingHeadController = floatingHeadParent.GetComponent<FloatingHeadController>();
    }

    private void OnBecameVisible()
    {
        floatingHeadController.isActive = true;
    }

    private void OnBecameInvisible()
    {
        floatingHeadController.isActive = false;
    }
}
