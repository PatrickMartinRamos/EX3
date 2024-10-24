using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class computerScript : MonoBehaviour
{
    public UnityEvent onComputerInteract; 
    private bool isPlayerInRange = false; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player entered interaction zone: " + gameObject.name);
            isPlayerInRange = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player left interaction zone: " + gameObject.name);
            isPlayerInRange = false;
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)) 
        {
            //Debug.Log("Player pressed E to interact with: " + gameObject.name);
            if (onComputerInteract != null)
            {
                onComputerInteract.Invoke();
            }
        }
    }
}
