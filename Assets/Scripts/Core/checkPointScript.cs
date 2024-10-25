using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPointScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerScript playerScript = collision.GetComponent<playerScript>();
            playerScript.setSavePos(collision.transform.position);
        }

    }
}
