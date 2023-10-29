using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfGameDone : MonoBehaviour
{
    [SerializeField] private BossSpawnManager bossSpawnManager;
    [SerializeField] private GameObject interactionObject;

    private bool isDone;

    private void Update()
    {
        if(isDone) 
            return;

        if (DebuffManager.instance.killCount == 3)
        {
            EnableInteraction();
            isDone = true;
        }

    }

    private void EnableInteraction()
    {
        interactionObject.gameObject.SetActive(true);
    }
}
