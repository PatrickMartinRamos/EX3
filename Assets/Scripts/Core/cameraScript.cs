using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float camXoffset;
    [SerializeField] private float camYoffset;
    [SerializeField] private float minX = -7f;
    [SerializeField] private float maxX = 7f;
    [SerializeField] private float minY = 2f;
    [SerializeField] private float maxY = 3f;

    // Update is called once per frame
    void Update()
    {
        // Calculate the new camera position
        Vector3 newCamPosition = new Vector3(playerTransform.transform.position.x + camXoffset, playerTransform.transform.position.y + camYoffset, -10f);

        // Clamp the camera position
        newCamPosition.x = Mathf.Clamp(newCamPosition.x, minX, maxX);
        newCamPosition.y = Mathf.Clamp(newCamPosition.y, minY, maxY);

        cam.transform.position = newCamPosition;
    }
}
