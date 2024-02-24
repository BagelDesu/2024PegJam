using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void MakeDamage(float damage, GameObject instigator);
}
