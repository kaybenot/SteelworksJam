using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossPlayerMover : MonoBehaviour
{
    [SerializeField] private Transform playerTeleportPoint;

    private GameObject player;

    public void TeleportPlayer()
    {
        player = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();
        if (player != null)
        {
            player.transform.position = playerTeleportPoint.position;
            player.transform.rotation = playerTeleportPoint.rotation;
        }
    }
}
