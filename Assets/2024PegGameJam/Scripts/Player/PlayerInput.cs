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

    [NonSerialized]
    public UnityEvent OnJumpKeyPressed = new UnityEvent();

    [NonSerialized]
    public UnityEvent OnDashKeyPressed = new UnityEvent();

    // Could be really bad if this grows into something bigger, but should be fine for now.
    void Update()
    {
        foreach (KeyCode key in Jump)
        {
            if (Input.GetKeyDown(key))
            {
                OnJumpKeyPressed?.Invoke();
            }
        }

        foreach (KeyCode key in Dash)
        {
            if (Input.GetKeyDown(key))
            {
                OnDashKeyPressed?.Invoke();
            }
        }
    }
}
