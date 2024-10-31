using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxDroperScript : MonoBehaviour
{
    [SerializeField] private GameObject boxToDrop;
    [SerializeField] private Transform boxDropPos;

    public void dropBox()
    {
        Instantiate(boxToDrop,boxDropPos.transform.position, Quaternion.identity);
    }
}
