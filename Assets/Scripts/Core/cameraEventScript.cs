using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraEventScript : MonoBehaviour
{
    /// <summary>
    /// note: set the vCam_1-Main camera as 0
    /// set the duration of the vCam_1-Main camera to 0
    /// </summary>

    [System.Serializable]
    public class CameraEvent
    {
        public GameObject camera; 
        public float duration;    
    }

    public static cameraEventScript Instance;

    // Note: Set vCam_1-Main Cam as index 0 in each list.
    [Header("Level 1 Cam Events")]
    [SerializeField] private List<CameraEvent> lvl1_VCams;
    [Header("Level 2 Cam Events")]
    [SerializeField] private List<CameraEvent> lvl2_VCams;
    [Header("Level 3 Cam Events")]
    [SerializeField] private List<CameraEvent> lvl3_VCams;

    private void Start()
    {
        Instance = this;

        // Initialize to the main camera for each level
        SetMainCamera(lvl1_VCams);
        SetMainCamera(lvl2_VCams);
        SetMainCamera(lvl3_VCams);
    }

    private void SetMainCamera(List<CameraEvent> camList)
    {
        for (int i = 0; i < camList.Count; i++)
        {
            camList[i].camera.SetActive(i == 0);  // Only activate the main camera (index 0)
        }
    }

    #region Level 1 Cam Events
    public void onLevelOneGreenGateOpenEvent()
    {
        int[] cameraIndices = { 1 };
        StartCoroutine(ActivateEventCameras(lvl1_VCams,cameraIndices));
    }
    #endregion

    #region Level 2 Cam Events 
    // Level 2 events
    public void onWindBlowFinishEvent()
    {
        int[] cameraIndices = { 1, 2 }; // Activate cameras 1 and 2
        StartCoroutine(ActivateEventCameras(lvl2_VCams, cameraIndices));
    }
    #endregion

    private IEnumerator ActivateEventCameras(List<CameraEvent> camList, int[] cameraIndices)
    {
        // Activate the cameras in sequence
        for (int i = 0; i < cameraIndices.Length; i++)
        {
            int camIndex = cameraIndices[i];

            // Activate the current event camera
            camList[camIndex].camera.SetActive(true);
            camList[0].camera.SetActive(false);  // Deactivate main camera

            // Wait for the specified duration
            yield return new WaitForSeconds(camList[camIndex].duration);

            // Deactivate the current event camera before the next one
            camList[camIndex].camera.SetActive(false);
        }

        // Finally, reset back to the main camera
        camList[0].camera.SetActive(true);  // Reactivate main camera
    }
}
