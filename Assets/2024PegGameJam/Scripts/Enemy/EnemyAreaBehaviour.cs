using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyAreaBehaviour : MonoBehaviour
{
    [field: SerializeField]
    public GameObject EnemyPrefab { get; set; }

    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        // Get the BoxCollider2D component
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //DrawCollider();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
    }

    private void OnTriggerExit2D(Collider2D other)
    {
    }


    private void OnDrawGizmosSelected()
    {
        if (boxCollider == null)
            return;

        Gizmos.color = Color.yellow;

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
    
}
