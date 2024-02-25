using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int maxHealth = 5;
    public int currentHealth { get; private set; }

    // gives a 
    public UnityEvent<int> OnHealthUpdated = new UnityEvent<int>();
    public UnityEvent OnHealthZero = new UnityEvent();

    public void MakeDamage(int damage, GameObject instigator)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        OnHealthUpdated?.Invoke(currentHealth);

        if (currentHealth == 0)
        {
            OnHealthZero?.Invoke();
        }
    }

    public void Heal(int healAmount, GameObject instigator)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        OnHealthUpdated?.Invoke(currentHealth);
    }
}
