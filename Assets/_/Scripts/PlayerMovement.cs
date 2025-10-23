using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // Horizontal movement speed
    public float jumpForce = 10f;       // Force applied when jumping
    public Rigidbody2D rb;              // Reference to Rigidbody2D component

    private float horizontalInput;      // Store horizontal input
    public bool isGrounded;            // Is the player on the ground?
    private bool canDoubleJump;         // Can the player perform a double jump?

    public Transform groundCheck;       // Position to check if grounded
    public float groundCheckRadius = 0.2f; // Radius of ground check circle
    public LayerMask groundLayer;       // Layer considered as ground

    Animator m_Animator;
    private bool grounded;


    void Start()
    {
        //This gets the Animator, which should be attached to the GameObject you are intending to animate.
        m_Animator = gameObject.GetComponent<Animator>();

    }


    void Update()
    {
        // Get horizontal input (-1 for left, 1 for right)
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Flip the player's sprite to face the direction of movement
        if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
            m_Animator.SetBool("run", true);
        }
        else if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
            m_Animator.SetBool("run", true);
        }
        else
        {

            m_Animator.SetBool("run", false);
        }

           

        // Check if the player is on the ground (should happen before jump input)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump input
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                // First jump
                Jump();
                canDoubleJump = true; // Enable double jump
            }
            else if (canDoubleJump)
            {
                // Double jump
                Jump();
                canDoubleJump = false; // Use up double jump
            }
        }
    }

    void FixedUpdate()
    {
        // Apply horizontal movement using physics (keeps gravity working properly)
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        // Apply vertical velocity for jumping
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

    }
    public bool deathState = false; // Set default death state to false
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            deathState = true; // Say to GameManager that player is dead
        }
        else
        {
            deathState = false;
        }
    }


}
