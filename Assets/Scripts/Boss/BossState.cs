using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : MonoBehaviour
{
    [SerializeField] private BossSpawnManager bossSpawnManager;

    [ContextMenu("Star boss fight")]
    public void StartBossFight()
    {
        //bossSpawnManager.SpawnBoss();
    }
}
