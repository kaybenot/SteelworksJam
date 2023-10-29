using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndInteraction : MonoBehaviour, IInteractable
{
    public void Use(Player player)
    {
        CommandProcessor.SendCommand("LastBoss.Start");
    }
}