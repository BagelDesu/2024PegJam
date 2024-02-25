using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDasher : EnemyBase
{

    [field: SerializeField]
    public int DamageOnTouch { get; set; } = 1;

    [field: Header("Timeouts")]
    [field: SerializeField]
    public float AttackCooldown { get; set; } = 5.0f;
    [field: SerializeField]
    public float ChargeTimeout { get; set; } = 1.0f;

    [field: Header("Dash Setting")]
    [field: SerializeField]
    public float DashDuration { get; set; } = 3.0f;

    [field: SerializeField]
    public float DashDistance { get; set; } = 3.0f;

    private Vector3 StartDashPoint;
    private Vector3 EndDashPoint;

    private float chargeTimeoutCounter;
    private float attackCooldownCounter;
    private float dashDurationCounter;

    public override void PerformAttackUpdate()
    {
        // Setup attack
        if (dashDurationCounter == DashDuration)
        {
            float playerX = PlayerToChase.transform.position.x;
            float myX = transform.position.x;

            float distanceToPlayer = playerX - myX;

            Vector2 directionToPlayer = new Vector2(distanceToPlayer, 0).normalized;
            Vector3 directionToPlayer3d = (Vector3)(directionToPlayer);

            StartDashPoint = transform.position;
            EndDashPoint = transform.position + directionToPlayer3d * DashDistance;
        }

        float t = Mathf.Clamp01(1.0f - (dashDurationCounter / DashDuration));
        float easedT = 0;
        if (t < 0.5f)
        {
            easedT = EaseInOut(2*t);
        }
        else
        {
            easedT = EaseInOut(2-(2 * t));
        }


        transform.position = Vector3.Lerp(StartDashPoint, EndDashPoint, easedT);


        dashDurationCounter -= Time.deltaTime;

        if (dashDurationCounter <= 0.0f)
        {
            Debug.Log("Attack Ended");
            ChangeState(EnemyState.ChaseCrawl);
        }
    }
    private float EaseInOut(float t)
    {
        // Applying ease-in-out function
        return t < 0.5 ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
    }

    protected override void Awake()
    {
        base.Awake();

        OnStateChanged.AddListener(StateChangedHandler);
    }
    protected override void Update()
    {
        base.Update();

        if (attackCooldownCounter > 0.0f)
        {
            attackCooldownCounter -= Time.deltaTime;
        }

        ProcessCharge();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.MakeDamage(DamageOnTouch, gameObject);
        }
    }

    private void StateChangedHandler(EnemyState newState)
    {
        /*
        switch (newState)
        {
            case EnemyState.Patrol:
            case EnemyState.PerformAttack:
                break;
            case EnemyState.ChaseCrawl:
                chargeTimeoutCounter = ChargeTimeout;
                break;
            case EnemyState.ReadyToAttack:

                break;
            default:
                break;
        }
        */
    }

    private void ProcessCharge()
    {
        if (State == EnemyState.ReadyToAttack)
        {
            chargeTimeoutCounter -= Time.deltaTime;
            if (chargeTimeoutCounter < 0.0f)
            {
                SetupForAttack();
                
            }
        }
        else
        {
            chargeTimeoutCounter = ChargeTimeout;
        }
    }

    private void SetupForAttack()
    {
        dashDurationCounter = DashDuration;
        ChangeState(EnemyState.PerformAttack);
    }
}
