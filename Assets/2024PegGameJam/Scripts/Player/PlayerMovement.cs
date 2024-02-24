using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private float jumpStrength = 5f;

    private Rigidbody2D playerRigidbody;
    private Vector2 moveVector = new Vector2 (0,0);
    private Vector2 jumpVector = new Vector2 (0,0);


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // MUST REMAIN AS FIXED UPDATE
    // Physics should be used inside of Fixed Update, not Update
    void FixedUpdate()
    {
        ApplyMovement();

    }

    private void Update()
    {
        ConsumeMovement();
        ConsumeJump();
    }

    private void ApplyMovement()
    {
        moveVector.y = playerRigidbody.velocity.y;
        moveVector.x *= moveSpeed;
        playerRigidbody.velocity = moveVector;

        jumpVector = new Vector2(0, jumpStrength);
        playerRigidbody.AddForce(jumpVector, ForceMode2D.Impulse);
    }

    // Movement Logic
    private void ConsumeMovement()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveVector.x = -1;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveVector.x = 1;
        }
    }

    // Jump Logic
    private void ConsumeJump()
    {
        // Check if Space or W is pressed
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Apply the Jump strength

        }
    }

}
