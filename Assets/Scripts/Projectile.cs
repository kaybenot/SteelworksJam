using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    [SerializeField] private int projectileDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable damagable))
        {
            Debug.Log($"Hit {other.gameObject} for {projectileDamage} damage!");
            Hit(damagable);
        }
    }

    public void Shot()
    {
    }

    public void Hit(IDamagable damagable)
    {
        damagable.Damage(projectileDamage);
    }
}
