using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPlayerAndBoxComputer : MonoBehaviour
{
    [SerializeField] private Transform boxPos;
    [SerializeField] private Transform resetPosition;

    private bool isPlayerInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            boxPos.transform.position = resetPosition.position;
        }
    }
}
