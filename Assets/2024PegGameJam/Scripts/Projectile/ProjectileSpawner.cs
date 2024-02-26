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

    public void ApplyShoot()
    {
        Debug.Log("ApplyShoot");
        //Vector3 direction = (FiringSocket.TransformPoint(Vector3.zero) - transform.position).normalized;
        //Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
        //GameObject proj = (GameObject)Instantiate(Projectile, FiringSocket.TransformPoint(Vector3.zero), rotation);

        GameObject proj = (GameObject)Instantiate(Projectile);
        if (proj != null)
        {
            proj.transform.position = FiringSocket.TransformPoint(Vector3.zero);

            ProjectileBehaviour projBehavior = proj.GetComponent<ProjectileBehaviour>();
            projBehavior.Instigator = gameObject;
            projBehavior.InitialDirection = GetDirectionToFromPositionToMouse(transform.position, FiringDirectionSocket.position);
            projBehavior.Damage = FireDamage;
            projBehavior.Apply();
        }
        //proj.transform.position = projectileSpawnLocation.position;

      
        // cast proj into projectileBehaviour
        // perform behaviour

    }

    private static Vector2 GetDirectionToFromPositionToMouse(Vector3 objectPosition, Vector3 firingDirection)
    {
        //Vector3 mousePosition = Input.mousePosition;
        //Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = (firingDirection - objectPosition).normalized;
        //Debug.Log(string.Format("GetDirectionToFromPositionToMouse: direction: {0}, mouseWorldPosition: {1}, objectPosition: {2}"
        //    , direction, mouseWorldPosition, objectPosition));
        return direction;
    }

}
