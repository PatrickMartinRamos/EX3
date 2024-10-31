using System.Collections;
using UnityEngine;

public class boxScript : MonoBehaviour
{
    private bool isGrounded = false;
    private Rigidbody2D rb;
    public float groundCheckDistance = 0.1f; // Adjust this value as necessary

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // Start as kinematic
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;

            // Check if the box is above the ground
            if (IsAboveGround())
            {
                rb.bodyType = RigidbodyType2D.Kinematic; // Set to kinematic when on the ground
                rb.velocity = Vector2.zero; // Reset velocity
            }
        }
        if(collision.gameObject.CompareTag("Trash"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }

    private void Update()
    {
        if (!isGrounded)
        {
            rb.bodyType = RigidbodyType2D.Dynamic; // Switch to dynamic when not grounded
        }
    }

    private bool IsAboveGround()
    {
        // Perform a raycast downwards to check for ground directly below the box
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, LayerMask.GetMask("Ground"));
        return hit.collider != null; // If we hit something, we're above the ground
    }
}
