using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set ; }

    public int maxFlowers = 0;
    public int currentFlowers = 0;

    public UnityEvent OnFlowerAdded = new UnityEvent();
    public UnityEvent OnFlowerAddedMax = new UnityEvent();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public void AddFlowers()
    {
        maxFlowers++;
        OnFlowerAddedMax?.Invoke();
    }

    public void increaseCurrentFlower()
    {
        currentFlowers++;
        OnFlowerAdded?.Invoke();
    }
}
