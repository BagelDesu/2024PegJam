using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlowersCollected : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;


    public void UpdateFlowers()
    {
        text.text = GameManager.Instance.currentFlowers.ToString() + "/" + GameManager.Instance.maxFlowers.ToString();
    }
}

