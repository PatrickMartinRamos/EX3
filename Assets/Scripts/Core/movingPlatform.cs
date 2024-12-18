using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public enum MovementType { Horizontal, Vertical }
    [SerializeField] private MovementType movementType = MovementType.Horizontal;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float distance = 3f; //from org position to distance

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
        {
            transform.position = startPosition + direction * movement;
        }
        else
        {
            transform.position = startPosition - direction * movement;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
        {
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
        {
            if (gameObject.activeInHierarchy) // Check if the platform is still active
            {
                collision.transform.SetParent(null);
            }
        }
    }
}
