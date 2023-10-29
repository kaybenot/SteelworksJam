using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnManager : MonoBehaviour
{
    [SerializeField] private List<BossData> bossDatas;

    private Boss currentBoss; 
    private BossData currentBossData;

    public void SpawnBoss(int bossIndex)
    {
        if (bossIndex < 0 || bossIndex >= bossDatas.Count)
        {
            Debug.LogError("Boss index is out of range!");
        }
        if (currentBoss != null)
        {
            DespawnBoss();
        }

        currentBossData = bossDatas[bossIndex];

        currentBoss = Instantiate(currentBossData.bossPrefab, currentBossData.ghostPoint.transform);
        currentBoss.transform.localPosition = Vector3.zero;
        currentBoss.transform.localRotation = Quaternion.identity;
        currentBoss.transform.SetParent(this.transform);
        currentBoss.Init();
    }

    public void DespawnBoss()
    {
        if(currentBoss != null)
        {
            //Destroy(currentBoss.gameObject);
            currentBossData.isKilled = true;
            currentBoss = null;
            currentBossData = null;
        }
    }
}
