using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class pressurePlateScript : MonoBehaviour
{
    public UnityEvent onPressurePlateActivate;
    public float deactivateDelay = 1.0f; // Delay in seconds for deactivating the box

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            // Activate the pressure plate immediately
            onPressurePlateActivate?.Invoke();

            // Start the coroutine to deactivate the box after a delay
            StartCoroutine(DeactivateBoxAfterDelay(collision.gameObject));
        }
    }

    private IEnumerator DeactivateBoxAfterDelay(GameObject box)
    {
        yield return new WaitForSeconds(deactivateDelay); // Wait for the specified delay

        // Deactivate the box object
        box.SetActive(false);
    }
}
