using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class DebuffManager : MonoBehaviour
{
    private Player player;
    public static DebuffManager instance;
    private List<int> debuffList = new (){ 0, 1, 2, 3 };
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player == null)
            Debug.LogAssertion("PlayerController could not find Player script!");
    }
    public void ApplyRandomDebuff()
    {
        int randomDebuff = Random.Range(0, debuffList.Count);
        switch (randomDebuff)
        {
            case 0:
                SlowPlayerSpeed();
                debuffList.Remove(randomDebuff);
                break;
            case 1:
                SetFogDensity();
                debuffList.Remove(randomDebuff);
                break;
            case 2:
                SetCameraNoise();
                debuffList.Remove(randomDebuff);
                break;
            case 3:
                SetUiObstacles();
                debuffList.Remove(randomDebuff);
                break;
        }

    }
    private void SlowPlayerSpeed()
    {
        player.MovementSpeed = 3f;
    }
    private void SetFogDensity()
    {
        RenderSettings.fogDensity *= 2;
    }
    private void SetCameraNoise()
    {
        player.Head.GetComponent<CinemachineVirtualCamera>().enabled = true;
    }
    private void SetUiObstacles()
    {
        CommandProcessor.SendCommand("Canvas.ShowBadSight");
    }
}
