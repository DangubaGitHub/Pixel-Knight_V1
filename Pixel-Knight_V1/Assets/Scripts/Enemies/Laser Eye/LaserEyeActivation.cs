using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEyeActivation : MonoBehaviour
{
    LaserEyeController laserEyeController;
    [SerializeField] GameObject LaserEye;

    private void Awake()
    {
        laserEyeController = LaserEye.GetComponent<LaserEyeController>();
    }

    private void OnBecameVisible()
    {
        laserEyeController.isActive = true;
    }

    private void OnBecameInvisible()
    {
        laserEyeController.isActive = false;
    }
}
