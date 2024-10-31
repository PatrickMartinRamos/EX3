using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class blowerScript : MonoBehaviour
{
    [SerializeField] private GameObject wind;
    [SerializeField] private Transform windPos;
    private float timer;

    private void Start()
    {

    }

    private void Update()
    {
        timer += Time.deltaTime;
        startBlow();
    }

    void startBlow()
    {
        if(timer > 2f)
        {
            timer = 0f;
            Instantiate(wind, windPos.transform.position, Quaternion.identity);
        }
    }
}
