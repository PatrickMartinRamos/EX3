using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI introText;
    [SerializeField] sceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        introText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
           introText.gameObject.SetActive(true);
           if (Input.GetKeyDown(KeyCode.Return))
           {
                sceneLoader.loadLevel_1();
           }
        }
    }

}
