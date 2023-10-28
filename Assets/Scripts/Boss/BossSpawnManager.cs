using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnManager : MonoBehaviour
{
    [SerializeField] private List<BossData> bossDatas;

    public void SpawnBoss(int bossIndex)
    {
        if (bossIndex < 0 || bossIndex >= bossDatas.Count)
        {
            Debug.LogError("Boss index is out of range!");
        }

        var bossData = bossDatas[bossIndex];

        var boss = Instantiate(bossData.bossPrefab, bossData.spawnPoint);
        boss.transform.position = Vector3.zero;
        boss.transform.rotation = Quaternion.identity;
        boss.Init();
    }
}
