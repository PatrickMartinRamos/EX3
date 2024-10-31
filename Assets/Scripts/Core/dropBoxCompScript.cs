using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class dropBoxCompScript : MonoBehaviour
{
    [SerializeField] private GameObject showDropBoxTextBoxBG;
    [SerializeField] private TextMeshProUGUI showDropBoxText;

    [SerializeField] private boxDroperScript _boxDrop;

    private bool isPlayerInRange = false;
    private float timer = 0f;

    private void Start()
    {
        timer = 0f;
        showDropBoxTextBoxBG.SetActive(false);
        showDropBoxText.gameObject.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && timer > 1)
        {
            _boxDrop.dropBox();
            timer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            showTextElement();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;

            showDropBoxTextBoxBG.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBounce).OnComplete(() =>
            {
                showDropBoxTextBoxBG.SetActive(false);
            });
            showDropBoxText.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBounce).OnComplete(() =>
            {
                showDropBoxText.gameObject.SetActive(false);
            });
        }
    }

    void showTextElement()
    {
        showDropBoxTextBoxBG.SetActive(true);
        showDropBoxText.gameObject.SetActive(true);

        showDropBoxTextBoxBG.transform.localScale = Vector3.zero;
        showDropBoxText.transform.localScale = Vector3.zero;

        showDropBoxTextBoxBG.transform.DOScale(new Vector3(1.277f, 0.83482f, 1), 0.3f).SetEase(Ease.OutBounce);
        showDropBoxText.transform.DOScale(new Vector3(1, 1.605136f, 1), 0.3f).SetEase(Ease.OutBounce);
    }
}
