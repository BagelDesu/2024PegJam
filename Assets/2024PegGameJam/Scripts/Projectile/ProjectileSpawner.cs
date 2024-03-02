using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileSpawner : MonoBehaviour
{
    [field: SerializeField]
    public Transform FiringSocket { get; set; }

    [SerializeField]
    private Transform FiringDirectionSocket;

    [field: SerializeField]
    public GameObject Projectile { get; set; }
    [field: SerializeField]
    private float FireRate { get; set; }
    [field: SerializeField]
    private int FireDamage { get; set; }

    private PlayerInput inputComponent;

    private float fireCooldown = 0f;

    private bool canFire = true;

    void Start()
    {
        inputComponent.OnShootProjectileKeyPressed.AddListener(ApplyShoot);
    }

    

    private void Awake()
    {
        inputComponent = GetComponent<PlayerInput>();

        var socketComponent = GameObject.Find("FiringLocationSocket");
        if (socketComponent == null)
        {
            throw new ArgumentException("FiringLocationSocket must be present");
        }
        FiringSocket = socketComponent.transform;
        //Debug.Log(socketComponent.transform.localPosition);

        if (Projectile == null)
        {
            throw new ArgumentException(string.Format("{0} must have projectile set!", gameObject.ToString()));
        }

        var projectileComponent = Projectile.GetComponent<ProjectileBehaviour>();
        if (projectileComponent == null)
        {
            throw new ArgumentException(string.Format("{0} must have {1} component!", gameObject.ToString(), typeof(ProjectileBehaviour)));
        }
    }

    private void Update()
    {
        if (!canFire)
        {
            fireCooldown -= Time.deltaTime;
            if (fireCooldown <= 0)
            {
                canFire = true;
            }
        }
    }

    public void ApplyShoot()
    {
        if (!canFire)
        {
            return;
        }

        GameObject proj = (GameObject)Instantiate(Projectile);
        if (proj != null)
        {
            proj.transform.position = FiringSocket.TransformPoint(Vector3.zero);

            ProjectileBehaviour projBehavior = proj.GetComponent<ProjectileBehaviour>();
            projBehavior.Instigator = gameObject;
            projBehavior.InitialDirection = GetFireDirection(transform.position, FiringDirectionSocket.position);
            projBehavior.Damage = FireDamage;
            projBehavior.Apply();
        }

        canFire = false;
        fireCooldown = FireRate;
    }

    private static Vector2 GetFireDirection(Vector3 objectPosition, Vector3 firingDirection)
    {
        Vector2 direction = (firingDirection - objectPosition).normalized;
        return direction;
    }

}
