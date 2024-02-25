using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDasher : EnemyBase
{
    [field: SerializeField]
    public float AttackCooldown { get; set; } = 5.0f;
    public float ChargeTimeout { get; set; } = 1.0f;

    private float chargeTimeoutCounter;
    private float attackCooldownCounter;

    public override void PerformAttackUpdate()
    {
        throw new System.NotImplementedException();
    }

    protected override void Awake()
    {
        base.Awake();

        //OnStateChange.
    }
    protected override void Update()
    {
        base.Update();

        if (attackCooldownCounter > 0.0f)
        {
            attackCooldownCounter -= Time.deltaTime;
        }

        //AttackDeci
    }



}
