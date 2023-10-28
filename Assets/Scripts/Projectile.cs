using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    [SerializeField] private int projectileDamage;
    [SerializeField] private string targetTag;
    [SerializeField] private float projectileSpeed = 1f;
    [SerializeField] private float timeToLive = 5f;

    private bool startMoving = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag && other.gameObject.TryGetComponent(out IDamagable damagable))
        {
            Debug.Log($"Hit {other.gameObject.name} for {projectileDamage} damage!");
            Hit(damagable);
        }
        else if(other.tag == "Obstacle")
        {
            DestroyProjectile();
        }
    }

    private void Update()
    {
        if (startMoving)
        {
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }
    }

    public void Shoot(Vector3 shotDirection)
    {
        this.transform.forward = shotDirection;
        startMoving = true;
        Invoke("DestroyProjectile", timeToLive);
    }

    public void Hit(IDamagable damagable)
    {
        damagable.Damage(projectileDamage);
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
