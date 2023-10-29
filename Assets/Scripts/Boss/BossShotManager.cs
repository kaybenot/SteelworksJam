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
    private Coroutine waitForAttackCoroutine;
    private bool enableAttacking = false;

    public void Init(Action onShoot)
    {
        enableAttacking = true;
        OnShoot += onShoot;
    }

    public void DisableAttacking()
    {
        enableAttacking = false;
        OnShoot = null;
    }

    private void Update()
    {
        if (!enableAttacking)
            return;

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

            OnShoot?.Invoke();
            var attack = shootingTypes[rand];
            if (attack.WaitForAttack != 0)
            {
                if (waitForAttackCoroutine != null)
                {
                    StopCoroutine(waitForAttackCoroutine);
                    waitForAttackCoroutine = null;
                }
                waitForAttackCoroutine = StartCoroutine(WaitForAttack(attack.WaitForAttack, attack));
            }
            else
            {
                attack.ShootProjectiles(this.transform);
            }
        }
    }

    private IEnumerator WaitForAttack(float waitTime, BaseBossShot attack)
    {
        yield return new WaitForSeconds(waitTime);
        attack.ShootProjectiles(this.transform);
        waitForAttackCoroutine = null;
    }
}
