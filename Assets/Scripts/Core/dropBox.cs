using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropBox : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Trash"))
        {
            Destroy(gameObject);
        }
    }
}
