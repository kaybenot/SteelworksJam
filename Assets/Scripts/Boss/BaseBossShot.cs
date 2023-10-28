using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBossShot : MonoBehaviour
{
    public abstract Projectile ProjectilePrefab { get; }

    public abstract void ShootProjectiles(Transform spawnPoint = null);
}
