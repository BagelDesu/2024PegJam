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
    public UnityEvent<int> OnHealthInitialized = new UnityEvent<int>();

    [SerializeField]
    private float invulnTime = 5f;

    private float internalinvulnTimer = 0f;

    private bool countdownInvuln = false;

    private bool canTakeDamage = true;

    [SerializeField]
    private bool destroyOnZero = true;

    private void Start()
    {
        InitializeHealth();
    }

    public void InitializeHealth()
    {
        currentHealth = maxHealth;
        OnHealthInitialized.Invoke(currentHealth);
    }

    private void Update()
    {
        if (countdownInvuln)
        {
            internalinvulnTimer -= Time.deltaTime;

            if (internalinvulnTimer <= 0)
            {
                canTakeDamage = true;
                countdownInvuln = false;
            }
        }
    }

    public void MakeDamage(int damage, GameObject instigator)
    {
        if (canTakeDamage)
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
                if (destroyOnZero)
                {
                    Destroy(gameObject);
                }
            }

            internalinvulnTimer = invulnTime;
            countdownInvuln = true;
            canTakeDamage = false;
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
