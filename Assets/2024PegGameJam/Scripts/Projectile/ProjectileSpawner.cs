using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform projectileSpawnLocation;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float fireRate;

    public void Shoot()
    {
        Debug.Log("Shoot");
        GameObject proj = (GameObject)Instantiate(projectile);
        proj.transform.position = projectileSpawnLocation.position;

      
        // cast proj into projectileBehaviour
        // perform behaviour

    }
}
