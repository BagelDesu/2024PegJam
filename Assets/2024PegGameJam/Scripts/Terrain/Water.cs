using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField]
    private int Damage;

    [SerializeField]
    private Transform Checkpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            HealthComponent hc = (HealthComponent)collision.gameObject.GetComponent<IDamageable>();

            if(hc != null)
            {
                hc.MakeDamage(Damage, this.gameObject);
            }

            collision.gameObject.transform.SetPositionAndRotation(Checkpoint.transform.position, Quaternion.identity);
        }
    }
}
