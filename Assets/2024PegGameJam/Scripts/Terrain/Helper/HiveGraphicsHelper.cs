using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveGraphicsHelper : MonoBehaviour
{
    [SerializeField]
    private Color disabledColor = Color.gray;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DisableHive()
    {
        spriteRenderer.color = disabledColor;
    }

}
