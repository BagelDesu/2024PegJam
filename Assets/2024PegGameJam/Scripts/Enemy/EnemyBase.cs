using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum EnemyState
{
    Patrol,
    ChaseCrawl,
    ReadyToAttack,
    PerformAttack
}

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
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
    private float direction = 1.0f;

    public UnityEvent<EnemyState> OnStateChanged = new UnityEvent<EnemyState>();

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

        ApplyDirection();
    }

    private void ApplyDirection()
    {
        float previousDirection = direction;
        if (moveVector.x != 0)
        {
            direction = moveVector.x > 0 ? 1 : -1;
        }

        if (direction != previousDirection)
        {
            //bool isFacingForward = direction > 0;
            //gameObject.GetComponent<SpriteRenderer>().flipX = !isFacingForward;
            transform.localScale = new Vector3(direction, 1, 1);
        }

    }

    //public void Setup()
    //{

    //}
    public void SetPlayerInsideArea(GameObject player)
    {
        PlayerToChase = player;
        ChangeState(EnemyState.ChaseCrawl);
    }

    public void SetPlayerOutsideArea()
    {
        PlayerToChase = null;
        ChangeState(EnemyState.Patrol);
    }

    public virtual void PatrolUpdate()
    {
        Vector2 myPos = transform.position;

        //Debug.Log("PatrolUpdate");
        if (moveVector.x == 0)
        {
            // face to the center of the living box
            moveVector = (Vector2)BoxToLive.transform.position - myPos;
            moveVector.y = 0;
            moveVector = moveVector.normalized;
            if (moveVector.x == 0)
            {
                moveVector.x = Random.value < 0.5f ? 1 : -1;
            }
            //Debug.Log(string.Format("   moveVector.x == 0, moveVector = {0}", moveVector));
        }
        else if (myPos.x > BoxToLive.transform.position.x + BoxToLive.size.x*0.5f)
        {
            moveVector.x = -1.0f;
        }
        else if(myPos.x < BoxToLive.transform.position.x - BoxToLive.size.x * 0.5f)
        {
            moveVector.x = 1.0f;
        }

        myRigidbody.velocity = new Vector2(moveVector.x * PatrolSpeed, myRigidbody.velocity.y);
    }

    public virtual void ChaseCrawlUpdate()
    {
        if (PlayerToChase == null)
        {
            ChangeState(EnemyState.Patrol);
            return;
        }
        TryToMaintainChaseDistance();
    }

    public abstract void PerformAttackUpdate();


    private void TryToMaintainChaseDistance()
    {
        float playerX = PlayerToChase.transform.position.x;
        float myX = transform.position.x;

        float distanceToPlayer = playerX - myX;
        float distanceToPlayerAbs = Mathf.Abs(playerX - myX);

        Vector2 directionToPlayer = new Vector2(distanceToPlayer, 0).normalized;

        bool isInsideMaintainTolerance = false;

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
            moveVector = Vector2.zero;
            isInsideMaintainTolerance = true;
        }

        if (State == EnemyState.ReadyToAttack
            && !isInsideMaintainTolerance)
        {
            ChangeState(EnemyState.ChaseCrawl);
        }
        else if (State == EnemyState.ChaseCrawl
                 && isInsideMaintainTolerance)
        {
            ChangeState(EnemyState.ReadyToAttack);
        }

        myRigidbody.velocity = new Vector2(moveVector.x * ChaseCrawlSpeed, myRigidbody.velocity.y);
    }

    protected void ChangeState(EnemyState newState)
    {
        State = newState;
        OnStateChanged.Invoke(newState);
    }

}
