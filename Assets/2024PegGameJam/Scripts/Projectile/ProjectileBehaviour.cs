using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    
    [field: SerializeField]
    public GameObject Instigator { get; set; }

    [field: SerializeField]
    public bool IsGravityAffected { get; set; }


    [field: SerializeField]
    public Vector2 InitialDirection { get; set; }

    [field: SerializeField]
    public Vector2 InitialSpeed { get; set; }

    [field: SerializeField]
    public float Damage { get; set; }


    private Rigidbody2D projectileRigidbody;

    private void Awake()
    {
        projectileRigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -100)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.MakeDamage(Damage, gameObject);
            // todo: play hit animation
        }
        Destroy(gameObject);
    }
}
