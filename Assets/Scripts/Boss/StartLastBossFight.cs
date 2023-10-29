using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLastBossFight : MonoBehaviour, ICommandListener
{
    [SerializeField] private BossSpawnManager bossSpawnManager;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private HeadFollow headManager;

    public string ListenerName { get; set; } = "LastBoss";

    void Awake()
    {
        CommandProcessor.RegisterListener(this);
    }

    public void ProcessCommand(string command, List<string> parameters)
    {
        if (command == "Start")
        {
            StartLastFight();
        }
        else if (command == "End")
        {
            EndLastFight();
        }
    }

    public void StartLastFight()
    {
        headManager.Teleport(playerSpawnPoint);

        bossSpawnManager.SpawnBoss(0);
        CommandProcessor.SendCommand("Player.EnableGun");
        CommandProcessor.SendCommand("Canvas.ShowWeapons");
        CommandProcessor.SendCommand("Canvas.ShowEnemyHealth");
        CommandProcessor.SendCommand("Canvas.ShowPlayerHealth");
    }

    private void EndLastFight()
    {
        bossSpawnManager.DespawnBoss();
    }
}
