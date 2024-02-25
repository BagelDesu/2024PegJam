using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FlowerController : MonoBehaviour
{
    //=== Inspector Accessible ===//
    [Header("Flower Properties")]
    [SerializeField]
    private bool isActive = true;
    public UnityEvent<GameObject> onPlayerTrigger = new UnityEvent<GameObject>();


    //=== Internal variables ===//
    [SerializeField]
    private GameObject beeParticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive)
        {
            beeParticle.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(string.Format("Collided with: {0}",collision.gameObject.tag));
        if(collision.gameObject.tag == "Player")
        {
            isActive = false;
            onPlayerTrigger?.Invoke(gameObject);
        }
    }
}
