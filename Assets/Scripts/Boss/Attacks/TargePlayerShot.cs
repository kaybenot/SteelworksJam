using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargePlayerShot : BaseBossShot
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private int projectileAmount = 5;
    [SerializeField] private float timeBetweenShoots = 0.5f;
    [SerializeField] private float customSpeed = 2f; // -1 default
    [SerializeField] private float randomSpread = 0f;

    private Coroutine currentCoroutine;

    public override Projectile ProjectilePrefab { get => projectilePrefab; }

    private void OnDestroy()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
    }

    public override void ShootProjectiles(Transform spawnPoint = null)
    {
        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
        var player = GameObject.FindGameObjectWithTag("Player").transform;
        currentCoroutine = StartCoroutine(Shooting(projectileAmount, timeBetweenShoots, player, spawnPoint));
    }

    public IEnumerator Shooting(int amount, float waitTime, Transform target, Transform spawnPoint)
    {
        while (amount > 0)
        {
            var projectile = Instantiate(projectilePrefab);
            projectile.transform.position = spawnPoint.position;

            Vector3 eulerAngles = Quaternion.LookRotation(target.position - spawnPoint.position).eulerAngles;
            if (randomSpread != 0) eulerAngles.y += Random.Range(-randomSpread, randomSpread);
            eulerAngles.x = 0;
            eulerAngles.z = 0;

            Vector3 direction = Quaternion.Euler(eulerAngles) * Vector3.forward;
            direction = direction.normalized;

            projectile.Shoot(direction, customSpeed);
            amount --;
            yield return new WaitForSeconds(waitTime);
        }
        currentCoroutine = null;
    }
}
