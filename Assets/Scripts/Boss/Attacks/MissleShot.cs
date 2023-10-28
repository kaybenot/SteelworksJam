using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MissleShot : BaseBossShot
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float shotRangeDegrees = 120f;
    [SerializeField] private int shotAmount = 20;
    [SerializeField] private float targetPlayerInSeconds = 2f;
    [SerializeField] private float aimBetweenShots = 0.125f;
    [SerializeField] private float newSpeed = 3f;

    private Coroutine currentCoroutine;
    private Projectile[] currentProjectiles;

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
        float degreesBetween = (float)(shotRangeDegrees / shotAmount);

        currentProjectiles = new Projectile[shotAmount];
        var player = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < shotAmount; i++)
        {
            var projectile = Instantiate(projectilePrefab, spawnPoint);
            projectile.transform.localPosition = Vector3.zero;

            Vector3 playerEulerAngles = Quaternion.LookRotation(player.position - spawnPoint.position).eulerAngles;
            playerEulerAngles.y += (i - shotAmount / 2) * degreesBetween;


            Vector3 playerDirection = Quaternion.Euler(playerEulerAngles) * Vector3.forward;
            playerDirection = playerDirection.normalized;

            projectile.Shoot(playerDirection);
            currentProjectiles[i] = projectile;
        }

        currentCoroutine = StartCoroutine(ChangeTargetInTime(targetPlayerInSeconds, aimBetweenShots, player));
    }

    public IEnumerator ChangeTargetInTime(float waitTime, float timeBetween, Transform target)
    {
        yield return new WaitForSeconds(waitTime);
        for (int i = 0; i < shotAmount;i++)
        {
            var projectile = currentProjectiles[i];

            Vector3 eulerAngles = Quaternion.LookRotation(target.position - projectile.transform.position).eulerAngles;

            Vector3 direction = Quaternion.Euler(eulerAngles) * Vector3.forward;
            direction = direction.normalized;

            projectile.transform.forward = direction;
            projectile.Speed = newSpeed;
            yield return new WaitForSeconds(aimBetweenShots);
        }
        currentCoroutine = null;
    }

}
