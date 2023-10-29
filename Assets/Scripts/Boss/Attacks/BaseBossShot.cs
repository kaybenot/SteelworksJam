using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBossShot : MonoBehaviour
{
    public float WaitForAttack = 0f;
    public abstract Projectile ProjectilePrefab { get; }
    public abstract void ShootProjectiles(Transform spawnPoint = null);
}
