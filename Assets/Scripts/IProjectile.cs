using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    void Shot();
    void Hit(IDamagable damagable);
}
