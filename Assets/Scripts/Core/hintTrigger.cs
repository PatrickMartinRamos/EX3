using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class hintTrigger : MonoBehaviour
{
    [SerializeField] private scriptHandler hintHandler;

    [Header("Hints")]
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private GameObject hintTextBG;

    private void Start()
    {
        hintText.gameObject.SetActive(false);
        hintTextBG.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            switch (gameObject.tag)
            {
                case "hintTrigger_1":
                    if (hintHandler.hintLines.Count > 0)
                    {
                        hintText.text = hintHandler.hintLines[0].hintLine;
                        ShowHintElements();
                    }
                    break;

                case "hintTrigger_2":
                    if (hintHandler.hintLines.Count > 1)
                    {
                        hintText.text = hintHandler.hintLines[1].hintLine;
                        ShowHintElements();
                    }
                    break;

                case "hintTrigger_3":
                    if (hintHandler.hintLines.Count > 2)
                    {
                        hintText.text = hintHandler.hintLines[2].hintLine;
                        ShowHintElements();
                    }
                    break;

                default:
                    Debug.Log("No hint associated with this trigger.");
                    break;
            }
        }
    }

    private void ShowHintElements()
    {
        hintText.gameObject.SetActive(true);
        hintTextBG.SetActive(true);

        // Bouncing effect
        hintText.transform.localScale = Vector3.zero;
        hintTextBG.transform.localScale = Vector3.zero;

        hintText.transform.DOScale(new Vector3(0.12f, 0.12f, 0.12f), 0.3f).SetEase(Ease.OutBounce);
        hintTextBG.transform.DOScale(new Vector3(5.8f, 5.8f, 0), 0.3f).SetEase(Ease.OutBounce);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hintText.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBounce).OnComplete(() =>
            {
                hintText.gameObject.SetActive(false);
            });
            hintTextBG.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBounce).OnComplete(() =>
            {
                hintTextBG.SetActive(false);
            });
        }
    }
}
