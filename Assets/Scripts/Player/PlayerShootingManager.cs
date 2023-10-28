using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingManager : MonoBehaviour
{
    [SerializeField] private Projectile gunProjectile;
    [SerializeField] private Transform leftBarrelPoint, rightBarrelPoint;
    [SerializeField] private float fireRate = 5f;

    private float nextFire = 0.0f;

    public void ShootGuns()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            this.transform.rotation = Camera.main.transform.rotation;
            Projectile leftBullet = Instantiate(gunProjectile, leftBarrelPoint.position, Quaternion.identity);
            Projectile rightBullet = Instantiate(gunProjectile, rightBarrelPoint.position, Quaternion.identity);

            leftBullet.Shoot(transform.forward);
            rightBullet.Shoot(transform.forward);
        }
    }
}
