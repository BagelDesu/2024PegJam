using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    
    [field: SerializeField]
    public GameObject Instigator { get; set; }

    [field: SerializeField]
    public bool IsGravityAffected { get; set; } = true;

    [field: SerializeField]
    public Vector2 InitialDirection { get; set; }


    [field: SerializeField]
    public float Damage { get; set; }
    [field: SerializeField]
    public float Impulse { get; set; }


    [field: SerializeField]
    public float DieDelay { get; set; } = 1.0f;

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
        //Quaternion.LookRotation(targetVector, Vector3.forward);

        Vector3 movingDirection = projectileRigidbody.velocity.normalized;
        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.right, movingDirection);

        transform.rotation = targetRotation;


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);

        if (collision.gameObject == Instigator
            || collision.gameObject.GetComponent<ProjectileBehaviour>() != null)
        {
            return;
        }


        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.MakeDamage(Damage, gameObject);
            // todo: play hit animation
        }
        StartCoroutine(DieAfterDelay(DieDelay));
    }

    IEnumerator DieAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }

    public void Apply()
    {
        Debug.Log(string.Format("Spawned, position: {0}, rotation: {1}, damage: {2}", transform.position, InitialDirection, Damage));
        Debug.DrawLine(transform.position, transform.position + new Vector3(InitialDirection.x, InitialDirection.y, transform.position.z) * 3, Color.green, 3f);

        projectileRigidbody.AddForce(InitialDirection.normalized * Impulse, ForceMode2D.Impulse);
    }
}
