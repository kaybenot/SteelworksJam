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
            CommandProcessor.SendCommand("Player.DisableGun");
            CommandProcessor.SendCommand("Canvas.HideWeapons");
            CommandProcessor.SendCommand("Canvas.HideEnemyHealth");
        }
        else if (IsInteger(command, out int index))
        {
            Debug.Log($"Spawned boss with index {index}");
            bossSpawnManager.SpawnBoss(index);
            CommandProcessor.SendCommand("Player.EnableGun");
            CommandProcessor.SendCommand("Canvas.ShowWeapons");
            if (parameters.Count > 0)
                CommandProcessor.SendCommand($"Player.PushPlayer {parameters[0]}");
            else
                CommandProcessor.SendCommand("Player.PushPlayer");
            CommandProcessor.SendCommand("Canvas.ShowEnemyHealth");
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
