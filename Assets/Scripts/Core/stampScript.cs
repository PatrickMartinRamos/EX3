using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stampScript : MonoBehaviour
{
    [System.Serializable]
    public class Stamp
    {
        public GameObject stampObject;  // The stamp object
        public float speed = 1f;        // Speed of movement
        public float delay = 0f;        // Delay before the stamp starts moving
        public float distance = 5f;     // Distance to move down before returning
        [HideInInspector] public bool started = false;  // To track if the stamp started moving
        [HideInInspector] public float timer = 0f;      // Timer to manage delay
        [HideInInspector] public Vector3 startPosition; // Original position of the stamp
        [HideInInspector] public bool movingDown = true; // Direction of movement
        [HideInInspector] public BoxCollider2D stampCollider;
    }

    public List<Stamp> stamps = new List<Stamp>();

    void Start()
    {
        foreach (var stamp in stamps)
        {
            stamp.startPosition = stamp.stampObject.transform.position;

            var triggerComponent = stamp.stampObject.AddComponent<StampTrigger>();
            triggerComponent.stamp = stamp;
        }
    }

    void Update()
    {
        foreach (var stamp in stamps)
        {
            if (!stamp.started)
            {
                stamp.timer += Time.deltaTime;
                if (stamp.timer >= stamp.delay)
                {
                    stamp.started = true;
                }
            }
            else
            {
                if (stamp.movingDown)
                {
                    stamp.stampObject.transform.Translate(Vector3.down * stamp.speed * Time.deltaTime);
                    if (Vector3.Distance(stamp.startPosition, stamp.stampObject.transform.position) >= stamp.distance)
                    {
                        stamp.movingDown = false;
                    }
                }
                else
                {
                    // Move stamp back up to the starting position
                    stamp.stampObject.transform.Translate(Vector3.up * stamp.speed * Time.deltaTime);

                    if (Vector3.Distance(stamp.startPosition, stamp.stampObject.transform.position) <= 0.1f)
                    {
                        stamp.movingDown = true;
                    }
                }
            }
        }
    }

    // Method to add a new stamp
    public void AddStamp(GameObject stampPrefab, float speed, float delay, float distance)
    {
        GameObject newStamp = Instantiate(stampPrefab, transform);
        newStamp.transform.localPosition = Vector3.zero;

        var newStampInstance = new Stamp
        {
            stampObject = newStamp,
            speed = speed,
            delay = delay,
            distance = distance,
            startPosition = newStamp.transform.position
        };

        stamps.Add(newStampInstance);
    }
}

// Separate component for handling player collision
public class StampTrigger : MonoBehaviour
{
    public stampScript.Stamp stamp;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript playerScript = collision.gameObject.GetComponent<playerScript>();
            playerScript.resetToSavePos();
            //stamp.OnPlayerCollision();
        }
    }
}
