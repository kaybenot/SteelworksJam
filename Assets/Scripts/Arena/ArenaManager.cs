using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour, ICommandListener
{
    [SerializeField] private List<Arena> arenas;

    public string ListenerName { get; set; } = "ArenaManager";

    private void Awake()
    {
        CommandProcessor.RegisterListener(this);
    }

    public void ProcessCommand(string command, List<string> parameters)
    {
        switch (command)
        {
            case "Show":
                if (parameters.Count <= 0)
                    break;
                arenas[int.Parse(parameters[0])].Show();
                break;
            case "Hide":
                if (parameters.Count <= 0)
                    break;
                arenas[int.Parse(parameters[0])].Hide();
                break;
            
            default:
                Debug.LogWarning($"Unhandled command {command} in listener {ListenerName}");
                break;
        }
    }
}
