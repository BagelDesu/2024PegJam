using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager Instance { get => instance; private set => instance = value; }

    private static GameManager instance;

    public int maxFlowers = 0;
    public int currentFlowers = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Instance = instance;
        }
        else
        {
            Destroy(this);
        }
    }

}
