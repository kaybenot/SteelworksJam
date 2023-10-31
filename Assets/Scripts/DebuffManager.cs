using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class DebuffManager : MonoBehaviour
{
    [SerializeField] private GameObject ghostAI;
    [SerializeField] private BossSpawnManager bossSpawnManager;
    private Player player;
    public static DebuffManager instance;
    private List<int> debuffList = new() { 0, 1, 2, 3 };
    public int killCount = 0;
private void Awake()
    {
        debuffList = new() { 0, 1, 2, 3 };
        killCount = 0;

        if (instance == null)
        {
            instance = this;
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player == null)
            Debug.LogAssertion("PlayerController could not find Player script!");
    }
    public void ApplyRandomDebuff()
    {
        ghostAI.SetActive(true);
        bossSpawnManager.SummonRemainingGhosts();
        int randomDebuff = Random.Range(0, debuffList.Count);
        switch (debuffList[randomDebuff])
        {
            case 0:
                SlowPlayerSpeed();
                break;
            case 1:
                SetFogDensity();
                break;
            case 2:
                SetCameraNoise();
                break;
            case 3:
                SetUiObstacles();
                break;
        }
        debuffList.RemoveAt(randomDebuff);
        killCount++;
    }
    private void SlowPlayerSpeed()
    {
        Debug.Log("slowSpeed");
        player.MovementSpeed = 4f;
    }
    private void SetFogDensity()
    {
        Debug.Log("Fog");
        RenderSettings.fogDensity *= 2;
    }
    private void SetCameraNoise()
    {
        Debug.Log("CameraNoise");
        player.Head.GetComponent<CinemachineVirtualCamera>().enabled = true;
    }
    private void SetUiObstacles()
    {
        Debug.Log("UiObstacle");
        CommandProcessor.SendCommand("Canvas ShowBadSight");
    }
}
