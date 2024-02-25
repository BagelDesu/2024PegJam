using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    Patrol,
    ChaseCrawl,
    ReadyToAttack,
    PerformAttack
}
public abstract class EnemyBase : MonoBehaviour
{
    [field: SerializeField]
    public EnemyState State { get; private set; } = EnemyState.Patrol;


    [field: SerializeField]
    public GameObject PlayerToChase { get; set; }
    [field: SerializeField]
    public BoxCollider2D BoxToLive { get; set; }

    [field: Header("Patrol related")]
    [field: SerializeField]
    public float PatrolSpeed { get; set; }

    [field: Header("Chase related")]
    [field: SerializeField]
    public float ChaseCrawlSpeed { get; set; }
    [field: SerializeField]
    public float ChaseDistanceToMaintain { get; set; }
    [field: SerializeField]
    public float ChaseDistanceTolerance { get; set; }


    private Vector2 moveVector = new Vector2(0, 0);
    private Rigidbody2D myRigidbody;



    protected virtual void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {

        switch (State)
        {
            case EnemyState.Patrol:
                PatrolUpdate();
                break;
            case EnemyState.ChaseCrawl:
            case EnemyState.ReadyToAttack:
                ChaseCrawlUpdate();
                break;
            case EnemyState.PerformAttack:
                PerformAttackUpdate();
                break;
            default:
                break;
        }
    }

    public void SetPlayerInsideArea(GameObject player)
    {
        PlayerToChase = player;
        State = EnemyState.ChaseCrawl;
    }

    public void SetPlayerOutsideArea()
    {
        PlayerToChase = null;
        State = EnemyState.Patrol;
    }

    public virtual void PatrolUpdate()
    {
        Vector2 playerPos = transform.position;

        if (moveVector.x == 0)
        {
            // face to the center of the living box
            moveVector = BoxToLive.offset - playerPos;
            moveVector.y = 0;
            moveVector = moveVector.normalized;
        }
        else if (playerPos.x > BoxToLive.offset.x + BoxToLive.size.x*0.5f)
        {
            moveVector.x = -1.0f;
        }
        else if(playerPos.x < BoxToLive.offset.x - BoxToLive.size.x * 0.5f)
        {
            moveVector.x = 1.0f;
        }

        moveVector *= PatrolSpeed;
        myRigidbody.velocity = moveVector;
    }

    public virtual void ChaseCrawlUpdate()
    {
        TryToMaintainChaseDistance();
    }

    public abstract void PerformAttackUpdate();


    private void TryToMaintainChaseDistance()
    {
        State = EnemyState.ChaseCrawl;

        float playerX = PlayerToChase.transform.position.x;
        float myX = transform.position.x;

        float distanceToPlayer = playerX - myX;
        float distanceToPlayerAbs = Mathf.Abs(playerX - myX);

        Vector2 directionToPlayer = new Vector2(distanceToPlayer, 0).normalized;

        if (distanceToPlayerAbs > ChaseDistanceToMaintain + ChaseDistanceTolerance)
        {
            moveVector = directionToPlayer;
        }
        else if (distanceToPlayerAbs < ChaseDistanceToMaintain - ChaseDistanceTolerance)
        {
            moveVector = -directionToPlayer;
        }
        else
        {
            State = EnemyState.ReadyToAttack;
            moveVector = Vector2.zero;
        }

        moveVector *= ChaseCrawlSpeed;
        myRigidbody.velocity = moveVector;
    }

}
