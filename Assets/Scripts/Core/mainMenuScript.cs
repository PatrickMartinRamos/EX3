using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening; 

public class mainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject controls;

    private void Start()
    {
        controls.gameObject.SetActive(false);
    }

    public void goToIntro()
    {
        SceneManager.LoadScene(1);
    }

    public void showControls()
    {
        controls.gameObject.SetActive(true);
        //controls.transform.localScale = Vector3.zero;  // Set initial scale to 0
        //controls.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
    }

    public void hideControls()
    {
        controls.gameObject.SetActive(false);
    }

    public void quitApp()
    {
        Application.Quit();
    }
}
