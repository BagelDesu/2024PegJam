using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealingHive : MonoBehaviour
{
    [SerializeField]
    private int healAmount;
    [SerializeField]
    private UnityEvent OnHivePickup = new UnityEvent();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthComponent hc = (HealthComponent)collision.gameObject.GetComponent<IDamageable>();
            if (hc != null)
            {
                hc.Heal(healAmount, this.gameObject);
            }
            OnHivePickup?.Invoke();
        }
    }
}
