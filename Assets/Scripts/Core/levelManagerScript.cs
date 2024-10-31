using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManagerScript : MonoBehaviour
{
    public static levelManagerScript Instance { get; private set; }
    public int CurrentLevel { get; private set; } = 1;
    [SerializeField] private cameraScript camScript;
    [SerializeField] private List<GameObject> Levels;
    [SerializeField] private List<Transform> lvlStartPos;
    [SerializeField] private Transform playerTransform; // Reference to the player's Transform

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

        InitializeLevels();
    }

    private void InitializeLevels()
    {
        // Start game with only level 1 active
        for (int i = 0; i < Levels.Count; i++)
        {
            Levels[i].SetActive(i == 0);
        }

        // Set the player to the start position of the first level
        if (lvlStartPos.Count > 0 && playerTransform != null)
        {
            playerTransform.position = lvlStartPos[0].position;
        }
    }

    public void ChangeLevel()
    {
        CurrentLevel++;
        Debug.Log($"Changed to level {CurrentLevel}");

        if (CurrentLevel > Levels.Count)
        {
            Debug.LogWarning("No more levels to change to.");
            return;
        }

        // Activate the current level and deactivate others
        for (int i = 0; i < Levels.Count; i++)
        {
            Levels[i].SetActive(i == CurrentLevel - 1);
        }

        // Move the player to the start position of the new level
        if (CurrentLevel - 1 < lvlStartPos.Count && playerTransform != null)
        {
            playerTransform.position = lvlStartPos[CurrentLevel - 1].position;
        }

        // Update the camera based on the current level
        if (camScript != null)
        {
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
                    Debug.LogWarning("No camera settings defined for this level.");
                    break;
            }
        }
    }
}
