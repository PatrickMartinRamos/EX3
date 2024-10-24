using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public enum MovementType { Horizontal, Vertical }
    public MovementType movementType = MovementType.Horizontal;

    public float speed = 2f;
    public float distance = 3f;

    private Vector3 startPosition;
    private bool movingForward = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        Vector3 direction = movementType == MovementType.Horizontal ? Vector3.right : Vector3.up;
        float movement = Mathf.PingPong(Time.time * speed, distance);

        if (movingForward)
            transform.position = startPosition + direction * movement;
        else
            transform.position = startPosition - direction * movement;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // When the player or box lands on the platform, make it a child
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
        {
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // When the player or box leaves the platform, unparent it
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
        {
            collision.transform.SetParent(null);
        }
    }
}
