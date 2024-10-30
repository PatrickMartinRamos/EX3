using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class spikeTrap : MonoBehaviour
{
    [System.Serializable]
    public class Spike
    {
        public GameObject spikeObject;
        public float speed = 1f;
        public float delay = 0f;
        public float distance = 1f;
        [HideInInspector] public Vector3 startPosition;
        [HideInInspector] public bool movingUp = true;
    }

    public List<Spike> spikes = new List<Spike>();

    void Start()
    {
        foreach (var spike in spikes)
        {
            spike.startPosition = spike.spikeObject.transform.position;

            // Start the spike movement after its initial delay
            StartSpikeMovement(spike);
        }
    }

    void StartSpikeMovement(Spike spike)
    {
        // Move up and back to start position
        spike.spikeObject.transform.DOMoveY(spike.startPosition.y + spike.distance, spike.speed)
            .SetDelay(spike.delay)
            .SetLoops(-1, LoopType.Yoyo); // Repeat indefinitely, moving up and back
    }

    // Method to add a new spike
    public void AddSpike(GameObject spikePrefab, float speed, float delay, float distance)
    {
        GameObject newSpike = Instantiate(spikePrefab, transform);
        newSpike.transform.localPosition = Vector3.zero;

        var newSpikeInstance = new Spike
        {
            spikeObject = newSpike,
            speed = speed,
            delay = delay,
            distance = distance,
            startPosition = newSpike.transform.position
        };

        spikes.Add(newSpikeInstance);
        StartSpikeMovement(newSpikeInstance);
    }
}
