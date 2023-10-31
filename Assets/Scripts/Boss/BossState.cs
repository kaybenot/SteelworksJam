using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : MonoBehaviour, ICommandListener
{
    [SerializeField] private BossSpawnManager bossSpawnManager;
    [SerializeField] private BossSpawnManager lastBossSpawnManager;
    [SerializeField] private GameObject bossAI;
    [SerializeField] private AudioSource music; 
    public string ListenerName { get; set; } = "Boss";

    public static int CurrentBossIndex => currentBossIndex;

    private static int currentBossIndex = -1;

    void Awake()
    {
        music = GetComponent<AudioSource>();
        CommandProcessor.RegisterListener(this);
    }

    public void ProcessCommand(string command, List<string> parameters)
    {
        if (command is "End" or "Restart")
        {
            if (command == "End")
                EndFight();
            CommandProcessor.SendCommand("Player DisableGun");
            CommandProcessor.SendCommand("Canvas HideWeapons");
            CommandProcessor.SendCommand("Canvas HideEnemyHealth");
            CommandProcessor.SendCommand("Canvas HidePlayerHealth");
            music.Stop();
            if (currentBossIndex != -1)
            {
                CommandProcessor.SendCommand($"ArenaManager Hide {currentBossIndex}");
            }

            if (command == "Restart")
            {
                bossSpawnManager.RealDespawnBoss();
                lastBossSpawnManager.RealDespawnBoss();
                music.Stop();
                if (currentBossIndex != -1) bossSpawnManager.bossDatas[currentBossIndex].ghostPoint.gameObject.SetActive(true);
            }
            
            currentBossIndex = -1;
        }
        else if (IsInteger(command, out int index))
        {
            Debug.Log($"Spawned boss with index {index}");
            music.Play();
            bossSpawnManager.SpawnBoss(index);
            CommandProcessor.SendCommand("Player EnableGun");
            CommandProcessor.SendCommand("Canvas ShowWeapons");
            bossAI.SetActive(false);
            if (parameters.Count > 0)
                CommandProcessor.SendCommand($"Player PushPlayer {parameters[0]}");
            else
                CommandProcessor.SendCommand("Player PushPlayer");

            CommandProcessor.SendCommand("Canvas ShowEnemyHealth");
            CommandProcessor.SendCommand("Canvas ShowPlayerHealth");
            CommandProcessor.SendCommand($"Canvas SetEnemyName {bossSpawnManager.bossDatas[index].bossName}");
            CommandProcessor.SendCommand($"ArenaManager Show {index}");
            currentBossIndex = index;
        }
    }

    private void EndFight()
    {
        bossSpawnManager.DespawnBoss();
    }

    public bool IsInteger(string input, out int index)
    {
        var isInteger = int.TryParse(input, out index);
        return isInteger;
    }
}
