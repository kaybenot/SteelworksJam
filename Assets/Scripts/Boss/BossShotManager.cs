using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShotManager : MonoBehaviour
{
    [SerializeField] private float fireRate = 5f;
    [SerializeField] private List<BaseBossShot> shootingTypes;

    private float nextFire = 0.0f;
    private Action OnShoot;

    public void Init(Action onShoot)
    {
        OnShoot += onShoot;
    }

    private void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (shootingTypes != null || shootingTypes.Count != 0)
        {
            int rand = UnityEngine.Random.Range(0, shootingTypes.Count);
            shootingTypes[rand].ShootProjectiles(this.transform);
            OnShoot?.Invoke();
        }
    }
}
