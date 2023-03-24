using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuParallex : MonoBehaviour
{

    [SerializeField] float parallaxSpeed;
    [SerializeField] Vector3 startPosition;
 
    void Start()
    {
        startPosition = transform.position;
    }

 
    void Update()
    {
        transform.Translate(Vector3.left * parallaxSpeed * Time.deltaTime);

        if (transform.position.x <= -128f)
        {
            transform.position = startPosition;
        }
    }
}
