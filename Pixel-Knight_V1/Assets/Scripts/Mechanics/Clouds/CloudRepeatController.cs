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
        /*float newPosition = Mathf.Repeat(Time.time * scrollSpeed, 264);
        transform.position = startPosition + Vector2.right * newPosition;*/

        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);   

        if (transform.position.x < -260f)
        {
            transform.position = startPosition;
        }

    }
}
