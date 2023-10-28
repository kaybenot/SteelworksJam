using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGhost : MonoBehaviour, IInteractable
{
    [SerializeField] private int bossGhostIndex;

    public void Use(Player player)
    {
        string command = "Boss." + bossGhostIndex;
        CommandProcessor.SendCommand(command);
    }

    [ContextMenu("Test")]
    public void Test()
    {
        Use(null);
    }
}
