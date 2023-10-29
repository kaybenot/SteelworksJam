using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLastBossFight : MonoBehaviour, ICommandListener
{
    [SerializeField] private BossSpawnManager bossSpawnManager;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private HeadFollow headManager;
    [SerializeField] private AudioSource music;

    public string ListenerName { get; set; } = "LastBoss";

    void Awake()
    {
        music= GetComponent<AudioSource>(); 
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
        music.Play();
        headManager.Teleport(playerSpawnPoint);

        bossSpawnManager.SpawnBoss(0);
        CommandProcessor.SendCommand("Player.EnableGun");
        CommandProcessor.SendCommand("Canvas.ShowWeapons");
        CommandProcessor.SendCommand("Canvas.ShowEnemyHealth");
        CommandProcessor.SendCommand("Canvas.ShowPlayerHealth");
    }

    private void EndLastFight()
    {
        music.Stop();
        bossSpawnManager.DespawnBoss();
        SceneManager.LoadScene(3);
    }
}
