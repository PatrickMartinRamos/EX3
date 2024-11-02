using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windScript : MonoBehaviour
{
    [SerializeField] private float windStrength;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * windStrength, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("PressurePlate"))
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("dropBox"))
        {
            var getRb = collision.gameObject.GetComponent<Rigidbody2D>();

            getRb.AddForce(Vector2.right * 20,ForceMode2D.Impulse);
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Wind"))
        {
            Destroy(gameObject);
        }
    }
}
