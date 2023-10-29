using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootByDegrees : BaseBossShot
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float degreesBetweenProjectiles = 30f;
    [SerializeField] private float projectileSpeed = 20f;

    public override Projectile ProjectilePrefab { get => projectilePrefab;  }

    public override void ShootProjectiles(Transform spawnPoint = null)
    {
        int amount = (int)(360f / degreesBetweenProjectiles);
        float randomRotationOffset = Random.Range(0, degreesBetweenProjectiles);

        for(int i = 0; i < amount; i++)
        {
            var projectile = Instantiate(projectilePrefab, spawnPoint);
            projectile.transform.localPosition = Vector3.zero;

            Vector3 eulerAngles = new Vector3(0f, i * degreesBetweenProjectiles + randomRotationOffset, 0f);
            Vector3 direction = Quaternion.Euler(eulerAngles) * Vector3.forward;
            direction = direction.normalized;

            projectile.Speed = projectileSpeed;
            projectile.Shoot(direction);
        }
    }
}
