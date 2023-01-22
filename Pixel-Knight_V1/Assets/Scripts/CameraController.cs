using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform parallax_1;
    [SerializeField] float velocity1;

    [SerializeField] Transform parallax_2;
    [SerializeField] float velocity2;

    [SerializeField] Transform parallax_3;
    [SerializeField] float velocity3;

    float lastPositionX;

    void Start()
    {
        lastPositionX = transform.position.x;
    }

    void Update()
    {
        float amountToMoveX = transform.position.x - lastPositionX;

        parallax_3.position += new Vector3(amountToMoveX * velocity3, 0, 0);
        parallax_2.position += new Vector3(amountToMoveX * velocity2, 0, 0);
        parallax_1.position += new Vector3(amountToMoveX * velocity1, 0, 0);

        lastPositionX = transform.position.x;
    }
}
