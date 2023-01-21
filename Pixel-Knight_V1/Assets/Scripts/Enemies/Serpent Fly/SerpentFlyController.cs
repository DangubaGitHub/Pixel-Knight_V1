using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentFlyController : MonoBehaviour
{
    [SerializeField] Transform rotationCenter;
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationRadius;

    float positionX;
    float positionY;
    float angle;

    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {

    }

    void Update()
    {
        positionX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        positionY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;
        transform.position = new Vector2(positionX, positionY);
        angle = angle + Time.deltaTime * rotationSpeed;

        if (angle >= 360)
        {
            angle = 0;
        }

        if (positionY < transform.parent.position.y)
        {
            sr.flipX = false;
        }

        else if (positionY > transform.parent.position.y)
        {
            sr.flipX = true;
        }
    }
}
