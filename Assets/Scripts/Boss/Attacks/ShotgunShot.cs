using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShot : BaseBossShot
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float shotRangeDegrees = 120f;
    [SerializeField] private int shotAmount = 20;
    [SerializeField] private float shotSpeed = 1f;

    public override Projectile ProjectilePrefab { get => projectilePrefab; }

    public override void ShootProjectiles(Transform spawnPoint = null)
    {
        float degreesBetween = (float)(shotRangeDegrees / shotAmount);

        for (int i = 0; i < shotAmount; i++)
        {
            var projectile = Instantiate(projectilePrefab, spawnPoint);
            projectile.transform.localPosition = Vector3.zero;

            var player = GameObject.FindGameObjectWithTag("Player").transform;
            Vector3 playerEulerAngles = Quaternion.LookRotation(player.position - spawnPoint.position).eulerAngles;
            playerEulerAngles.y += (i - shotAmount / 2) * degreesBetween;

            Vector3 playerDirection = Quaternion.Euler(playerEulerAngles) * Vector3.forward;
            playerDirection = playerDirection.normalized;

            projectile.Speed = shotSpeed;

            projectile.Shoot(playerDirection);
        }
    }
}
