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
    private bool isGrounded = false;


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
        playerRigidbody.velocity = moveVector;
        playerRigidbody.AddForce(jumpVector, ForceMode2D.Impulse);
    }

    // Movement Logic
    private void ConsumeMovement()
    {
        moveVector.x = 0;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveVector.x = -1;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveVector.x = 1;
        }

        moveVector.y = playerRigidbody.velocity.y;
        moveVector.x *= moveSpeed;
    }

    // Jump Logic
    private void ConsumeJump()
    {
        jumpVector.y = 0;
        // Check if Space or W is pressed
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // Apply the Jump strength
            jumpVector = new Vector2(0, jumpStrength);
        }
    }

}
