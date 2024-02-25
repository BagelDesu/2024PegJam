using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FlowerController : MonoBehaviour
{
    //=== Inspector Accessible ===//
    [Header("Flower Properties")]
    [SerializeField]
    private UnityEvent OnFlowerPickup = new UnityEvent();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            OnFlowerPickup?.Invoke();
        }
    }
}
