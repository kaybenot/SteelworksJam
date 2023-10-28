using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamagable
{
    [SerializeField] private int startingHealth;

    private int currentHealth;

    public void Init()
    {
        currentHealth = startingHealth;
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        Debug.Log("Boss killed");
        CommandProcessor.SendCommand("Boss.End");
    }
}
