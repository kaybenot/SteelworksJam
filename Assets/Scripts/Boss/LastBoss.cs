using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LastBoss : Boss
{
    [SerializeField] private float timeToSwitch = 5f;
    [SerializeField] private List<BossData> otherBosses;

    private float nextSwitch = 0.0f;

    private int currentIndex;

    public override void Init(Transform spawnPoint, BossSpawnManager spawnManager, int ghostBossIndex)
    {
        this.spawnManager = spawnManager;
        this.spawnPoint = spawnPoint;
        this.ghostBossIndex = ghostBossIndex;
        bossInteraction.Init(spawnManager, this);
        bossInteraction.gameObject.SetActive(false);
        baseSprite = spriteRenderer.sprite;
        currentHealth = startingHealth;
        currentIndex = 0;
        nextSwitch = timeToSwitch;
        SwitchBoss(otherBosses[currentIndex]);
    }

    public void Update()
    {
        if (isKilled)
        {
            return;

        }

        if (Time.time > nextSwitch)
        {
            nextSwitch = Time.time + timeToSwitch;
            currentIndex++;
            if (currentIndex >= otherBosses.Count)
            {
                currentIndex = 0;
            }
            SwitchBoss(otherBosses[currentIndex]);
        }
    }

    public void SwitchBoss(BossData data)
    {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        if (shotManager != null)
        {
            shotManager.DisableAttacking();
        }

        shotManager = data.bossShotManager;
        specialActions = data.specialActions;
        shotManager.Init(Attack);
        foreach (var action in specialActions)
        {
            action.Init(this);
        }
    }

    protected override void OnDeath()
    {
        Debug.Log("Last boss killed");
        shotManager.DisableAttacking();
        spriteRenderer.sprite = deadSprite;
        bossInteraction.gameObject.SetActive(true);
        isKilled = true;
        CommandProcessor.SendCommand("LastBoss.End");
    }

    [Serializable]
    public class BossData
    {
        public BossShotManager bossShotManager;
        public List<BaseBossAction> specialActions;
    }
}
