using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<GameObject> OnPlayerInRange = new UnityEvent<GameObject>();
    [SerializeField]
    private UnityEvent<GameObject> OnPlayerOutOfRange = new UnityEvent<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnPlayerInRange?.Invoke(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnPlayerOutOfRange?.Invoke(collision.gameObject);
        }
    }
}
