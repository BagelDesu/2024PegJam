using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void MakeDamage(int damage, GameObject instigator);
}
