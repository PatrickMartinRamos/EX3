using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class resetPlayerAndBoxComputer : MonoBehaviour
{
    [SerializeField] private Transform boxPos;
    [SerializeField] private Transform resetPosition;

    [SerializeField] private GameObject showResetCompTextBoxBG;
    [SerializeField] private TextMeshProUGUI showResetCompText;

    private bool isPlayerInRange = false;

    private void Start()
    {
        showResetCompTextBoxBG.SetActive(false);
        showResetCompText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            showtextElement();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;

            showResetCompTextBoxBG.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBounce).OnComplete(() =>
            {
                showResetCompTextBoxBG.SetActive(false);
            });
            showResetCompText.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBounce).OnComplete(() =>
            {
                showResetCompText.gameObject.SetActive(false);
            });
        }
    }

    private void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            boxPos.transform.position = resetPosition.position;
        }
    }

    void showtextElement()
    {
        showResetCompTextBoxBG.SetActive(true);
        showResetCompText.gameObject.SetActive(true);

        showResetCompTextBoxBG.transform.localScale = Vector3.zero;
        showResetCompText.transform.localScale = Vector3.zero;

        showResetCompTextBoxBG.transform.DOScale(new Vector3(1.277f, 0.83482f, 1), 0.3f).SetEase(Ease.OutBounce);
        showResetCompText.transform.DOScale(new Vector3(1, 1.605136f, 1), 0.3f).SetEase(Ease.OutBounce);
    }
    void OnDestroy()
    {
        DOTween.KillAll();
    }
}
