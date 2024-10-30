using UnityEngine;

public class animationScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    private float lastDirection = 1; // 1 for right, -1 for left

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        flipSprite();
        animController();
    }

    void flipSprite()
    {
        // Check for input to update last direction
        if (rb.velocity.x > 0.1f)
        {
            lastDirection = 1;
            sprite.flipX = false;
        }
        else if (rb.velocity.x < -0.1f)
        {
            lastDirection = -1;
            sprite.flipX = true;
        }
        else
        {
            // No input: keep sprite facing last direction
            sprite.flipX = lastDirection == -1;
        }
    }

    void animController()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            animator.SetBool("starWalk", true);
        }
        else
        {
            animator.SetBool("starWalk", false);
        }
    }
}
