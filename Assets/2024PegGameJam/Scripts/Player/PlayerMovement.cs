using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerMovement : MonoBehaviour
{

    // ------ EXTERNAL ------
    [Header("Movement")]
    [Space]
    [SerializeField]
    private float moveSpeed = 3f;
    [Space]
    [SerializeField]
    private float direction = 1.0f;

    [Space]
    [SerializeField]
    private int maxAdditionalJumps = 1;
    [SerializeField]
    private float jumpStrength = 5f;

    [Space]
    [SerializeField]
    private int maxDashes = 1;
    [SerializeField]
    private float dashStrength = 5f;
    [SerializeField]
    private float dashDuration = 0.5f;
    [SerializeField]
    private float dashCooldown = 1f;

    [Space]
    [Header("Ground Checking")]
    [Space]
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float groundCheckDistance = 5f;

    // ------ INTERNAL ------
    private Rigidbody2D playerRigidbody;

    private Vector2 moveVector = new Vector2(0, 0);
    private Vector2 dashVector = new Vector2(0, 0);

    private bool isDashing = false;
    private float dashCounter = 0f;
    private float dashCooldownCounter = 0f;

    private int currentDashesCharges = 0;
    private int currentJumpCharges = 0;

    private PlayerInput keyStrokes;


    private void Awake()
    {
        keyStrokes = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentDashesCharges = maxDashes;
        currentJumpCharges = maxAdditionalJumps;
        keyStrokes.OnJumpKeyPressed.AddListener(AppyJump);
        keyStrokes.OnDashKeyPressed.AddListener(ApplyDash);
    }

    // MUST REMAIN AS FIXED UPDATE
    // Physics should be used inside of Fixed Update, not Update
    void FixedUpdate()
    {
        CheckGround();

        if (isDashing == false)
        {
            ApplyMovement();
        }
        else
        {
            if (currentDashesCharges > 0)
            {
                dashCounter -= Time.deltaTime;
                // should replace this with forward, but will need to add a flip function
                dashVector.x = direction * dashStrength;
                playerRigidbody.AddForce(dashVector, ForceMode2D.Force);

                if (dashCounter < 0)
                {
                    isDashing = false;
                }
            }
        }
    }

    // Apply Input processes inside of update
    private void Update()
    {
        ConsumeMovement();

        if (dashCooldownCounter > 0)
        {
            dashCooldownCounter -= Time.deltaTime;
        }
        // Debug.Log(string.Format("    dashCooldownCounter: {0}", dashCooldownCounter));
    }

    // Use the move vector to move our character
    private void ApplyMovement()
    {
        playerRigidbody.velocity = moveVector;
    }


    // Checks if player is hitting ground
    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        if (hit.collider != null)
        {
            currentJumpCharges = maxAdditionalJumps;
            //Debug.Log(string.Format("    currentJumpCharges: {0}", currentJumpCharges));
        }
    }

    // Movement Logic
    private void ConsumeMovement()
    {
        // Reset our move vector before doing operations to it
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

        // apply movespeed to our move vector
        moveVector.x *= moveSpeed;

        ApplyDirection();
    }

    private void ApplyDirection()
    {
        float previousDirection = direction;
        if (moveVector.x != 0)
        {
            direction = moveVector.x > 0 ? 1 : -1;
        }

        if (direction != previousDirection)
        {
            bool isFacingForward = direction > 0;
            gameObject.GetComponent<SpriteRenderer>().flipX = !isFacingForward;
        }

    }

    // Jump Logic
    private void AppyJump()
    {
        Vector2 jumpVector = new Vector2(0, jumpStrength);

        if (currentJumpCharges > 0)
        {
            // Apply the Jump strength
            // Debug.Log(string.Format("    playerRigidbody.velocity: {0}", playerRigidbody.velocity));
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
            playerRigidbody.AddForce(jumpVector, ForceMode2D.Impulse);
            currentJumpCharges--;
        }
    }

    private void ApplyDash()
    {
        bool isDashOnCooldown = dashCooldownCounter > 0;

        if (!isDashOnCooldown)
        {
            isDashing = true;
            dashCounter = dashDuration;
            dashCooldownCounter = dashCooldown;
        }
        
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + groundCheckDistance * -1,0));
    }
#endif


}
