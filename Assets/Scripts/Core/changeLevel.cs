using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeLevel : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject dropbox = GameObject.Find("dropBox");
            GameObject wind = GameObject.Find("Wind");

            if (dropbox != null)
            {
                Destroy(dropbox);
            }
            else
            {
                Debug.LogWarning("dropbox object not found!");
            }

            if (wind != null)
            {
                Destroy(wind);
            }

            levelManagerScript.Instance.ChangeLevel();
        }
    }
}
