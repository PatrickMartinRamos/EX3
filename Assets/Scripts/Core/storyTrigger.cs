using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class storyTrigger : MonoBehaviour
{
    [SerializeField] private scriptHandler storyHandler;
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private TextMeshProUGUI pressEnterTxt;
    [SerializeField] private GameObject storyBoxBG;
    [SerializeField] private Camera cam;
    private playerScript _playerScript;
    private bool isDialogueActive = false;
    private int textIndex = 0;
    private int currentStoryIndex = 0;
    private float characterSpeed = 0.005f;
    private float timer = 0f;
    private string currentText = "";
    private int charIndex = 0;
    private BoxCollider2D boxCollider;
    public AudioSource storyAudio;
    private GameObject audioSource;

    private void Start()
    {
        storyText.gameObject.SetActive(false);
        storyBoxBG.SetActive(false);
        _playerScript = FindAnyObjectByType<playerScript>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GameObject.Find("playerAudio");
        if (audioSource != null)
        {
            storyAudio = audioSource.GetComponent<AudioSource>();
            //todo make audio source for story
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isDialogueActive)
        {
            switch (gameObject.tag)
            {
                case "storyTrigger_1":
                    currentStoryIndex = 0;
                    boxCollider.enabled = false;
                    StartDialogue();
                    break;

                case "storyTrigger_2":
                    currentStoryIndex = 1;
                    boxCollider.enabled = false;
                    StartDialogue();
                    break;

                case "storyTrigger_3":
                    currentStoryIndex = 2;
                    boxCollider.enabled = false;
                    StartDialogue();
                    break;

                case "storyTrigger_4":
                    currentStoryIndex = 3;
                    boxCollider.enabled = false;
                    StartDialogue();
                    break;

                case "storyTrigger_5":
                    currentStoryIndex = 4;
                    boxCollider.enabled = false;
                    StartDialogue();
                    break;

                case "storyTrigger_6":
                    currentStoryIndex = 5;
                    boxCollider.enabled = false;
                    StartDialogue();
                    break;

                case "storyTrigger_7":
                    currentStoryIndex = 6;
                    boxCollider.enabled = false;
                    StartDialogue();
                    break;

                default:
                    Debug.Log("No story associated with this trigger."); 
                    break;
            }
        }
    }

    private void StartDialogue()
    {
        isDialogueActive = true;
        _playerScript.stopMove(true);
        textIndex = 0;
        charIndex = 0;
        timer = 0f;
        currentText = storyHandler.storyElements[currentStoryIndex].storyLines[textIndex].storyText;
        storyText.gameObject.SetActive(true);

        storyBoxBG.SetActive(true);
        storyBoxBG.transform.localScale = Vector3.zero; // Start from 0 scale
        storyBoxBG.transform.DOScale(123.5f, 0.5f).SetEase(Ease.OutBounce); // Scale up with bounce effect
        pressEnterTxt.DOFade(0, 1f).SetLoops(-1, LoopType.Yoyo);
        storyText.text = "";
    }

    private void Update()
    {
        if (isDialogueActive)
        {
            timer += Time.deltaTime;

            if (timer > characterSpeed)
            {
                if (charIndex < currentText.Length)
                {
                    storyText.text += currentText[charIndex];
                    charIndex++;
                    timer = 0f;
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    textIndex++;
                    DisplayNextStoryElement();
                }
            }
        }

        storyBoxBG.transform.position = new Vector3(cam.transform.position.x + .5f, cam.transform.position.y + -6, 0f);
    }

    public void activateTriggerColliderOnReset(bool activateTrigger)
    {
        boxCollider.enabled = activateTrigger;
    }

    private void DisplayNextStoryElement()
    {
        if (storyHandler.storyElements.Count > currentStoryIndex)
        {
            if (textIndex < storyHandler.storyElements[currentStoryIndex].storyLines.Count)
            {
                charIndex = 0;
                timer = 0f;
                currentText = storyHandler.storyElements[currentStoryIndex].storyLines[textIndex].storyText;
                storyText.text = "";
            }
            else
            {
                EndDialogue();
            }
        }
    }

    private void EndDialogue()
    {
        // Hide storyBoxBG with a bounce effect
        storyBoxBG.transform.DOScale(0f, 0.5f).SetEase(Ease.InBounce).OnComplete(() =>
        {
            storyBoxBG.SetActive(false);
            storyText.gameObject.SetActive(false);
        });

        isDialogueActive = false;
        _playerScript.stopMove(false);
    }
}
