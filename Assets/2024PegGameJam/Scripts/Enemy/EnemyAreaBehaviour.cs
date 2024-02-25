using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyAreaBehaviour : MonoBehaviour
{
    [field: SerializeField]
    public GameObject PlayerInside { get; set; }
    [field: SerializeField]
    public GameObject EnemyPrefab { get; set; }

    private GameObject enemyInstance;
    private EnemyBase enemyInstanceComponent;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        

    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
#endif

    private void Awake()
    {
        // Get the BoxCollider2D component
        boxCollider = GetComponent<BoxCollider2D>();
        Vector3 boxCenter = new Vector3(boxCollider.offset.x, boxCollider.offset.y, transform.position.z);

        enemyInstance = (GameObject)Instantiate(EnemyPrefab, boxCenter, transform.rotation);
        if (enemyInstance != null)
        {
            enemyInstanceComponent = enemyInstance.GetComponent<EnemyBase>();
            if (enemyInstanceComponent == null)
            {
                Destroy(this);
                throw new ArgumentException("EnemyAreaBehaviour must have valid EnemyPrefab with EnemyBase-derived component attached");
            }
            enemyInstanceComponent.BoxToLive = boxCollider;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyInstance == null)
        {
            Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            PlayerInside = other.gameObject;
            enemyInstanceComponent.SetPlayerInsideArea(other.gameObject);
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            PlayerInside = null;
            enemyInstanceComponent.SetPlayerOutsideArea();
        }
    }


//#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //if (boxCollider == null)
        //    return;

        Gizmos.color = Color.blue;

        Vector2 size = boxCollider.size*1.1f;
        Vector2 center = boxCollider.offset;

        Vector2 topLeft = (Vector2)transform.position + center + Vector2.Scale(size, new Vector2(-0.5f, 0.5f));
        Vector2 topRight = (Vector2)transform.position + center + Vector2.Scale(size, new Vector2(0.5f, 0.5f));
        Vector2 bottomLeft = (Vector2)transform.position + center + Vector2.Scale(size, new Vector2(-0.5f, -0.5f));
        Vector2 bottomRight = (Vector2)transform.position + center + Vector2.Scale(size, new Vector2(0.5f, -0.5f));

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
//#endif

}
