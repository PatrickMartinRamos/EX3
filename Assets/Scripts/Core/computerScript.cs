using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class computerScript : MonoBehaviour
{
    public UnityEvent onComputerInteract; 
    private bool isPlayerInRange = false;

    [SerializeField] private GameObject showOpenGateTextBoxBG;
    [SerializeField] private TextMeshProUGUI showOpenGateCompText;

    private void Start()
    {
        showOpenGateTextBoxBG.SetActive(false);
        showOpenGateCompText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player entered interaction zone: " + gameObject.name);
            showtextElement();
            isPlayerInRange = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player left interaction zone: " + gameObject.name);
            isPlayerInRange = false;

            showOpenGateTextBoxBG.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBounce).OnComplete(() =>
            {
                showOpenGateTextBoxBG.SetActive(false);
            });
            showOpenGateCompText.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBounce).OnComplete(() =>
            {
                showOpenGateCompText.gameObject.SetActive(false);
            });
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


    void showtextElement()
    {
        showOpenGateTextBoxBG.SetActive(true);
        showOpenGateCompText.gameObject.SetActive(true);

        showOpenGateTextBoxBG.transform.localScale = Vector3.zero;
        showOpenGateCompText.transform.localScale = Vector3.zero;

        showOpenGateTextBoxBG.transform.DOScale(new Vector3(1.093f, 0.6871229f, 1), 0.3f).SetEase(Ease.OutBounce);
        showOpenGateCompText.transform.DOScale(new Vector3(1, 1.605136f, 1), 0.3f).SetEase(Ease.OutBounce);
    }
}
