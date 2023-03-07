using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealthController.instance.livesCount = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
