using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Colors { blue, red, yellow, green};

public class poweredWireStats : MonoBehaviour
{
    public bool movable = false;
    public bool moving = false;
    public Vector3 startPosition;
    public Colors objetColor;
    public bool connected = false;
    public Vector3 connectedPos;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
