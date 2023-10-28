using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    void Shoot(Vector3 shotDirection, float customSpeed = -1);
    void Hit(IDamagable damagable);
}
