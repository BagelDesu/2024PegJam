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

    [NonSerialized]
    public UnityEvent OnShootProjectileKeyPressed = new UnityEvent();

    private Dictionary<KeyCode, UnityEvent> KeyEventMap = new();

    private void Awake()
    {
        AddKeyCodes(Jump, OnJumpKeyPressed);
        AddKeyCodes(Dash, OnJumpKeyPressed);
        AddKeyCodes(ShootProjectile, OnShootProjectileKeyPressed);
    }

    void AddKeyCodes(KeyCode[] keyCodes, UnityEvent eventToAssign)
    {
        if (eventToAssign == null)
            return;

        foreach (var key in keyCodes)
        {
            KeyEventMap.Add(key, eventToAssign);
        }
    }

    // Could be really bad if this grows into something bigger, but should be fine for now.
    void Update()
    {
        foreach (var registered in KeyEventMap)
        {
            if(Input.GetKeyDown(registered.Key))
            {
                registered.Value.Invoke();
            }
        }
    }
}
