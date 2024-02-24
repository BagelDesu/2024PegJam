using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [field: SerializeField]
    public float Health { get; set; }

    // todo: events?

    public void MakeDamage(float damage, GameObject instigator)
    {
        Health -= damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
