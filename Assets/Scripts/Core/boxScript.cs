using System.Collections;
using UnityEngine;

public class boxScript : MonoBehaviour
{
    private bool isGrounded = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            rb.bodyType = RigidbodyType2D.Kinematic; // Set to kinematic when on the ground
            rb.velocity = Vector2.zero;
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
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
