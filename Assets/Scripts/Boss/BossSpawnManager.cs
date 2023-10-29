using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossSpawnManager : MonoBehaviour
{
    public List<BossData> bossDatas;

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

        currentBoss = Instantiate(currentBossData.bossPrefab, currentBossData.ghostPoint.SpawnPositionPoint);
        currentBoss.transform.localPosition = Vector3.zero;
        currentBoss.transform.localRotation = Quaternion.identity;
        currentBoss.transform.SetParent(this.transform);
        currentBoss.Init(currentBossData.ghostPoint.SpawnPositionPoint, this, bossIndex);
    }

    public void DespawnBoss()
    {
        if(currentBoss != null)
        {
            currentBossData.isKilled = true;
            currentBoss = null;
            currentBossData = null;
        }
    }

    public void SpawnGhost(Boss boss, int bossIndex)
    {
        var data = bossDatas[bossIndex];
        if (data != null)
        {
            Destroy(boss.gameObject);
            data.ghostPoint.transform.position = boss.transform.position;
            data.ghostPoint.gameObject.SetActive(true);
            data.ghostPoint.GoToTheFireplace();
            for (int i = 0; i < 3; i++)
            {
                if (i != bossIndex)
                {
                    bossDatas[i].ghostPoint.gameObject.SetActive(false);
                }
            }
            CommandProcessor.SendCommand("Dialog.Next");
        }
    }
}
