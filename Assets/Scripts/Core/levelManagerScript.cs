using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManagerScript : MonoBehaviour
{
    public static levelManagerScript Instance { get; private set; }
    public int CurrentLevel { get; private set; } = 1;
    [SerializeField] private cameraScript camScript;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeLevel()
    {
        CurrentLevel++;
        Debug.Log($"Changed to level {CurrentLevel}");

        if (camScript != null)
        {
            // Change camera level based on the current level
            switch (CurrentLevel)
            {
                case 1:
                    camScript.changeLevel(cameraScript.Level.level_1);
                    break;
                case 2:
                    camScript.changeLevel(cameraScript.Level.level_2);
                    break;
                case 3:
                    camScript.changeLevel(cameraScript.Level.level_3);
                    break;
                default:
                    //Debug.LogWarning("No camera settings defined for this level.");
                    break;
            }
        }
    }
}
