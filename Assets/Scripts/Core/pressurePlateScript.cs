using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class pressurePlateScript : MonoBehaviour
{
    public UnityEvent onPressurePlateActivate;
    [SerializeField] private float deactivateDelay = 1.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            onPressurePlateActivate?.Invoke();
            StartCoroutine(DeactivateBoxAfterDelay(collision.gameObject));
        }
        else if(collision.gameObject.CompareTag("BigBox"))
        {
            onPressurePlateActivate?.Invoke();
            StartCoroutine(DeactivateBoxAfterDelay(collision.gameObject));
        }
        else if (collision.gameObject.CompareTag("dropBox"))
        {
            onPressurePlateActivate?.Invoke();
            StartCoroutine(DeactivateBoxAfterDelay(collision.gameObject));
        }
        else if (collision.gameObject.CompareTag("dropBoxlvl2"))
        {
            onPressurePlateActivate?.Invoke();
            StartCoroutine(DeactivateBoxAfterDelay(collision.gameObject));
        }
    }

    private IEnumerator DeactivateBoxAfterDelay(GameObject box)
    {
        yield return new WaitForSeconds(deactivateDelay);
        box.SetActive(false);
    }
}
