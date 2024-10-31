using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraEventScript : MonoBehaviour
{
    public static cameraEventScript Instance;

    // Note: Set vCam_1-Main Cam as index 0 in each list.
    [Header("Level 1 Cam Events")]
    [SerializeField] private List<GameObject> lvl1_VCams;
    [Header("Level 2 Cam Events")]
    [SerializeField] private List<GameObject> lvl2_VCams;
    [Header("Level 3 Cam Events")]
    [SerializeField] private List<GameObject> lvl3_VCams;

    private void Start()
    {
        Instance = this;

        // Initialize to the main camera for each level
        SetMainCamera(lvl1_VCams);
        SetMainCamera(lvl2_VCams);
        SetMainCamera(lvl3_VCams);
    }

    private void SetMainCamera(List<GameObject> camList)
    {
        for (int i = 0; i < camList.Count; i++)
        {
            camList[i].SetActive(i == 0);  // Only activate the main camera (index 0)
        }
    }

    #region Level 2 Cam Events 
    // Level 2 events
    public void onWindBlowFinishEvent()
    {
        int[] cameraIndices = { 1, 2 }; // Example: activate cameras 1 and 2
        float[] durations = { 3f, 3f }; // Duration for each camera
        StartCoroutine(ActivateEventCameras(lvl2_VCams, cameraIndices, durations));
    }
    #endregion

    private IEnumerator ActivateEventCameras(List<GameObject> camList, int[] cameraIndices, float[] durations)
    {
        // Ensure the camera indices and durations arrays are of the same length
        if (cameraIndices.Length != durations.Length)
        {
            Debug.LogError("Camera indices and durations arrays must be of the same length.");
            yield break;
        }

        // Activate the cameras in sequence
        for (int i = 0; i < cameraIndices.Length; i++)
        {
            // Activate the current event camera
            camList[cameraIndices[i]].SetActive(true);
            camList[0].SetActive(false);  // Deactivate main camera

            // Wait for the specified duration
            yield return new WaitForSeconds(durations[i]);

            // Deactivate the current event camera before the next one
            camList[cameraIndices[i]].SetActive(false);
        }

        // Finally, reset back to the main camera
        camList[0].SetActive(true);  // Reactivate main camera
    }
}
