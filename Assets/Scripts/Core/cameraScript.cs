using System;
using System.Collections.Generic;
using UnityEngine;
public class cameraScript : MonoBehaviour
{
    public enum Level { level_1, level_2, level_3 }

    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform vCam_1;
    [SerializeField] private Transform playerTransform;
    public Level currentLevel; // make this public so it can be change on level change adjust camera


    [System.Serializable]
    public struct CameraSettings
    {
        public float camXoffset;
        public float camYoffset;
        public float minX;
        public float maxX;
        public float minY;
        public float maxY;
    }

    [Header("Level Camera Settings")]
    [SerializeField] private CameraSettings level1Settings;
    [SerializeField] private CameraSettings level2Settings;
    [SerializeField] private CameraSettings level3Settings;

    private CameraSettings activeSettings;

    private void Start()
    {
        currentLevel = Level.level_1; //start the game at cam level 1 position
        //ApplyCameraSettings();
    }

    public void changeLevel(Level newLevel)
    {
        currentLevel = newLevel;
        ApplyCameraSettings();
    }

    void Update()
    {
        Vector3 newCamPosition = new Vector3(playerTransform.position.x + activeSettings.camXoffset, playerTransform.position.y + activeSettings.camYoffset, -10f);

        // Clamp the camera position
        newCamPosition.x = Mathf.Clamp(newCamPosition.x, activeSettings.minX, activeSettings.maxX);
        newCamPosition.y = Mathf.Clamp(newCamPosition.y, activeSettings.minY, activeSettings.maxY);

        cam.transform.position = newCamPosition;
        vCam_1.transform.position = newCamPosition;
        ApplyCameraSettings();
    }

    private void ApplyCameraSettings()
    {
        switch (currentLevel)
        {
            case Level.level_1:
                activeSettings = level1Settings;
                break;
            case Level.level_2:
                activeSettings = level2Settings;
                break;
            case Level.level_3:
                activeSettings = level3Settings;
                break;
        }
    }
}
