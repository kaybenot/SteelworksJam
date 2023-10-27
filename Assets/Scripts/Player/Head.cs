using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFollow : MonoBehaviour
{
    public Player Player;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.HeadMountPoint.transform.position,
            Player.MovementSpeed * Time.deltaTime);
    }
}
