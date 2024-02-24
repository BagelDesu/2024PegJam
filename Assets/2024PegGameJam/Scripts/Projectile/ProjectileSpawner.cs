using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    private Transform projectileSpawnLocation;
    private GameObject fireRate;
    private GameObject projectile;

    public void Shoot()
    {
        Debug.Log("Shoot");
    }
}
