using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnManager : MonoBehaviour
{
    [SerializeField] private Boss bossPrefab;
    [SerializeField] private Transform bossSpawnPoint;

    public void SpawnBoss()
    {
        Instantiate(bossPrefab, bossSpawnPoint);
        bossPrefab.transform.position = Vector3.zero;
        bossPrefab.transform.rotation = Quaternion.identity;
        bossPrefab.Init();
    }
}
