using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInteraction : MonoBehaviour, IInteractable
{
    private BossSpawnManager spawnManager;
    private Boss boss;

    public void Init(BossSpawnManager spawnManager, Boss boss)
    {
        this.spawnManager = spawnManager;
        this.boss = boss;
    }

    public void Use(Player player)
    {
        spawnManager.SpawnGhost(boss, boss.GhostBossIndex);
    }
}
