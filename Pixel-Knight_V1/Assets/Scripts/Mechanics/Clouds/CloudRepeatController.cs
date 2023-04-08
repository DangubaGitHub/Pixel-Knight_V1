using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRepeatController : MonoBehaviour
{
    [SerializeField] float scrollSpeed;
    Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);   

        if (transform.position.x < -260f)  
        {
            transform.position = startPosition;
        }
    }

}
