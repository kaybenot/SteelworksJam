using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform player;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
            Debug.LogAssertion("Billboard effect could not locate Player by tag!");
    }

    private void Update()
    {
        transform.LookAt(player);
        var angles = transform.eulerAngles;
        angles.x = 90f;
        transform.eulerAngles = angles;
    }
}
