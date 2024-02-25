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
}
