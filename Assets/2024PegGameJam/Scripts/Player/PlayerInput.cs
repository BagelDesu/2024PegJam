using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{

    [Header("Keybinding")]
    [SerializeField]
    private KeyCode[] Jump;
    [SerializeField]
    private KeyCode[] Dash;
    [SerializeField]
    private KeyCode[] ShootProjectile;

    [NonSerialized]
    public UnityEvent OnJumpKeyPressed = new UnityEvent();

    [NonSerialized]
    public UnityEvent OnDashKeyPressed = new UnityEvent();

    public UnityEvent OnShootProjectileKeyPressed = new UnityEvent();

    private Dictionary<KeyCode, UnityEvent> KeyEventMap = new();

    private void Awake()
    {
        AddKeyCodes(Jump, OnJumpKeyPressed);
        AddKeyCodes(Dash, OnDashKeyPressed);
        AddKeyCodes(ShootProjectile, OnShootProjectileKeyPressed);
    }

    private void AddKeyCodes(KeyCode[] keyCodes, UnityEvent eventToAssign)
    {
        if (eventToAssign == null)
            return;

        foreach (var key in keyCodes)
        {
            KeyEventMap.Add(key, eventToAssign);
        }
    }

    void Update()
    {
        foreach (var registered in KeyEventMap)
        {
            if (Input.GetKeyDown(registered.Key))
            {
                registered.Value.Invoke();
            }
        }
    }
}
