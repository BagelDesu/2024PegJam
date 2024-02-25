using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspAnimationManager : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SwitchAnimation(EnemyState state)
    {
        switch(state) {
            case EnemyState.Patrol:
                animator.SetTrigger("Idle");
                break;
            case EnemyState.PerformAttack:
                animator.SetTrigger("Attack");
                break;
            default: break;
        }
    }
}
