using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    void Shoot(Vector3 shotDirection);
    void Hit(IDamagable damagable);
}
