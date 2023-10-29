using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBoss : Boss
{
    [SerializeField] private float timeToSwitch;
    [SerializeField] private List<BossData> otherBosses;

    public override void Init(Transform spawnPoint, BossSpawnManager spawnManager, int ghostBossIndex)
    {
        this.spawnManager = spawnManager;
        this.spawnPoint = spawnPoint;
        this.ghostBossIndex = ghostBossIndex;
        bossInteraction.Init(spawnManager, this);
        bossInteraction.gameObject.SetActive(false);
        baseSprite = spriteRenderer.sprite;
        currentHealth = startingHealth;

        foreach (var boss in otherBosses)
        {
            boss.bossShotManager.Init(Attack);
            foreach (var action in boss.specialActions)
            {
                action.Init(this);
            }
        }
    }

    public void Update()
    {
        
    }

    public void SwitchBoss()
    {

    }

    [Serializable]
    public class BossData
    {
        public BossShotManager bossShotManager;
        public List<BaseBossAction> specialActions;
    }
}
