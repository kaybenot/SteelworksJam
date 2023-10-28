using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : MonoBehaviour, ICommandListener
{
    [SerializeField] private BossSpawnManager bossSpawnManager;
    public string ListenerName { get; set; } = "Boss";

    void Awake()
    {
        CommandProcessor.RegisterListener(this);
    }

    public void ProcessCommand(string command, List<string> parameters)
    {
        if (command == "End")
        {
            EndFight();
        }

        if (IsInteger(command, out int index))
        {
            Debug.Log($"Spawned boss with index {index}");
            bossSpawnManager.SpawnBoss(index);
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
